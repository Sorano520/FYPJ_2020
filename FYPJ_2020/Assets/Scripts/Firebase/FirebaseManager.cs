using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Firebase;
using Firebase.Auth;
using Firebase.Firestore;
using Firebase.Extensions;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

public class FirebaseManager : MonoBehaviour
{
    public static FirebaseManager instance;
    private FirebaseApp app;
    private FirebaseAuth auth;
    private FirebaseFirestore database;
    protected Dictionary<string, Firebase.Auth.FirebaseUser> userByAuth = new Dictionary<string, Firebase.Auth.FirebaseUser>();

    // Flag set when a token is being fetched.  This is used to avoid printing the token
    // in IdTokenChanged() when the user presses the get token button.
    private bool fetchingToken = false;

    Dictionary<string, object> data = new Dictionary<string, object>();
    protected Task previousTask;
    protected bool operationInProgress;
    protected CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
    
    #region Getters & Setters
    public FirebaseManager Instance
    {
        get
        {
            return instance;
        }
    }
    public FirebaseApp App
    {
        get
        {
            if (app == null) app = FirebaseApp.DefaultInstance;
            return app;
        }
    }
    public FirebaseAuth Auth
    {
        get
        {
            if (auth == null) auth = FirebaseAuth.GetAuth(App);
            return auth;
        }
    }
    #endregion

    public UnityEvent OnFirebaseInit = new UnityEvent();
    public UnityEvent OnFireStoreResult = new UnityEvent();
    public UnityEvent OnSigninSuccessful = new UnityEvent();
    public UnityEvent OnSigninFailed = new UnityEvent();

    private async void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
            var dependencyResult = await FirebaseApp.CheckAndFixDependenciesAsync();
            if (dependencyResult == DependencyStatus.Available)
            {
                app = FirebaseApp.DefaultInstance;
                OnFirebaseInit.Invoke();
            }
            else GameManager.instance.con.LogError($"Failed to initialize Firebase with {dependencyResult}");
        }
        else GameManager.instance.con.LogWarning($"An instance of {nameof(FirebaseManager)} already exists!");
    }

    void Start()
    {
        GameManager.instance.con.Log("FirebaseManager.Start()");
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task => {
            var dependencyStatus = task.Result;
            GameManager.instance.con.Log("var dependencyStatus = task.Result;");
            if (dependencyStatus == DependencyStatus.Available) InitializeFirebase();
            else GameManager.instance.con.LogError("Could not resolve all Firebase dependencies: " + dependencyStatus);
        });
    }

    // Wait for task completion, throwing an exception if the task fails.
    // This could be typically implemented using
    // yield return new WaitUntil(() => task.IsCompleted);
    // however, since many procedures in this sample nest coroutines and we want any task exceptions
    // to be thrown from the top level coroutine (e.g GetKnownValue) we provide this
    // CustomYieldInstruction implementation wait for a task in the context of the coroutine using
    // common setup and tear down code.
    class WaitForTaskCompletion : CustomYieldInstruction
    {
        Task task;

        // Create an enumerator that waits for the specified task to complete.
        public WaitForTaskCompletion(Task task)
        {
            instance.previousTask = task;
            instance.operationInProgress = true;
            this.task = task;
        }

        // Wait for the task to complete.
        public override bool keepWaiting
        {
            get
            {
                if (task.IsCompleted)
                {
                    instance.operationInProgress = false;
                    instance.cancellationTokenSource = new CancellationTokenSource();
                    if (task.IsFaulted) GameManager.instance.con.Log(task.Exception.ToString());
                    return false;
                }
                return true;
            }
        }
    }

    protected void InitializeFirebase()
    {
        GameManager.instance.con.Log("Setting up Firebase Auth");
        auth = FirebaseAuth.DefaultInstance;
        database = FirebaseFirestore.DefaultInstance;
        auth.StateChanged += AuthStateChanged;
        auth.IdTokenChanged += IdTokenChanged;
        AuthStateChanged(this, null);
        SigninAnonymouslyAsync();
    }

    // Track state changes of the auth object.
    void AuthStateChanged(object sender, System.EventArgs eventArgs)
    {
        Firebase.Auth.FirebaseAuth senderAuth = sender as Firebase.Auth.FirebaseAuth;
        Firebase.Auth.FirebaseUser user = null;
        if (senderAuth != null) userByAuth.TryGetValue(senderAuth.App.Name, out user);
        if (senderAuth == auth && senderAuth.CurrentUser != user)
        {
            bool signedIn = user != senderAuth.CurrentUser && senderAuth.CurrentUser != null;
            if (!signedIn && user != null)
            {
                GameManager.instance.con.Log("Signed out " + user.UserId);
            }
            user = senderAuth.CurrentUser;
            userByAuth[senderAuth.App.Name] = user;
            if (signedIn)
            {
                GameManager.instance.con.Log("AuthStateChanged Signed in " + user.UserId);
                DisplayDetailedUserInfo(user);
            }
        }
    }
    // Track ID token changes.
    void IdTokenChanged(object sender, System.EventArgs eventArgs)
    {
        Firebase.Auth.FirebaseAuth senderAuth = sender as Firebase.Auth.FirebaseAuth;
        if (senderAuth == auth && senderAuth.CurrentUser != null && !fetchingToken)
            senderAuth.CurrentUser.TokenAsync(false).ContinueWithOnMainThread(task => GameManager.instance.con.Log(String.Format("Token[0:8] = {0}", task.Result.Substring(0, 8))));
    }

    protected void DisplayDetailedUserInfo(Firebase.Auth.FirebaseUser user)
    {
        GameManager.instance.con.Log("User ID     : " + user.UserId);
        GameManager.instance.con.Log("Display Name: " + user.DisplayName);
        GameManager.instance.con.Log("Is Anonymous: " + user.IsAnonymous);
    }
    protected bool LogTaskCompletion(Task task, string operation)
    {
        bool complete = false;
        if (task.IsCanceled) GameManager.instance.con.Log(operation + " canceled.");
        else if (task.IsFaulted)
        {
            GameManager.instance.con.Log(operation + " encounted an error.");
            foreach (Exception exception in task.Exception.Flatten().InnerExceptions)
            {
                string authErrorCode = "";
                Firebase.FirebaseException firebaseEx = exception as Firebase.FirebaseException;
                if (firebaseEx != null)
                {
                    authErrorCode = String.Format("AuthError.{0}: ", ((Firebase.Auth.AuthError)firebaseEx.ErrorCode).ToString());
                }
                GameManager.instance.con.Log(authErrorCode + exception.ToString());
            }
        }
        else if (task.IsCompleted)
        {
            GameManager.instance.con.Log(operation + " completed");
            complete = true;
        }
        return complete;
    }
    bool HandleSignInWithUser(Task<Firebase.Auth.FirebaseUser> task)
    {
        if (LogTaskCompletion(task, "Sign-in"))
        {
            GameManager.instance.con.Log(String.Format("{0} signed in", task.Result.DisplayName));
            return true;
        }
        return false;
    }

    void AddData(string dataID, object dataType)
    {
        if (data == null) data = new Dictionary<string, object>();
        if (data.ContainsKey(dataID)) data[dataID] = dataType;
        else data.Add(dataID, dataType);
    }
    void RemoveData(string dataID)
    {
        if (data == null) data = new Dictionary<string, object>();
        if (data.ContainsKey(dataID)) data.Remove(dataID);
    }

    public void SignUp(string username, string password)
    {
        GameManager.instance.con.Log("Signing up: " + username + "...");
        Dictionary<string, object> variables = new Dictionary<string, object>
            {
                { "Password", password },
                { "TimesLoggedIn", 1 }
            };
        AddData("Sign-Up", null);
        StartCoroutine(SetSpecificUserVariableAsync(database.Collection("Data").Document(username), variables, null, "Sign-Up"));
        OnFireStoreResult.AddListener(SignUpComplete);
    }
    public void SignUpComplete()
    {
        if (data["Sign-Up"] == null) return;
        GameManager.instance.con.Log("Sign-up successful!");
        GameManager.instance.DisplayName = (string)data["Username"];
        GameManager.instance.con.Log("Welcome " + GameManager.instance.DisplayName + "!");

        OnSigninSuccessful.Invoke();
    }
    public void SignIn(string username, string password)
    {
        GameManager.instance.con.Log("Signing In: " + username + "...");

        AddData("Sign-In", null);
        AddData("Username", username);
        AddData("Unverified", password);
        OnFireStoreResult.AddListener(VerifyExistingUser);
        StartCoroutine(GetSpecificUserDocumentAsync(database.Collection("Data").Document(username), "Sign-In"));
    }
    public void SignOut()
    {
        StartCoroutine(UpdateLogoutTimestampAsync());
    }
    public void SignOutComplete()
    {
        auth.SignOut();
        SceneManager.LoadScene("Title Screen");
    }

    public void VerifyExistingUser()
    {
        if (data["Sign-In"] == null) return;
        GameManager.instance.con.Log("Checking existing user...");
        OnFireStoreResult.RemoveListener(VerifyExistingUser);
        if ((bool)data["Sign-In"] == true)
        {
            GameManager.instance.con.Log("Existing user " + (string)data["Username"] + " found!");
            AddData("Password", null);
            List<string> variables = new List<string> { "Password" };
            StartCoroutine(GetSpecificUserVariableAsync(database.Collection("Data").Document((string)data["Username"]), variables));
            OnFireStoreResult.AddListener(VerifyCorrectPassword);
        }
        else
        {
            GameManager.instance.con.Log("No existing user " + (string)data["Username"] + " found!");
            SignUp((string)data["Username"], (string)data["Unverified"]);
        }
    }
    public void VerifyCorrectPassword()
    {
        if (data["Password"] == null) return;
        GameManager.instance.con.Log("Verifying password...");
        OnFireStoreResult.RemoveListener(VerifyCorrectPassword);

        string unverified = (string)data["Unverified"] ?? "";
        string pass = (string)data["Password"] ?? "";

        if (unverified == pass)
        {
            GameManager.instance.con.Log("Sign-in successful!");
            GameManager.instance.DisplayName = (string)data["Username"];
            GameManager.instance.con.Log("Welcome back " + GameManager.instance.DisplayName + "!");

            OnSigninSuccessful.Invoke();
        }
        else
        {
            GameManager.instance.con.Log((string)data["Unverified"] + " : " + (string)data["Password"]);
            OnSigninFailed.Invoke();
        }
    }

    public void EnterGame()
    {
        StartCoroutine(UpdateLoginTimestampAsync());
    }
    
    public void LogData()
    {
        Dictionary<string, object> documentDictionary = new Dictionary<string, object>
        {
            { "DailyData", GameManager.instance.Data.daily }
        };

        string year = GameManager.instance.Data.date.Year.ToString();
        DocumentReference docRef = database.Collection("Data").Document(GameManager.instance.DisplayName).Collection("Year").Document(year);
        StartCoroutine(SetSpecificUserVariableAsync(docRef, documentDictionary, SetOptions.MergeAll));
    }

    // Used to create a new Username
    public Task UpdateUserProfileAsync(string newDisplayName = null)
    {
        if (auth.CurrentUser == null)
        {
            GameManager.instance.con.Log("Not signed in, unable to update user profile");
            return Task.FromResult(0);
        }
        //displayName = newDisplayName ?? displayName;
        GameManager.instance.con.Log("Updating user profile");
        return auth.CurrentUser.UpdateUserProfileAsync(new Firebase.Auth.UserProfile
        {
            DisplayName = newDisplayName,
            PhotoUrl = auth.CurrentUser.PhotoUrl,
        });
    }

    // Used to check if a document exists
    public IEnumerator GetSpecificUserDocumentAsync(DocumentReference docRef, string onComplete)
    {
        GameManager.instance.con.Log(String.Format("Getting document {0} from database!", onComplete));
        Task<DocumentSnapshot> task = docRef.GetSnapshotAsync();
        yield return new WaitForTaskCompletion(task);
        if (!(task.IsFaulted || task.IsCanceled))
        {
            if (task.Result.Exists)
            {
                data[onComplete] = true;
                GameManager.instance.con.Log(String.Format("Document data for {0} document:", task.Result.Id));
            }
            else
            {
                data[onComplete] = false;
                GameManager.instance.con.Log(String.Format("Document does not exist!"));
            }
        }
        else
        {
            data[onComplete] = false;
            GameManager.instance.con.Log(String.Format("Document does not exist!"));
        }
        OnFireStoreResult.Invoke();
    }

    // Used to get an existing variable's value
    public IEnumerator GetSpecificUserVariableAsync(DocumentReference docRef, List<string> variables)
    {
        GameManager.instance.con.Log(String.Format("Getting variables from database!"));
        var task = docRef.GetSnapshotAsync();
        yield return new WaitForTaskCompletion(task);
        if (!(task.IsFaulted || task.IsCanceled))
        {
            DocumentSnapshot snapshot = task.Result;
            if (snapshot.Exists)
            {
                IDictionary<string, object> documentDictionary = snapshot.ToDictionary();
                foreach (string variable in variables)
                {
                    if (documentDictionary.ContainsKey(variable))
                    {
                        data[variable] = documentDictionary[variable];
                        GameManager.instance.con.Log(String.Format("{0}: {1}", variable, documentDictionary[variable]));
                    }
                    else GameManager.instance.con.Log(String.Format("Variable {0} does not exist!", variable));
                }
            }
            else GameManager.instance.con.Log(String.Format("Document does not exist!"));
        }
        else GameManager.instance.con.Log(String.Format("Document does not exist!"));
        OnFireStoreResult.Invoke();
    }

    // Used to add a new variable to a new / existing document
    public IEnumerator SetSpecificUserVariableAsync(DocumentReference docRef, Dictionary<string, object> variables, SetOptions options = null, string onComplete = "")
    {
        GameManager.instance.con.Log(String.Format("Setting variables into database!"));
        Task task;
        if (options == null) task = docRef.SetAsync(variables);
        else task = docRef.UpdateAsync(variables);
        yield return new WaitForTaskCompletion(task);
        if (!(task.IsFaulted || task.IsCanceled)) GameManager.instance.con.Log(String.Format("Set variables into database!"));
        else GameManager.instance.con.Log(String.Format("Failed to set variables into database!"));
        if (onComplete != "") data[onComplete] = true;
        OnFireStoreResult.Invoke();
    }
    
    // Used to update an existing variable / set fo variables in an existing document
    // valueChange is true if value is used to change the variable's value and not overwrite it
    public IEnumerator UpdateSpecificUserVariableAsync(DocumentReference docRef, string variable, object value, bool valueChange)
    {
        GameManager.instance.con.Log(String.Format("Updating variable {0} into database!", variable));
        Task<DocumentSnapshot> task = docRef.GetSnapshotAsync();
        yield return new WaitForTaskCompletion(task);
        if (!(task.IsFaulted || task.IsCanceled))
        {
            DocumentSnapshot snapshot = task.Result;
            if (snapshot.Exists)
            {
                Dictionary<string, object> documentDictionary = snapshot.ToDictionary();
                if (documentDictionary.ContainsKey(variable)) GameManager.instance.con.Log(String.Format("{0}: {1}", variable, documentDictionary[variable]));
                else GameManager.instance.con.Log(String.Format("Variable {0} does not exist!", variable));

                if (valueChange)
                {
                    GameManager.instance.con.Log(String.Format("Before Value: {0}, Data Type: {1}", value, value.GetType()));

                    switch (Type.GetTypeCode(value.GetType()))
                    {
                        case TypeCode.Int16:
                        case TypeCode.Int32:
                        case TypeCode.Int64:
                            if (Type.GetTypeCode(documentDictionary[variable].GetType()) != TypeCode.Int16 &&
                                Type.GetTypeCode(documentDictionary[variable].GetType()) != TypeCode.Int32 &&
                                Type.GetTypeCode(documentDictionary[variable].GetType()) != TypeCode.Int64)
                                GameManager.instance.con.LogError(String.Format("Variable {0} ({2}) has a different type than Variable {1} ({3})!", variable, value, documentDictionary[variable].GetType(), value.GetType()));
                            if (Type.GetTypeCode(value.GetType()) != TypeCode.Int16 &&
                                Type.GetTypeCode(value.GetType()) != TypeCode.Int32 &&
                                Type.GetTypeCode(value.GetType()) != TypeCode.Int64)
                                GameManager.instance.con.LogError(String.Format("Variable {0} ({2}) has a different type than Variable {1} ({3})!", variable, value, documentDictionary[variable].GetType(), value.GetType()));
                            value = snapshot.GetValue<int>(variable) + (int)value;
                            break;
                        case TypeCode.Decimal:
                        case TypeCode.String:
                            value = snapshot.GetValue<string>(variable) + (string)value;
                            break;
                    }
                }
                GameManager.instance.con.Log(String.Format("After Value: {0}, Data Type: {1}", value, value.GetType()));
                Dictionary<string, object> updates = new Dictionary<string, object> { { variable, value } };

                Task updateTask = docRef.UpdateAsync(updates);
                if (!(updateTask.IsFaulted || updateTask.IsCanceled)) GameManager.instance.con.Log(String.Format("Updated variable {0} into database!", variable));
                else GameManager.instance.con.Log(String.Format("Failed to update variable {0} into database!", variable));
            }
            else GameManager.instance.con.Log(String.Format("Document does not exist!"));
        }
        else GameManager.instance.con.Log(String.Format("Document does not exist!"));
    }
    
    // Called whenever a user logins to create a new document for the date they logged in and to increment their TimesLoggedIn counter
    public IEnumerator UpdateLoginTimestampAsync()
    {
        GameManager.instance.con.Log("Updating login timestamp");

        DocumentReference docRef = database.Collection("Data").Document(GameManager.instance.DisplayName);
        Task<DocumentSnapshot> task = docRef.GetSnapshotAsync();
        yield return new WaitForTaskCompletion(task);
        if (!(task.IsFaulted || task.IsCanceled))
        {
            if (task.Result.Exists)
            {
                GameManager.instance.con.Log(String.Format("Document data for document {0}:", task.Result.Id));
                IDictionary<string, object> documentDictionary = task.Result.ToDictionary();
                if (documentDictionary.ContainsKey("AllTimeData"))
                {
                    GameManager.instance.Data.allTime = JsonUtility.FromJson<AllTime>((string)documentDictionary["AllTimeData"]);
                    GameManager.instance.con.Log(String.Format("Variable AllTimeData found!"));
                }
                else GameManager.instance.con.Log(String.Format("Variable AllTimeData does not exist!"));
            }
            else GameManager.instance.con.Log(String.Format("Variable AllTimeData does not exist!"));
        }
        StartCoroutine(UpdateSpecificUserVariableAsync(database.Collection("Data").Document(GameManager.instance.DisplayName), "TimesLoggedIn", 1, true));
        
        string year = GameManager.instance.Data.date.Year.ToString();
        docRef = database.Collection("Data").Document(GameManager.instance.DisplayName).Collection("Year").Document(year);
        task = docRef.GetSnapshotAsync();
        yield return new WaitForTaskCompletion(task);
        if (!(task.IsFaulted || task.IsCanceled))
        {
            if (task.Result.Exists)
            {
                GameManager.instance.con.Log(String.Format("Document data for document {0}:", task.Result.Id));
                IDictionary<string, object> documentDictionary = task.Result.ToDictionary();
                if (documentDictionary.ContainsKey("YearlyData"))
                {
                    GameManager.instance.Data.yearly = JsonUtility.FromJson<Yearly>((string)documentDictionary["YearlyData"]);
                    GameManager.instance.con.Log(String.Format("Variable YearlyData found!"));
                }
                else GameManager.instance.con.Log(String.Format("Variable YearlyData does not exist!"));
                StartCoroutine(UpdateSpecificUserVariableAsync(docRef, "TimesLoggedInThisYear", 1, true));
            }
            else
            {
                GameManager.instance.con.Log(String.Format("Document {0} does not exist!", year));
                Dictionary<string, object> variables = new Dictionary<string, object> { { "TimesLoggedInThisYear", 1 } };
                StartCoroutine(SetSpecificUserVariableAsync(docRef, variables));
            }
        }
        else
        {
            GameManager.instance.con.Log(String.Format("Document {0} does not exist!", year));
            Dictionary<string, object> variables = new Dictionary<string, object> { { "TimesLoggedInThisYear", 1 } };
            StartCoroutine(SetSpecificUserVariableAsync(docRef, variables));
        }

        string month = GameManager.instance.Data.date.Month.ToString();
        docRef = database.Collection("Data").Document(GameManager.instance.DisplayName).Collection("Year").Document(year).Collection("Month").Document(month);
        task = docRef.GetSnapshotAsync();
        yield return new WaitForTaskCompletion(task);
        if (!(task.IsFaulted || task.IsCanceled))
        {
            if (task.Result.Exists)
            {
                GameManager.instance.con.Log(String.Format("Document data for document {0}:", task.Result.Id));
                IDictionary<string, object> documentDictionary = task.Result.ToDictionary();
                if (documentDictionary.ContainsKey("MonthlyData"))
                {
                    GameManager.instance.Data.monthly = JsonUtility.FromJson<Monthly>((string)documentDictionary["MonthlyData"]);
                    GameManager.instance.con.Log(String.Format("Variable MonthlyData found!"));
                }
                else GameManager.instance.con.Log(String.Format("Variable MonthlyData does not exist!"));
                StartCoroutine(UpdateSpecificUserVariableAsync(docRef, "TimesLoggedInThisMonth", 1, true));
            }
            else
            {
                GameManager.instance.con.Log(String.Format("Document {0} does not exist!", month));
                Dictionary<string, object> variables = new Dictionary<string, object> { { "TimesLoggedInThisMonth", 1 } };
                StartCoroutine(SetSpecificUserVariableAsync(docRef, variables));
            }
        }
        else
        {
            GameManager.instance.con.Log(String.Format("Document {0} does not exist!", month));
            Dictionary<string, object> variables = new Dictionary<string, object> { { "TimesLoggedInThisMonth", 1 } };
            StartCoroutine(SetSpecificUserVariableAsync(docRef, variables));
        }

        string day = GameManager.instance.Data.date.Day.ToString();
        docRef = database.Collection("Data").Document(GameManager.instance.DisplayName).Collection("Year").Document(year).Collection("Month").Document(month).Collection("Day").Document(day);
        task = docRef.GetSnapshotAsync();
        yield return new WaitForTaskCompletion(task);
        if (!(task.IsFaulted || task.IsCanceled))
        {
            if (task.Result.Exists)
            {
                GameManager.instance.con.Log(String.Format("Document data for document {0}:", task.Result.Id));
                IDictionary<string, object> documentDictionary = task.Result.ToDictionary();
                if (documentDictionary.ContainsKey("DailyData"))
                {
                    GameManager.instance.Data.daily = JsonUtility.FromJson<Daily>((string)documentDictionary["DailyData"]);
                    GameManager.instance.con.Log(String.Format("Variable DailyData found!"));
                }
                else GameManager.instance.con.Log(String.Format("Variable DailyData does not exist!"));
                StartCoroutine(UpdateSpecificUserVariableAsync(docRef, "TimesLoggedInToday", 1, true));
            }
            else
            {
                GameManager.instance.con.Log(String.Format("Document {0} does not exist!", day));
                Dictionary<string, object> variables = new Dictionary<string, object> { { "TimesLoggedInToday", 1 } };
                StartCoroutine(SetSpecificUserVariableAsync(docRef, variables));
            }
        }
        else
        {
            GameManager.instance.con.Log(String.Format("Document {0} does not exist!", day));
            Dictionary<string, object> variables = new Dictionary<string, object> { { "TimesLoggedInToday", 1 } };
            StartCoroutine(SetSpecificUserVariableAsync(docRef, variables));
        }

        GameManager.instance.con.Log("GameManager.instance.Data.date = " + GameManager.instance.Data.date);
    }
    
    // Called whenever a user logins to create a new document for the date they logged in and to increment their TimesLoggedIn counter
    public IEnumerator UpdateLogoutTimestampAsync(bool exit = false)
    {
        GameManager.instance.con.Log("Updating logout timestamp");

        if (GameManager.instance.DisplayName == "") yield break;

        GameManager.instance.Data.LogData();
        
        DocumentReference docRef = database.Collection("Data").Document(GameManager.instance.DisplayName);
        Task<DocumentSnapshot> task = docRef.GetSnapshotAsync();
        yield return new WaitForTaskCompletion(task);
        if (!(task.IsFaulted || task.IsCanceled))
        {
            if (task.Result.Exists)
            {
                GameManager.instance.con.Log(String.Format("Document data for document {0}:", task.Result.Id));
                StartCoroutine(UpdateSpecificUserVariableAsync(docRef, "AllTimeData", JsonUtility.ToJson(GameManager.instance.Data.allTime), false));
            }
            else
            {
                GameManager.instance.con.Log(String.Format("Document {0} does not exist!", GameManager.instance.DisplayName));
                Dictionary<string, object> variables = new Dictionary<string, object> { { "AllTimeData", JsonUtility.ToJson(GameManager.instance.Data.allTime) } };
                StartCoroutine(SetSpecificUserVariableAsync(docRef, variables, SetOptions.MergeAll));
            }
        }
        else
        {
            GameManager.instance.con.Log(String.Format("Document {0} does not exist!", GameManager.instance.DisplayName));
            Dictionary<string, object> variables = new Dictionary<string, object> { { "AllTimeData", JsonUtility.ToJson(GameManager.instance.Data.allTime) } };
            StartCoroutine(SetSpecificUserVariableAsync(docRef, variables, SetOptions.MergeAll));
        }

        string year = GameManager.instance.Data.date.Year.ToString();
        docRef = database.Collection("Data").Document(GameManager.instance.DisplayName).Collection("Year").Document(year);
        task = docRef.GetSnapshotAsync();
        yield return new WaitForTaskCompletion(task);
        if (!(task.IsFaulted || task.IsCanceled))
        {
            if (task.Result.Exists)
            {
                GameManager.instance.con.Log(String.Format("Document data for document {0}:", task.Result.Id));
                StartCoroutine(UpdateSpecificUserVariableAsync(docRef, "YearlyData", JsonUtility.ToJson(GameManager.instance.Data.yearly), false));
            }
            else
            {
                GameManager.instance.con.Log(String.Format("Document {0} does not exist!", GameManager.instance.DisplayName));
                Dictionary<string, object> variables = new Dictionary<string, object> { { "YearlyData", JsonUtility.ToJson(GameManager.instance.Data.yearly) } };
                StartCoroutine(SetSpecificUserVariableAsync(docRef, variables, SetOptions.MergeAll));
            }
        }
        else
        {
            GameManager.instance.con.Log(String.Format("Document {0} does not exist!", GameManager.instance.DisplayName));
            Dictionary<string, object> variables = new Dictionary<string, object> { { "YearlyData", JsonUtility.ToJson(GameManager.instance.Data.yearly) } };
            StartCoroutine(SetSpecificUserVariableAsync(docRef, variables, SetOptions.MergeAll));
        }

        string month = GameManager.instance.Data.date.Month.ToString();
        docRef = database.Collection("Data").Document(GameManager.instance.DisplayName).Collection("Year").Document(year).Collection("Month").Document(month);
        task = docRef.GetSnapshotAsync();
        yield return new WaitForTaskCompletion(task);
        if (!(task.IsFaulted || task.IsCanceled))
        {
            if (task.Result.Exists)
            {
                GameManager.instance.con.Log(String.Format("Document data for document {0}:", task.Result.Id));
                StartCoroutine(UpdateSpecificUserVariableAsync(docRef, "MonthlyData", JsonUtility.ToJson(GameManager.instance.Data.monthly), false));
            }
            else
            {
                GameManager.instance.con.Log(String.Format("Document {0} does not exist!", GameManager.instance.DisplayName));
                Dictionary<string, object> variables = new Dictionary<string, object> { { "MonthlyData", JsonUtility.ToJson(GameManager.instance.Data.monthly) } };
                StartCoroutine(SetSpecificUserVariableAsync(docRef, variables, SetOptions.MergeAll));
            }
        }
        else
        {
            GameManager.instance.con.Log(String.Format("Document {0} does not exist!", GameManager.instance.DisplayName));
            Dictionary<string, object> variables = new Dictionary<string, object> { { "MonthlyData", JsonUtility.ToJson(GameManager.instance.Data.monthly) } };
            StartCoroutine(SetSpecificUserVariableAsync(docRef, variables, SetOptions.MergeAll));
        }

        string day = GameManager.instance.Data.date.Day.ToString();
        docRef = database.Collection("Data").Document(GameManager.instance.DisplayName).Collection("Year").Document(year).Collection("Month").Document(month).Collection("Day").Document(day);
        task = docRef.GetSnapshotAsync();
        yield return new WaitForTaskCompletion(task);
        if (!(task.IsFaulted || task.IsCanceled))
        {
            if (task.Result.Exists)
            {
                GameManager.instance.con.Log(String.Format("Document data for document {0}:", task.Result.Id));
                StartCoroutine(UpdateSpecificUserVariableAsync(docRef, "DailyData", JsonUtility.ToJson(GameManager.instance.Data.daily), false));
            }
            else
            {
                GameManager.instance.con.Log(String.Format("Document {0} does not exist!", GameManager.instance.DisplayName));
                Dictionary<string, object> variables = new Dictionary<string, object> { { "DailyData", JsonUtility.ToJson(GameManager.instance.Data.daily) } };
                StartCoroutine(SetSpecificUserVariableAsync(docRef, variables, SetOptions.MergeAll));
            }
        }
        else
        {
            GameManager.instance.con.Log(String.Format("Document {0} does not exist!", GameManager.instance.DisplayName));
            Dictionary<string, object> variables = new Dictionary<string, object> { { "DailyData", JsonUtility.ToJson(GameManager.instance.Data.daily) } };
            StartCoroutine(SetSpecificUserVariableAsync(docRef, variables, SetOptions.MergeAll));
        }

        if (exit)
        {
            database = null;
            if (auth != null)
            {
                auth.StateChanged -= AuthStateChanged;
                auth.IdTokenChanged -= IdTokenChanged;
                auth.SignOut();
                auth = null;
            }
            app = null;
            if (instance == this) instance = null;
        }
        else SignOutComplete();
    }

    public Task SigninAnonymouslyAsync()
    {
        GameManager.instance.con.Log("Attempting to sign anonymously...");
        string newDisplayName = "temp";
        return auth.SignInAnonymouslyAsync().ContinueWithOnMainThread((task) =>
        {
            if (HandleSignInWithUser(task))
            {
                var user = task.Result;
                DisplayDetailedUserInfo(user);
                return UpdateUserProfileAsync(newDisplayName: newDisplayName);
            }
            return task;
        });
    }

    public Task SigninWithTokenAsync()
    {
        GameManager.instance.con.Log("Attempting to sign-in with token...");
        string token = "";
        return auth.SignInWithCustomTokenAsync(token).ContinueWithOnMainThread(HandleSignInWithUser);
    }

    //private void OnApplicationPause(bool pause)
    //{
    //    StartCoroutine(UpdateLogoutTimestampAsync(true));
    //}

    //private void OnApplicationQuit()
    //{
    //    GameManager.instance.con.Log("Updating logout timestamp");

    //    if (GameManager.instance && GameManager.instance.DisplayName == "") return;

    //    DocumentReference docRef = database.Collection("Data").Document(GameManager.instance.DisplayName);
    //    Dictionary<string, object> variables = new Dictionary<string, object>
    //        {
    //            { "AllTimeData", JsonUtility.ToJson(GameManager.instance.Data.allTime) }
    //        };
    //    docRef.SetAsync(variables, SetOptions.MergeAll).ContinueWith(task => {
    //        GameManager.instance.con.Log("Added data to the server.");
    //    });

    //    string year = GameManager.instance.Data.date.Year.ToString();
    //    docRef = database.Collection("Data").Document(GameManager.instance.DisplayName).Collection("Year").Document(year);
    //    variables = new Dictionary<string, object>
    //        {
    //            { "YearlyData", JsonUtility.ToJson(GameManager.instance.Data.yearly) }
    //        };
    //    docRef.SetAsync(variables, SetOptions.MergeAll).ContinueWith(task => {
    //        GameManager.instance.con.Log("Added data to the server.");
    //    });

    //    string month = GameManager.instance.Data.date.Month.ToString();
    //    docRef = database.Collection("Data").Document(GameManager.instance.DisplayName).Collection("Year").Document(year).Collection("Month").Document(month);
    //    variables = new Dictionary<string, object>
    //        {
    //            { "MonthlyData", JsonUtility.ToJson(GameManager.instance.Data.monthly) }
    //        };
    //    docRef.SetAsync(variables, SetOptions.MergeAll).ContinueWith(task => {
    //        GameManager.instance.con.Log("Added data to the server.");
    //    });

    //    string day = GameManager.instance.Data.date.Day.ToString();
    //    docRef = database.Collection("Data").Document(GameManager.instance.DisplayName).Collection("Year").Document(year).Collection("Month").Document(month).Collection("Day").Document(day);
    //    variables = new Dictionary<string, object>
    //        {
    //            { "DailyData", JsonUtility.ToJson(GameManager.instance.Data.daily) }
    //        };
    //    docRef.SetAsync(variables, SetOptions.MergeAll).ContinueWith(task => {
    //        GameManager.instance.con.Log("Added data to the server.");
    //    });

    //    database = null;
    //    if (auth != null)
    //    {
    //        auth.StateChanged -= AuthStateChanged;
    //        auth.IdTokenChanged -= IdTokenChanged;
    //        auth.SignOut();
    //        auth = null;
    //    }
    //    app = null;
    //    if (instance == this) instance = null;
    //}
}
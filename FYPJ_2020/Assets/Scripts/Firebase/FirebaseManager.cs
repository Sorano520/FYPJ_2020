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
    public Dictionary<string, object> Data
    {
        get { return data; }
        set { data = value; }
    }
    #endregion

    public UnityEvent OnFirebaseInit = new UnityEvent();
    public UnityEvent OnFireStoreResult = new UnityEvent();
    public UnityEvent OnSigninSuccessful = new UnityEvent();
    public UnityEvent OnSigninFailed = new UnityEvent();

    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else Debug.LogWarning($"An instance of {nameof(FirebaseManager)} already exists!");
    }

    void Start()
    {
        Debug.Log("FirebaseManager.Start()");
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            var dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available) InitializeFirebase();
            else Debug.LogError("Could not resolve all Firebase dependencies: " + dependencyStatus);
        });

        Debug.Log("FirebaseManager.Start() - Finished");
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
                    if (task.IsFaulted) Debug.Log(task.Exception.ToString());
                    return false;
                }
                return true;
            }
        }
    }

    protected void InitializeFirebase()
    {
        Debug.Log("Setting up Firebase Auth");
        app = FirebaseApp.DefaultInstance;
        auth = FirebaseAuth.DefaultInstance;
        database = FirebaseFirestore.DefaultInstance;
        auth.StateChanged += AuthStateChanged;
        auth.IdTokenChanged += IdTokenChanged;
        AuthStateChanged(this, null);
        SigninAnonymouslyAsync();
    }

    async void Func()
    {
        Debug.Log("141");
        List<Task> tasks = new List<Task>
        {
            GetDatabaseNode("path_to_node", SampleCallback)
        };
        //Add more tasks here...

        Debug.Log("148");
        int timeout = 10000;
        Task timeoutTask = Task.Delay(timeout);

        Debug.Log("152");
        if (await Task.WhenAny(Task.WhenAll(tasks), timeoutTask) == timeoutTask)
        {
            // timeout logic
            Debug.Log("TIMED OUT");

        }
        else
        {
            // task completed within timeout 
            Debug.Log("162");
        }
    }
    public async Task GetDatabaseNode(string path, System.Action<DocumentSnapshot> callback)
    {
        Debug.Log("167");
        await database.Collection("Data").Document("yytt").GetSnapshotAsync().ContinueWith(task =>
        {
            Debug.Log("170");
            if (task.IsFaulted || task.IsCanceled)
            {
                Debug.Log("173");
                Debug.Log("Inner task was " + (task.IsFaulted ? "faulted." : "cancelled."));
                return;
            }

            Debug.Log("177");
            callback?.Invoke(task.Result);
        });

        // anything that you put here will be run once the awaiting above has finished
    }

    private void SampleCallback(DocumentSnapshot snapshot)
    {
        Debug.Log("186");
        foreach (KeyValuePair<string, object> childSnapshot in snapshot.ToDictionary())
        {
            Debug.Log("190");
            Debug.LogFormat("node contains: {0}", childSnapshot.Key);
        }
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
                Debug.Log("Signed out " + user.UserId);
            }
            user = senderAuth.CurrentUser;
            userByAuth[senderAuth.App.Name] = user;
            if (signedIn)
            {
                Debug.Log("AuthStateChanged Signed in " + user.UserId);
                DisplayDetailedUserInfo(user);
            }
        }
    }
    // Track ID token changes.
    void IdTokenChanged(object sender, System.EventArgs eventArgs)
    {
        Firebase.Auth.FirebaseAuth senderAuth = sender as Firebase.Auth.FirebaseAuth;
        if (senderAuth == auth && senderAuth.CurrentUser != null && !fetchingToken)
            senderAuth.CurrentUser.TokenAsync(false).ContinueWithOnMainThread(task => Debug.Log(String.Format("Token[0:8] = {0}", task.Result.Substring(0, 8))));
    }

    protected void DisplayDetailedUserInfo(Firebase.Auth.FirebaseUser user)
    {
        Debug.Log("User ID     : " + user.UserId);
        Debug.Log("Display Name: " + user.DisplayName);
        Debug.Log("Is Anonymous: " + user.IsAnonymous);
    }
    protected bool LogTaskCompletion(Task task, string operation)
    {
        bool complete = false;
        if (task.IsCanceled) Debug.Log(operation + " canceled.");
        else if (task.IsFaulted)
        {
            Debug.Log(operation + " encounted an error.");
            foreach (Exception exception in task.Exception.Flatten().InnerExceptions)
            {
                string authErrorCode = "";
                Firebase.FirebaseException firebaseEx = exception as Firebase.FirebaseException;
                if (firebaseEx != null)
                {
                    authErrorCode = String.Format("AuthError.{0}: ", ((Firebase.Auth.AuthError)firebaseEx.ErrorCode).ToString());
                }
                Debug.Log(authErrorCode + exception.ToString());
            }
        }
        else if (task.IsCompleted)
        {
            Debug.Log(operation + " completed");
            complete = true;
        }
        return complete;
    }
    bool HandleSignInWithUser(Task<Firebase.Auth.FirebaseUser> task)
    {
        if (LogTaskCompletion(task, "Sign-in"))
        {
            Debug.Log(String.Format("{0} signed in", task.Result.DisplayName));
            return true;
        }
        return false;
    }

    public void AddData(string dataID, object dataType)
    {
        if (data == null) data = new Dictionary<string, object>();
        if (data.ContainsKey(dataID)) data[dataID] = dataType;
        else data.Add(dataID, dataType);
    }
    public void RemoveData(string dataID)
    {
        if (data == null) data = new Dictionary<string, object>();
        if (data.ContainsKey(dataID)) data.Remove(dataID);
    }

    public void CheckUsernameExists(string username)
    {
        Debug.Log("Checking " + username + " exists...");

        AddData("Username", username);
        Func();
        GetSpecificUserDocumentAsync(database.Collection("Data").Document(username), "Sign-In");
    }

    public void SignUp(string username, string password)
    {
        Debug.Log("Signing up: " + username + "...");
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
        Debug.Log("Sign-up successful!");
        OnFireStoreResult.RemoveListener(SignUpComplete);
        GameManager.instance.DisplayName = (string)data["Username"];
        RemoveData("Username");
        Debug.Log("Welcome " + GameManager.instance.DisplayName + "!");

        OnSigninSuccessful.Invoke();
    }
    public void SignIn(string username, string password)
    {
        Debug.Log("Signing In: " + username + "...");

        AddData("Sign-In", null);
        AddData("Username", username);
        AddData("Unverified", password);
        OnFireStoreResult.AddListener(VerifyExistingUser);
        StartCoroutine(GetSpecificUserDocument(database.Collection("Data").Document(username), "Sign-In"));
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
        Debug.Log("Checking existing user...");
        OnFireStoreResult.RemoveListener(VerifyExistingUser);
        if ((bool)data["Sign-In"] == true)
        {
            Debug.Log("Existing user " + (string)data["Username"] + " found!");
            AddData("Password", null);
            List<string> variables = new List<string> { "Password" };
            StartCoroutine(GetSpecificUserVariableAsync(database.Collection("Data").Document((string)data["Username"]), variables));
            OnFireStoreResult.AddListener(VerifyCorrectPassword);
        }
        else
        {
            Debug.Log("No existing user " + (string)data["Username"] + " found!");
            SignUp((string)data["Username"], (string)data["Unverified"]);
        }
    }
    public void VerifyCorrectPassword()
    {
        if (data["Password"] == null) return;
        Debug.Log("Verifying password...");
        OnFireStoreResult.RemoveListener(VerifyCorrectPassword);

        string unverified = (string)data["Unverified"] ?? "";
        string pass = (string)data["Password"] ?? "";

        if (unverified == pass)
        {
            Debug.Log("Sign-in successful!");
            GameManager.instance.DisplayName = (string)data["Username"];
            Debug.Log("Welcome back " + GameManager.instance.DisplayName + "!");

            OnSigninSuccessful.Invoke();
        }
        else
        {
            Debug.Log((string)data["Unverified"] + " : " + (string)data["Password"]);
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
            Debug.Log("Not signed in, unable to update user profile");
            return Task.FromResult(0);
        }
        //displayName = newDisplayName ?? displayName;
        Debug.Log("Updating user profile");
        return auth.CurrentUser.UpdateUserProfileAsync(new Firebase.Auth.UserProfile
        {
            DisplayName = newDisplayName,
            PhotoUrl = auth.CurrentUser.PhotoUrl,
        });
    }

    // Used to check if a document exists
    public async void GetSpecificUserDocumentAsync(DocumentReference docRef, string onComplete)
    {
        Debug.Log(String.Format("Getting document {0} from database!", onComplete));
        DocumentSnapshot task = await docRef.GetSnapshotAsync();
        if (task.Exists)
        {
            data[onComplete] = true;
            Debug.Log(String.Format("Document data for {0} document:", task.Id));
        }
        else
        {
            data[onComplete] = false;
            Debug.Log(String.Format("Document does not exist!"));
        }
        OnFireStoreResult.Invoke();
    }

    // Used to check if a document exists
    public IEnumerator GetSpecificUserDocument(DocumentReference docRef, string onComplete)
    {
        Debug.Log(String.Format("Getting document {0} from database!", onComplete));
        Task<DocumentSnapshot> task = docRef.GetSnapshotAsync();
        yield return new WaitForTaskCompletion(task);
        if (!(task.IsFaulted || task.IsCanceled))
        {
            if (task.Result.Exists)
            {
                data[onComplete] = true;
                Debug.Log(String.Format("Document data for {0} document:", task.Result.Id));
            }
            else
            {
                data[onComplete] = false;
                Debug.Log(String.Format("Document does not exist!"));
            }
        }
        else
        {
            data[onComplete] = false;
            Debug.Log(String.Format("Document does not exist!"));
        }
        OnFireStoreResult.Invoke();
    }

    // Used to get an existing variable's value
    public IEnumerator GetSpecificUserVariableAsync(DocumentReference docRef, List<string> variables)
    {
        Debug.Log(String.Format("Getting variables from database!"));
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
                        Debug.Log(String.Format("{0}: {1}", variable, documentDictionary[variable]));
                    }
                    else Debug.Log(String.Format("Variable {0} does not exist!", variable));
                }
            }
            else Debug.Log(String.Format("Document does not exist!"));
        }
        else Debug.Log(String.Format("Document does not exist!"));
        OnFireStoreResult.Invoke();
    }

    // Used to add a new variable to a new / existing document
    public IEnumerator SetSpecificUserVariableAsync(DocumentReference docRef, Dictionary<string, object> variables, SetOptions options = null, string onComplete = "")
    {
        Debug.Log(String.Format("Setting variables into database!"));
        Task task;
        if (options == null) task = docRef.SetAsync(variables);
        else task = docRef.UpdateAsync(variables);
        yield return new WaitForTaskCompletion(task);
        if (!(task.IsFaulted || task.IsCanceled)) Debug.Log(String.Format("Set variables into database!"));
        else Debug.Log(String.Format("Failed to set variables into database!"));
        if (onComplete != "") data[onComplete] = true;
        OnFireStoreResult.Invoke();
    }
    
    // Used to update an existing variable / set fo variables in an existing document
    // valueChange is true if value is used to change the variable's value and not overwrite it
    public IEnumerator UpdateSpecificUserVariableAsync(DocumentReference docRef, string variable, object value, bool valueChange)
    {
        Debug.Log(String.Format("Updating variable {0} into database!", variable));
        Task<DocumentSnapshot> task = docRef.GetSnapshotAsync();
        yield return new WaitForTaskCompletion(task);
        if (!(task.IsFaulted || task.IsCanceled))
        {
            DocumentSnapshot snapshot = task.Result;
            if (snapshot.Exists)
            {
                Dictionary<string, object> documentDictionary = snapshot.ToDictionary();
                if (documentDictionary.ContainsKey(variable)) Debug.Log(String.Format("{0}: {1}", variable, documentDictionary[variable]));
                else Debug.Log(String.Format("Variable {0} does not exist!", variable));

                if (valueChange)
                {
                    Debug.Log(String.Format("Before Value: {0}, Data Type: {1}", value, value.GetType()));

                    switch (Type.GetTypeCode(value.GetType()))
                    {
                        case TypeCode.Int16:
                        case TypeCode.Int32:
                        case TypeCode.Int64:
                            if (Type.GetTypeCode(documentDictionary[variable].GetType()) != TypeCode.Int16 &&
                                Type.GetTypeCode(documentDictionary[variable].GetType()) != TypeCode.Int32 &&
                                Type.GetTypeCode(documentDictionary[variable].GetType()) != TypeCode.Int64)
                                Debug.LogError(String.Format("Variable {0} ({2}) has a different type than Variable {1} ({3})!", variable, value, documentDictionary[variable].GetType(), value.GetType()));
                            if (Type.GetTypeCode(value.GetType()) != TypeCode.Int16 &&
                                Type.GetTypeCode(value.GetType()) != TypeCode.Int32 &&
                                Type.GetTypeCode(value.GetType()) != TypeCode.Int64)
                                Debug.LogError(String.Format("Variable {0} ({2}) has a different type than Variable {1} ({3})!", variable, value, documentDictionary[variable].GetType(), value.GetType()));
                            value = snapshot.GetValue<int>(variable) + (int)value;
                            break;
                        case TypeCode.Decimal:
                        case TypeCode.String:
                            value = snapshot.GetValue<string>(variable) + (string)value;
                            break;
                    }
                }
                Debug.Log(String.Format("After Value: {0}, Data Type: {1}", value, value.GetType()));
                Dictionary<string, object> updates = new Dictionary<string, object> { { variable, value } };

                Task updateTask = docRef.UpdateAsync(updates);
                if (!(updateTask.IsFaulted || updateTask.IsCanceled)) Debug.Log(String.Format("Updated variable {0} into database!", variable));
                else Debug.Log(String.Format("Failed to update variable {0} into database!", variable));
            }
            else Debug.Log(String.Format("Document does not exist!"));
        }
        else Debug.Log(String.Format("Document does not exist!"));
    }
    
    // Called whenever a user logins to create a new document for the date they logged in and to increment their TimesLoggedIn counter
    public IEnumerator UpdateLoginTimestampAsync()
    {
        Debug.Log("Updating login timestamp");

        DocumentReference docRef = database.Collection("Data").Document(GameManager.instance.DisplayName);
        Task<DocumentSnapshot> task = docRef.GetSnapshotAsync();
        yield return new WaitForTaskCompletion(task);
        if (!(task.IsFaulted || task.IsCanceled))
        {
            if (task.Result.Exists)
            {
                Debug.Log(String.Format("Document data for document {0}:", task.Result.Id));
                IDictionary<string, object> documentDictionary = task.Result.ToDictionary();
                if (documentDictionary.ContainsKey("AllTimeData"))
                {
                    GameManager.instance.Data.allTime = JsonUtility.FromJson<AllTime>((string)documentDictionary["AllTimeData"]) ?? new AllTime();
                    Debug.Log(String.Format("Variable AllTimeData found!"));
                }
                else Debug.Log(String.Format("Variable AllTimeData does not exist!"));
            }
            else Debug.Log(String.Format("Variable AllTimeData does not exist!"));
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
                Debug.Log(String.Format("Document data for document {0}:", task.Result.Id));
                IDictionary<string, object> documentDictionary = task.Result.ToDictionary();
                if (documentDictionary.ContainsKey("YearlyData"))
                {
                    GameManager.instance.Data.yearly = JsonUtility.FromJson<Yearly>((string)documentDictionary["YearlyData"]) ?? new Yearly();
                    Debug.Log(String.Format("Variable YearlyData found!"));
                }
                else Debug.Log(String.Format("Variable YearlyData does not exist!"));
                StartCoroutine(UpdateSpecificUserVariableAsync(docRef, "TimesLoggedInThisYear", 1, true));
            }
            else
            {
                Debug.Log(String.Format("Document {0} does not exist!", year));
                Dictionary<string, object> variables = new Dictionary<string, object> { { "TimesLoggedInThisYear", 1 } };
                StartCoroutine(SetSpecificUserVariableAsync(docRef, variables));
            }
        }
        else
        {
            Debug.Log(String.Format("Document {0} does not exist!", year));
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
                Debug.Log(String.Format("Document data for document {0}:", task.Result.Id));
                IDictionary<string, object> documentDictionary = task.Result.ToDictionary();
                if (documentDictionary.ContainsKey("MonthlyData"))
                {
                    GameManager.instance.Data.monthly = JsonUtility.FromJson<Monthly>((string)documentDictionary["MonthlyData"]) ?? new Monthly();
                    Debug.Log(String.Format("Variable MonthlyData found!"));
                }
                else Debug.Log(String.Format("Variable MonthlyData does not exist!"));
                StartCoroutine(UpdateSpecificUserVariableAsync(docRef, "TimesLoggedInThisMonth", 1, true));
            }
            else
            {
                Debug.Log(String.Format("Document {0} does not exist!", month));
                Dictionary<string, object> variables = new Dictionary<string, object> { { "TimesLoggedInThisMonth", 1 } };
                StartCoroutine(SetSpecificUserVariableAsync(docRef, variables));
            }
        }
        else
        {
            Debug.Log(String.Format("Document {0} does not exist!", month));
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
                Debug.Log(String.Format("Document data for document {0}:", task.Result.Id));
                IDictionary<string, object> documentDictionary = task.Result.ToDictionary();
                if (documentDictionary.ContainsKey("DailyData"))
                {
                    GameManager.instance.Data.daily = JsonUtility.FromJson<Daily>((string)documentDictionary["DailyData"]) ?? new Daily();
                    Debug.Log(String.Format("Variable DailyData found!"));
                }
                else Debug.Log(String.Format("Variable DailyData does not exist!"));
                StartCoroutine(UpdateSpecificUserVariableAsync(docRef, "TimesLoggedInToday", 1, true));
            }
            else
            {
                Debug.Log(String.Format("Document {0} does not exist!", day));
                Dictionary<string, object> variables = new Dictionary<string, object> { { "TimesLoggedInToday", 1 } };
                StartCoroutine(SetSpecificUserVariableAsync(docRef, variables));
            }
        }
        else
        {
            Debug.Log(String.Format("Document {0} does not exist!", day));
            Dictionary<string, object> variables = new Dictionary<string, object> { { "TimesLoggedInToday", 1 } };
            StartCoroutine(SetSpecificUserVariableAsync(docRef, variables));
        }

        Debug.Log("GameManager.instance.Data.date = " + GameManager.instance.Data.date);
    }
    
    // Called whenever a user logins to create a new document for the date they logged in and to increment their TimesLoggedIn counter
    public IEnumerator UpdateLogoutTimestampAsync(bool exit = false)
    {
        Debug.Log("Updating logout timestamp");

        if (GameManager.instance.DisplayName == "") yield break;

        GameManager.instance.Data.LogData();
        
        DocumentReference docRef = database.Collection("Data").Document(GameManager.instance.DisplayName);
        Task<DocumentSnapshot> task = docRef.GetSnapshotAsync();
        yield return new WaitForTaskCompletion(task);
        if (!(task.IsFaulted || task.IsCanceled))
        {
            if (task.Result.Exists)
            {
                Debug.Log(String.Format("Document data for document {0}:", task.Result.Id));
                StartCoroutine(UpdateSpecificUserVariableAsync(docRef, "AllTimeData", JsonUtility.ToJson(GameManager.instance.Data.allTime), false));
            }
            else
            {
                Debug.Log(String.Format("Document {0} does not exist!", GameManager.instance.DisplayName));
                Dictionary<string, object> variables = new Dictionary<string, object> { { "AllTimeData", JsonUtility.ToJson(GameManager.instance.Data.allTime) } };
                StartCoroutine(SetSpecificUserVariableAsync(docRef, variables, SetOptions.MergeAll));
            }
        }
        else
        {
            Debug.Log(String.Format("Document {0} does not exist!", GameManager.instance.DisplayName));
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
                Debug.Log(String.Format("Document data for document {0}:", task.Result.Id));
                StartCoroutine(UpdateSpecificUserVariableAsync(docRef, "YearlyData", JsonUtility.ToJson(GameManager.instance.Data.yearly), false));
            }
            else
            {
                Debug.Log(String.Format("Document {0} does not exist!", GameManager.instance.DisplayName));
                Dictionary<string, object> variables = new Dictionary<string, object> { { "YearlyData", JsonUtility.ToJson(GameManager.instance.Data.yearly) } };
                StartCoroutine(SetSpecificUserVariableAsync(docRef, variables, SetOptions.MergeAll));
            }
        }
        else
        {
            Debug.Log(String.Format("Document {0} does not exist!", GameManager.instance.DisplayName));
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
                Debug.Log(String.Format("Document data for document {0}:", task.Result.Id));
                StartCoroutine(UpdateSpecificUserVariableAsync(docRef, "MonthlyData", JsonUtility.ToJson(GameManager.instance.Data.monthly), false));
            }
            else
            {
                Debug.Log(String.Format("Document {0} does not exist!", GameManager.instance.DisplayName));
                Dictionary<string, object> variables = new Dictionary<string, object> { { "MonthlyData", JsonUtility.ToJson(GameManager.instance.Data.monthly) } };
                StartCoroutine(SetSpecificUserVariableAsync(docRef, variables, SetOptions.MergeAll));
            }
        }
        else
        {
            Debug.Log(String.Format("Document {0} does not exist!", GameManager.instance.DisplayName));
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
                Debug.Log(String.Format("Document data for document {0}:", task.Result.Id));
                StartCoroutine(UpdateSpecificUserVariableAsync(docRef, "DailyData", JsonUtility.ToJson(GameManager.instance.Data.daily), false));
            }
            else
            {
                Debug.Log(String.Format("Document {0} does not exist!", GameManager.instance.DisplayName));
                Dictionary<string, object> variables = new Dictionary<string, object> { { "DailyData", JsonUtility.ToJson(GameManager.instance.Data.daily) } };
                StartCoroutine(SetSpecificUserVariableAsync(docRef, variables, SetOptions.MergeAll));
            }
        }
        else
        {
            Debug.Log(String.Format("Document {0} does not exist!", GameManager.instance.DisplayName));
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
        Debug.Log("Attempting to sign anonymously...");
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
        Debug.Log("Attempting to sign-in with token...");
        string token = "";
        return auth.SignInWithCustomTokenAsync(token).ContinueWithOnMainThread(HandleSignInWithUser);
    }

    //private void OnApplicationPause(bool pause)
    //{
    //    StartCoroutine(UpdateLogoutTimestampAsync(true));
    //}

    //private void OnApplicationQuit()
    //{
    //    Debug.Log("Updating logout timestamp");

    //    if (GameManager.instance && GameManager.instance.DisplayName == "") return;

    //    DocumentReference docRef = database.Collection("Data").Document(GameManager.instance.DisplayName);
    //    Dictionary<string, object> variables = new Dictionary<string, object>
    //        {
    //            { "AllTimeData", JsonUtility.ToJson(GameManager.instance.Data.allTime) }
    //        };
    //    docRef.SetAsync(variables, SetOptions.MergeAll).ContinueWith(task => {
    //        Debug.Log("Added data to the server.");
    //    });

    //    string year = GameManager.instance.Data.date.Year.ToString();
    //    docRef = database.Collection("Data").Document(GameManager.instance.DisplayName).Collection("Year").Document(year);
    //    variables = new Dictionary<string, object>
    //        {
    //            { "YearlyData", JsonUtility.ToJson(GameManager.instance.Data.yearly) }
    //        };
    //    docRef.SetAsync(variables, SetOptions.MergeAll).ContinueWith(task => {
    //        Debug.Log("Added data to the server.");
    //    });

    //    string month = GameManager.instance.Data.date.Month.ToString();
    //    docRef = database.Collection("Data").Document(GameManager.instance.DisplayName).Collection("Year").Document(year).Collection("Month").Document(month);
    //    variables = new Dictionary<string, object>
    //        {
    //            { "MonthlyData", JsonUtility.ToJson(GameManager.instance.Data.monthly) }
    //        };
    //    docRef.SetAsync(variables, SetOptions.MergeAll).ContinueWith(task => {
    //        Debug.Log("Added data to the server.");
    //    });

    //    string day = GameManager.instance.Data.date.Day.ToString();
    //    docRef = database.Collection("Data").Document(GameManager.instance.DisplayName).Collection("Year").Document(year).Collection("Month").Document(month).Collection("Day").Document(day);
    //    variables = new Dictionary<string, object>
    //        {
    //            { "DailyData", JsonUtility.ToJson(GameManager.instance.Data.daily) }
    //        };
    //    docRef.SetAsync(variables, SetOptions.MergeAll).ContinueWith(task => {
    //        Debug.Log("Added data to the server.");
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
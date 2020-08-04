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
            //if (instance == null) instance = ;
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
            else Debug.LogError($"Failed to initialize Firebase with {dependencyResult}");
        }
        else Debug.LogError($"An instance of {nameof(FirebaseManager)} already exists!");
    }

    void Start()
    {
        //data = new DataDictionary();

        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task => {
            var dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available) InitializeFirebase();
            else Debug.LogError("Could not resolve all Firebase dependencies: " + dependencyStatus);
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
        auth = FirebaseAuth.DefaultInstance;
        database = FirebaseFirestore.DefaultInstance;
        database.Settings.WithPersistenceEnabled(false);
        database.DisableNetworkAsync();
        database.EnableNetworkAsync();
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

    void AddData(string dataID, object dataType)
    {
        if (data == null)
            data = new Dictionary<string, object>();
        if (data.ContainsKey(dataID))
            data[dataID] = dataType;
        else
            data.Add(dataID, dataType);
    }

    void RemoveData(string dataID)
    {
        if (data == null)
            data = new Dictionary<string, object>();
        if (data.ContainsKey(dataID))
            data.Remove(dataID);
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
        GameManager.instance.DisplayName = (string)data["Username"];
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
        StartCoroutine(GetSpecificUserDocumentAsync(database.Collection("Data").Document(username), "Sign-In"));
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
            StartCoroutine(GetSpecificUserVariableAsync(database.Collection("Data").Document((string)data["Username"]), "Password"));
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

    public void FinishedGame(GAME_TYPES game)
    {
        switch (game)
        {
            case GAME_TYPES.TANGRAM_GAME:
                break;
            case GAME_TYPES.JIGSAW_GAME:
                //string date = GameManager.instance.Data.date.Day + "." + GameManager.instance.Data.date.Month + "." + GameManager.instance.Data.date.Year;
                //string gameID = " " + date + " " + GameManager.instance.Data.timeStarted.ToString();
                //GameManager.instance.Data.name = gameID;
                //Dictionary<string, object> documentDictionary = new Dictionary<string, object>
                //{
                //    { GameManager.instance.Data.name, JsonUtility.ToJson(data) }
                //};
                //StartCoroutine(SetSpecificUserVariableAsync(database.Collection("Data").Document(GameManager.instance.DisplayName).Collection("Date").Document(date), documentDictionary, SetOptions.MergeAll));
                break;
            case GAME_TYPES.COLOURING_GAME:
                break;
        }
        SceneManager.LoadSceneAsync("Game Select Scene");
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
    //public Task GetSpecificUserDocumentAsync(DocumentReference docRef, string variable)
    //{
    //    Debug.Log("Getting user document");
    //    return docRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
    //    {
    //        if (task.IsCompleted && !task.IsFaulted)
    //        {
    //            data[variable] = true;
    //            OnFireStoreResult.Invoke();
    //            Debug.Log(String.Format("Document data for {0} document:", task.Result.Id));
    //        }
    //        else if (task.IsCompleted && task.IsFaulted)
    //        {
    //            data[variable] = false;
    //            OnFireStoreResult.Invoke();
    //            Debug.Log(String.Format("Document does not exist!"));
    //        }
    //    });
    //}
    public IEnumerator GetSpecificUserDocumentAsync(DocumentReference docRef, string onComplete)
    {
        Debug.Log(String.Format("Getting document {0} from database!", onComplete));
        Task<DocumentSnapshot> task = docRef.GetSnapshotAsync();
        yield return new WaitForTaskCompletion(task);
        if (!(task.IsFaulted || task.IsCanceled))
        {
            data[onComplete] = true;
            Debug.Log(String.Format("Document data for {0} document:", task.Result.Id));
        }
        else
        {
            data[onComplete] = false;
            Debug.Log(String.Format("Document does not exist!"));
        }
        OnFireStoreResult.Invoke();
    }

    //// Used to get an existing variable's value
    //public Task GetSpecificUserVariableAsync(DocumentReference docRef, string variable)
    //{
    //    Debug.Log("Receiving user info");
    //    return docRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
    //    {
    //        if (task.IsCompleted && task.IsFaulted)
    //        {
    //            Debug.Log(String.Format("Document does not exist!"));
    //            return;
    //        }
    //        DocumentSnapshot snapshot = task.Result;
    //        Debug.Log(String.Format("Document data for {0} document:", snapshot.Id));

    //        Dictionary<string, object> documentDictionary = snapshot.ToDictionary();

    //        if (documentDictionary.ContainsKey(variable))
    //        {
    //            data[variable] = documentDictionary[variable];
    //            OnFireStoreResult.Invoke();
    //            Debug.Log(String.Format("{0}: {1}", variable, documentDictionary[variable]));
    //        }
    //        else Debug.Log(String.Format("Variable {0} does not exist!", variable));
    //    });
    //} 
    // Used to get an existing variable's value
    public IEnumerator GetSpecificUserVariableAsync(DocumentReference docRef, string variable)
    {
        Debug.Log(String.Format("Getting variable {0} from database!", variable));
        var task = docRef.GetSnapshotAsync();
        yield return new WaitForTaskCompletion(task);
        if (!(task.IsFaulted || task.IsCanceled))
        {
            DocumentSnapshot snapshot = task.Result;
            IDictionary<string, object> documentDictionary = snapshot.ToDictionary();
            if (documentDictionary.ContainsKey(variable))
            {
                data[variable] = documentDictionary[variable];
                Debug.Log(String.Format("{0}: {1}", variable, documentDictionary[variable]));
            }
            else Debug.Log(String.Format("Variable {0} does not exist!", variable));
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
        else task = docRef.SetAsync(variables, options);
        yield return new WaitForTaskCompletion(task);
        if (!(task.IsFaulted || task.IsCanceled)) Debug.Log(String.Format("Set variables into database!"));
        else Debug.Log(String.Format("Failed to set variables into database!"));
        if (onComplete != "") data[onComplete] = true;
        OnFireStoreResult.Invoke();
    }

    // Used to add a new variable to a new / existing document
    public IEnumerator SetSpecificUserClassAsync(DocumentReference docRef, string value)
    {
        Debug.Log("Updating user info");
        Task task = docRef.SetAsync(value);
        yield return new WaitForTaskCompletion(task);
        if (!(task.IsFaulted || task.IsCanceled)) Debug.Log(String.Format("Set class into database!"));
        else Debug.Log(String.Format("Failed to set class into database!"));
    }

    //// Used to update an existing variable / set fo variables in an existing document
    //// valueChange is true if value is used to change the variable's value and not overwrite it
    //public Task UpdateSpecificUserVariableAsync(DocumentReference docRef, string variable, object value, bool valueChange)
    //{
    //    Debug.Log(String.Format("Updating variable {0} into database!", variable));
    //    return database.RunTransactionAsync(transaction =>
    //    {
    //        return transaction.GetSnapshotAsync(docRef).ContinueWithOnMainThread(task =>
    //        {
    //            if (task.IsCompleted && task.IsFaulted)
    //            {
    //                Debug.Log(String.Format("Document does not exist!"));
    //                return;
    //            }

    //            DocumentSnapshot snapshot = task.Result;
    //            Dictionary<string, object> documentDictionary = snapshot.ToDictionary();
    //            if (documentDictionary.ContainsKey(variable)) Debug.Log(String.Format("{0}: {1}", variable, documentDictionary[variable]));
    //            else Debug.Log(String.Format("Variable {0} does not exist!", variable));

    //            if (valueChange)
    //            {
    //                Debug.Log(String.Format("Before Value: {0}, Data Type: {1}", value, value.GetType()));

    //                switch (Type.GetTypeCode(value.GetType()))
    //                { 
    //                    case TypeCode.Int16:
    //                    case TypeCode.Int32:
    //                    case TypeCode.Int64:
    //                        if (Type.GetTypeCode(documentDictionary[variable].GetType()) != TypeCode.Int16 &&
    //                            Type.GetTypeCode(documentDictionary[variable].GetType()) != TypeCode.Int32 &&
    //                            Type.GetTypeCode(documentDictionary[variable].GetType()) != TypeCode.Int64)
    //                            Debug.LogError(String.Format("Variable {0} ({2}) has a different type than Variable {1} ({3})!", variable, value, documentDictionary[variable].GetType(), value.GetType()));
    //                        if (Type.GetTypeCode(value.GetType()) != TypeCode.Int16 &&
    //                            Type.GetTypeCode(value.GetType()) != TypeCode.Int32 &&
    //                            Type.GetTypeCode(value.GetType()) != TypeCode.Int64)
    //                            Debug.LogError(String.Format("Variable {0} ({2}) has a different type than Variable {1} ({3})!", variable, value, documentDictionary[variable].GetType(), value.GetType()));
    //                        value = snapshot.GetValue<int>(variable) + (int)value;
    //                        break;
    //                    case TypeCode.Decimal:
    //                    case TypeCode.String:
    //                        value = snapshot.GetValue<string>(variable) + (string)value;
    //                        break;
    //                }
    //            }
    //            Debug.Log(String.Format("After Value: {0}, Data Type: {1}", value, value.GetType()));
    //            Dictionary<string, object> updates = new Dictionary<string, object>
    //            {
    //                { variable, value }
    //            };
    //            transaction.Update(docRef, updates);
    //            Debug.Log("Updated specific user info in the database");
    //        });
    //    });
    //}
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
    
    // Called whenever a user logins to create a new document for the date they logged in and to increment their TimesLoggedIn counter
    public IEnumerator UpdateLoginTimestampAsync()
    {
        Debug.Log("Updating login timestamp");

        StartCoroutine(UpdateSpecificUserVariableAsync(database.Collection("Data").Document(GameManager.instance.DisplayName), "TimesLoggedIn", 1, true));

        string year = GameManager.instance.Data.date.Year.ToString();
        DocumentReference docRef = database.Collection("Data").Document(GameManager.instance.DisplayName).Collection("Year").Document(year);
        
        Task<DocumentSnapshot> task = docRef.GetSnapshotAsync();
        yield return new WaitForTaskCompletion(task);
        if (!(task.IsFaulted || task.IsCanceled))
        {
            Debug.Log(String.Format("Document data for document {0}:", task.Result.Id));
            StartCoroutine(UpdateSpecificUserVariableAsync(docRef, "TimesLoggedInThisYear", 1, true));
        }
        else
        {
            Debug.Log(String.Format("Document {0} does not exist!", year));
            Dictionary<string, object> variables = new Dictionary<string, object>
            {
                { "TimesLoggedInThisYear", 1 }
            };
            StartCoroutine(SetSpecificUserVariableAsync(docRef, variables));
        }
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

    //public Task UpdateUserInfoAsync()
    //{
    //    Debug.Log("Updating user info");
    //    DocumentReference docRef = database.Collection("Data").Document(displayName);
    //    Dictionary<string, object> documentDictionary = new Dictionary<string, object>
    //    {
    //        { "DisplayName", displayName },
    //        { "TimesLoggedIn", timesLoggedIn }
    //    };
    //    return docRef.SetAsync(documentDictionary).ContinueWithOnMainThread(task => { Debug.Log("Updated user data into database"); });
    //}

    //public Task ReceiveUserInfoAsync()
    //{
    //    Debug.Log("Receiving user info");
    //    DocumentReference docRef = database.Collection("Data").Document(displayName);
    //    return docRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
    //    {
    //        DocumentSnapshot snapshot = task.Result;
    //        if (!snapshot.Exists) Debug.Log(String.Format("Document {0} does not exist!", snapshot.Id));
    //        Debug.Log(String.Format("Document data for {0} document:", snapshot.Id));
    //        Dictionary<string, object> documentDictionary = snapshot.ToDictionary();
    //        foreach (KeyValuePair<string, object> pair in documentDictionary)
    //        {
    //            Debug.Log(String.Format("{0}: {1}", pair.Key, pair.Value));
    //        }
    //        Debug.Log("Received all data from database");
    //    });
    //}

    private void OnDestroy()
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
}

public class Date : MonoBehaviour
{
    public int timesLoggedInToday;
}
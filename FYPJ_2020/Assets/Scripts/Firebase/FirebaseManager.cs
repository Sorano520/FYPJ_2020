using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Firebase;
using Firebase.Auth;
using Firebase.Firestore;
using Firebase.Extensions;
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
    public UnityEvent OnLoginSuccessful = new UnityEvent();
    public UnityEvent OnLoginFailed = new UnityEvent();

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

    protected void InitializeFirebase()
    {
        Debug.Log("Setting up Firebase Auth");
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

        SetSpecificUserVariableAsync(database.Collection("Data").Document(username), "Password", password);
    }

    public void SignIn(string username, string password)
    {
        Debug.Log("Signing In: " + username + "...");

        AddData("Sign-In", null);
        AddData("Username", username);
        AddData("Unverified", password);
        GetSpecificUserDocumentAsync(database.Collection("Data").Document(username), "Sign-In");
        OnFireStoreResult.AddListener(VerifyExistingUser);
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
            GetSpecificUserVariableAsync(database.Collection("Data").Document((string)data["Username"]), "Password");
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
            Debug.Log("Login successful!");
            GameManager.instance.DisplayName = (string)data["Username"];
            Debug.Log("Welcome back " + GameManager.instance.DisplayName + "!");
            
            OnLoginSuccessful.Invoke();
        }
        else
        {
            Debug.Log((string)data["Unverified"] + " : " + (string)data["Password"]);
            OnLoginFailed.Invoke();
        }
    }

    public void EnterGame()
    {
        UpdateSpecificUserVariableAsync(database.Collection("Data").Document(GameManager.instance.DisplayName), "TimesLoggedIn", 1, true);
    }

    public void FinishedGame(GAME_TYPES game, GameData data)
    {
        switch (game)
        {
            case GAME_TYPES.TANGRAM_GAME:
                break;
            case GAME_TYPES.JIGSAW_GAME:
                data.GameType = game;
                string gameID = data.GameType + " " + data.Date + " " + data.TimeStarted.ToString();
                data.name = gameID;
                //SetSpecificUserClassAsync(database.Collection("Data").Document(GameManager.instance.DisplayName), data);
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
    public Task GetSpecificUserDocumentAsync(DocumentReference docRef, string variable)
    {
        Debug.Log("Getting user document");
        return docRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted && !task.IsFaulted)
            {
                data[variable] = true;
                OnFireStoreResult.Invoke();
                Debug.Log(String.Format("Document data for {0} document:", task.Result.Id));
            }
            else if (task.IsCompleted && task.IsFaulted)
            {
                data[variable] = false;
                OnFireStoreResult.Invoke();
                Debug.Log(String.Format("Document does not exist!"));
            }
        });
    }
    //public IEnumerator GetSpecificUserDocumentAsync(DocumentReference docRef, string variable)
    //{
    //    Debug.Log("Getting user document");
    //    Task setTask = docRef.GetSnapshotAsync();
    //    yield return new WaitForTaskCompletion(this, setTask);
    //    if (!(setTask.IsFaulted || setTask.IsCanceled))
    //    {
    //        // Update the collectionPath/documentId because:
    //        // 1) If the documentId field was empty, this will fill it in with the autoid. This allows
    //        //    you to manually test via a trivial 'click set', 'click get'.
    //        // 2) In the automated test, the caller might pass in an explicit docRef rather than pulling
    //        //    the value from the UI. This keeps the UI up-to-date. (Though unclear if that's useful
    //        //    for the automated tests.)
    //        collectionPath = doc.Parent.Id;
    //        documentId = doc.Id;

    //        fieldContents = "Ok";
    //    }
    //    else
    //    {
    //        fieldContents = "Error";
    //    }
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

    // Used to get an existing variable's value
    public Task GetSpecificUserVariableAsync(DocumentReference docRef, string variable)
    {
        Debug.Log("Receiving user info");
        return docRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted && task.IsFaulted)
            {
                Debug.Log(String.Format("Document does not exist!"));
                return;
            }
            DocumentSnapshot snapshot = task.Result;
            Debug.Log(String.Format("Document data for {0} document:", snapshot.Id));

            Dictionary<string, object> documentDictionary = snapshot.ToDictionary();

            if (documentDictionary.ContainsKey(variable))
            {
                data[variable] = documentDictionary[variable];
                OnFireStoreResult.Invoke();
                Debug.Log(String.Format("{0}: {1}", variable, documentDictionary[variable]));
            }
            else Debug.Log(String.Format("Variable {0} does not exist!", variable));
        });
    }

    // Used to add a new variable to a new / existing document
    public Task SetSpecificUserVariableAsync(DocumentReference docRef, string variable, object value)
    {
        Debug.Log("Updating user info");
        Dictionary<string, object> documentDictionary = new Dictionary<string, object>
        {
            { variable, value }
        };
        return docRef.SetAsync(documentDictionary).ContinueWithOnMainThread(task => { Debug.Log("Updated user data into database"); });
    }

    // Used to add a new variable to a new / existing document
    public void SetSpecificUserClassAsync(DocumentReference docRef, object value)
    {
        Debug.Log("Updating user info");
        docRef.SetAsync(value).ContinueWithOnMainThread(task => { Debug.Log("Updated user data into database"); });
    }

    // Used to update an existing variable / set fo variables in an existing document
    // valueChange is true if value is used to change the variable's value and not overwrite it
    public Task UpdateSpecificUserVariableAsync(DocumentReference docRef, string variable, object value, bool valueChange)
    {
        Debug.Log("Updating specific user info");
        return database.RunTransactionAsync(transaction =>
        {
            return transaction.GetSnapshotAsync(docRef).ContinueWithOnMainThread(task =>
            {
                if (task.IsCompleted && task.IsFaulted)
                {
                    Debug.Log(String.Format("Document does not exist!"));
                    return;
                }

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
                Dictionary<string, object> updates = new Dictionary<string, object>
                {
                    { variable, value }
                };
                transaction.Update(docRef, updates);
                Debug.Log("Updated specific user info in the database");
            });
        });
    }

    // Called whenever a user logins to create a new document for the date they logged in and to increment their TimesLoggedIn counter
    public void UpdateLoginTimestampAsync()
    {
        Debug.Log("Updating login timestamp");

        UpdateSpecificUserVariableAsync(database.Collection("Data").Document(GameManager.instance.DisplayName), "TimesLoggedIn", 1, true);

        DocumentReference docRef = database.Collection("Data").Document(GameManager.instance.DisplayName);
        string date = DateTime.Today.Day + "." + DateTime.Today.Month + "." + DateTime.Today.Year;
        Date newDate = new Date
        {
            name = date,
            timesLoggedInToday = 1
        };
        docRef.SetAsync(newDate);

        //docRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        //{
        //    if (task.IsCompleted && !task.IsFaulted)
        //    {
        //        Debug.Log(String.Format("Document data for {0} document:", task.Result.Id));
        //        UpdateSpecificUserVariableAsync(docRef, "TimesLoggedInToday", 1, true);
        //    }
        //    else if (task.IsCompleted && task.IsFaulted)
        //    {
        //        Debug.Log(String.Format("Document does not exist!"));
        //        SetSpecificUserVariableAsync(docRef, "TimesLoggedInToday", 1);
        //    }
        //});
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
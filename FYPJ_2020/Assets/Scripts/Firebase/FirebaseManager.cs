using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Firebase;
using Firebase.Auth;
using Firebase.Firestore;
using Firebase.Extensions;
using System.Threading.Tasks;
using TMPro;

public class FirebaseManager : MonoBehaviour
{
    public static FirebaseManager instance;
    private FirebaseApp app;
    private FirebaseAuth auth;
    private FirebaseFirestore database;
    protected Dictionary<string, Firebase.Auth.FirebaseUser> userByAuth = new Dictionary<string, Firebase.Auth.FirebaseUser>();

    [SerializeField] protected string displayName = "";
    [SerializeField] protected Int64 timesLoggedIn;
    public TextMeshProUGUI displayText;
    // Flag set when a token is being fetched.  This is used to avoid printing the token
    // in IdTokenChanged() when the user presses the get token button.
    private bool fetchingToken = false;

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
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task => {
            var dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available) InitializeFirebase();
            else Debug.LogError("Could not resolve all Firebase dependencies: " + dependencyStatus);
        });
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C)) UpdateUserInfoAsync();
        if (Input.GetKeyDown(KeyCode.V)) ReceiveUserInfoAsync();
        if (Input.GetKeyDown(KeyCode.Q)) UpdateSpecificUserInfo();
    }

    protected void InitializeFirebase()
    {
        Debug.Log("Setting up Firebase Auth");
        auth = FirebaseAuth.DefaultInstance;
        database = FirebaseFirestore.DefaultInstance;
        auth.StateChanged += AuthStateChanged;
        auth.IdTokenChanged += IdTokenChanged;
        AuthStateChanged(this, null);
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
                displayName = user.DisplayName ?? "";
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
            UpdateLoginTimestampAsync();
            Debug.Log(String.Format("{0} signed in", task.Result.DisplayName));
            return true;
        }
        return false;
    }

    public void SignIn()
    {
        SigninAnonymouslyAsync();
    }

    public void UpdateSpecificUserInfo()
    {
        UpdateSpecificUserInfoAsync("TimesLoggedIn", (Int64)1, true);
    }

    public Task UpdateUserProfileAsync(string newDisplayName = null)
    {
        if (auth.CurrentUser == null)
        {
            Debug.Log("Not signed in, unable to update user profile");
            return Task.FromResult(0);
        }
        displayName = newDisplayName ?? displayName;
        Debug.Log("Updating user profile");
        return auth.CurrentUser.UpdateUserProfileAsync(new Firebase.Auth.UserProfile
        {
            DisplayName = displayName,
            PhotoUrl = auth.CurrentUser.PhotoUrl,
        });
    }

    // valueChange is true if value is used to change the variable's value and not overwrite it
    public Task UpdateSpecificUserInfoAsync(string variable, object value, bool valueChange)
    {
        Debug.Log("Updating specific user info");
        DocumentReference cityRef = database.Collection("Data").Document(displayName);
        return database.RunTransactionAsync(transaction =>
        {
            return transaction.GetSnapshotAsync(cityRef).ContinueWithOnMainThread(task =>
            {
                DocumentSnapshot snapshot = task.Result;
                if (!snapshot.Exists) Debug.Log(String.Format("Document {0} does not exist!", snapshot.Id));

                Dictionary<string, object> documentDictionary = snapshot.ToDictionary();
                if (documentDictionary.ContainsKey(variable)) Debug.Log(String.Format("{0}: {1}", variable, documentDictionary[variable]));
                else Debug.Log(String.Format("Variable {0} does not exist!", variable));

                if (documentDictionary[variable].GetType() != value.GetType()) Debug.LogError(String.Format("Variable {0} ({2}) has a different type than Variable {1} ({3})!", variable, value, documentDictionary[variable].GetType(), value.GetType()));
                if (valueChange)
                {
                    switch (Type.GetTypeCode(value.GetType()))
                    {
                        case TypeCode.Decimal:
                            value = snapshot.GetValue<float>(variable) + (float)value;
                            break;
                        case TypeCode.Int16:
                        case TypeCode.Int32:
                        case TypeCode.Int64:
                            value = snapshot.GetValue<int>(variable) + (int)value;
                            break;
                        case TypeCode.String:
                            value = snapshot.GetValue<string>(variable) + (string)value;
                            break;
                    }
                }
                Dictionary<string, object> updates = new Dictionary<string, object>
                {
                    { variable, value }
                };
                transaction.Update(cityRef, updates);
                Debug.Log("Updated specific user info in the database");
            });
        });
    }

    public Task ReceiveSpecificUserInfoAsync(string variable)
    {
        Debug.Log("Receiving user info");
        DocumentReference docRef = database.Collection("Data").Document(displayName);
        return docRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            DocumentSnapshot snapshot = task.Result;
            if (!snapshot.Exists) Debug.Log(String.Format("Document {0} does not exist!", snapshot.Id));
            Debug.Log(String.Format("Document data for {0} document:", snapshot.Id));
            Dictionary<string, object> documentDictionary = snapshot.ToDictionary();

            if (documentDictionary.ContainsKey(variable)) Debug.Log(String.Format("{0}: {1}", variable, documentDictionary[variable]));
            else Debug.Log(String.Format("Variable {0} does not exist!", variable));
        });
    }

    public Task UpdateUserInfoAsync()
    {
        Debug.Log("Updating user info");
        DocumentReference docRef = database.Collection("Data").Document(displayName);
        Dictionary<string, object> documentDictionary = new Dictionary<string, object>
        {
            { "DisplayName", displayName },
            { "TimesLoggedIn", timesLoggedIn }
        };
        return docRef.SetAsync(documentDictionary).ContinueWithOnMainThread(task => { Debug.Log("Updated user data into database"); });
    }

    public Task ReceiveUserInfoAsync()
    {
        Debug.Log("Receiving user info");
        DocumentReference docRef = database.Collection("Data").Document(displayName);
        return docRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            DocumentSnapshot snapshot = task.Result;
            if (!snapshot.Exists) Debug.Log(String.Format("Document {0} does not exist!", snapshot.Id));
            Debug.Log(String.Format("Document data for {0} document:", snapshot.Id));
            Dictionary<string, object> documentDictionary = snapshot.ToDictionary();
            foreach (KeyValuePair<string, object> pair in documentDictionary)
            {
                Debug.Log(String.Format("{0}: {1}", pair.Key, pair.Value));
            }
            Debug.Log("Received all data from database");
        });
    }

    public Task UpdateLoginTimestampAsync()
    {
        Debug.Log("Updating login timestamp");
        string date = DateTime.Today.Day + "." + DateTime.Today.Month + "." + DateTime.Today.Year;
        DocumentReference docRef = database.Collection("Data").Document(displayName).Collection("Date").Document(date);
        Dictionary<string, object> userData = new Dictionary<string, object>
        {
            { "TimesLoggedInToday", timesLoggedIn }
        };
        return docRef.SetAsync(userData).ContinueWithOnMainThread(task => { Debug.Log("Updated user login timestamp into database"); });
    }

    public Task SigninAnonymouslyAsync()
    {
        Debug.Log("Attempting to sign anonymously...");
        displayName = displayText.text;
        string newDisplayName = displayName;
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

    private void OnDestroy()
    {
        if (auth != null)
        {
            auth.StateChanged -= AuthStateChanged;
            auth.IdTokenChanged -= IdTokenChanged;
            auth = null;
        }
        database = null;
        app = null;
        if (instance == this) instance = null;
    }
}

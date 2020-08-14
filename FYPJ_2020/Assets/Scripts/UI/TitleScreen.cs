using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleScreen : MonoBehaviour
{
    // For new user
    [SerializeField] GameObject newUsername;
    [SerializeField] InputField newUsernameInput;
    [SerializeField] Color newUsernameTextColor;
    [SerializeField] GameObject newPassword;
    [SerializeField] InputField newPasswordInput;
    [SerializeField] Color newPasswordTextColor;
    [SerializeField] GameObject newAgain;
    [SerializeField] InputField newAgainInput;
    [SerializeField] Color newAgainTextColor;
    [SerializeField] GameObject newFin;

    // For Registered User
    bool user;
    [SerializeField] GameObject login;
    [SerializeField] InputField loginUsername;
    [SerializeField] InputField loginPassword;
    [SerializeField] Text loginErrorMsg;
    
    private void Awake()
    {
        newUsernameTextColor = newUsernameInput.placeholder.GetComponent<Text>().color;
    }

    public void Start()
    {
        newUsername.SetActive(false);
        newUsernameInput.text = "";
        newUsernameInput.characterLimit = 15;
        newUsernameInput.placeholder.GetComponent<Text>().text = "E.g. Sarah";
        newUsernameInput.placeholder.GetComponent<Text>().color = newUsernameTextColor;
        newPassword.SetActive(false);
        newUsernameInput.text = "";
        newUsernameInput.placeholder.GetComponent<Text>().text = "";
        newUsernameInput.placeholder.GetComponent<Text>().color = newUsernameTextColor;
        newAgain.SetActive(false);
        newUsernameInput.text = "";
        newUsernameInput.placeholder.GetComponent<Text>().text = "";
        newUsernameInput.placeholder.GetComponent<Text>().color = newUsernameTextColor;
        newFin.SetActive(false);
        user = true;
        login.SetActive(false);
        loginErrorMsg.text = "";
    }

    public void EnableNewUsername()
    {
        newUsername.SetActive(true);
    }
    public void EnableNewPassword()
    {
        newUsername.SetActive(false);
        newPassword.SetActive(true);
    }
    public void EnableNewAgain()
    {
        newPassword.SetActive(false);
        newAgain.SetActive(true);
    }
    public void EnableNewFin()
    {
        newAgain.SetActive(false);
        newFin.SetActive(true);

        StartCoroutine(NextScene(user));
    }
    public void UserLogin()
    {
        login.SetActive(true);
    }
    public void CaregiverLogin()
    {
        user = false;
        login.SetActive(true);
    }

    public void HandleUsernameInput()
    {
        if (String.IsNullOrWhiteSpace(newUsernameInput.text))
        {
            newUsernameInput.text = "";
            newUsernameInput.placeholder.GetComponent<Text>().text = "Please enter a username!";
            newUsernameInput.placeholder.GetComponent<Text>().color = new Color(0.67f, 0, 0, 0.63f);
            return;
        }

        FirebaseManager.instance.AddData("Sign-In", null);
        FirebaseManager.instance.OnFireStoreResult.AddListener(HandleUsernameResponse);
        FirebaseManager.instance.CheckUsernameExists(newUsernameInput.text);
    }
    void HandleUsernameResponse()
    {
        if (FirebaseManager.instance.Data["Sign-In"] == null) return;

        FirebaseManager.instance.OnFireStoreResult.RemoveListener(HandleUsernameResponse);
        
        if ((bool)FirebaseManager.instance.Data["Sign-In"] == false)
        {
            Debug.Log("Username " + (string)FirebaseManager.instance.Data["Username"] + " has not been taken!");
            EnableNewPassword();
        }
        else
        {
            newUsernameInput.text = "";
            newUsernameInput.placeholder.GetComponent<Text>().text = "Username has been taken!";
            newUsernameInput.placeholder.GetComponent<Text>().color = new Color(0.67f, 0, 0, 0.63f);
        }

        FirebaseManager.instance.RemoveData("Sign-In");
    }

    public void HandlePasswordInput()
    {
        if (String.IsNullOrWhiteSpace(newPasswordInput.text))
        {
            newPasswordInput.text = "";
            newPasswordInput.placeholder.GetComponent<Text>().text = "Please enter a password!";
            newPasswordInput.placeholder.GetComponent<Text>().color = new Color(0.67f, 0, 0, 0.63f);
            return;
        }

        Debug.Log("Password entered!");
        EnableNewAgain();
    }
    public void HandleAgainInput()
    {
        if (String.IsNullOrWhiteSpace(newAgainInput.text))
        {
            newAgainInput.text = "";
            newAgainInput.placeholder.GetComponent<Text>().text = "Please enter a password!";
            newAgainInput.placeholder.GetComponent<Text>().color = new Color(0.67f, 0, 0, 0.63f);
            return;
        }
        else if (newPasswordInput.text != newAgainInput.text)
        {
            newAgainInput.text = "";
            newAgainInput.placeholder.GetComponent<Text>().text = "Please enter the same password!";
            newAgainInput.placeholder.GetComponent<Text>().color = new Color(0.67f, 0, 0, 0.63f);
            return;
        }
        Debug.Log("Again entered!");
        
        FirebaseManager.instance.OnSigninSuccessful.AddListener(EnableNewFin);
        FirebaseManager.instance.SignUp(newUsernameInput.text, newPasswordInput.text);
    }
    void HandlePasswordResponse()
    {
        if (FirebaseManager.instance.Data["Sign-In"] == null) return;

        FirebaseManager.instance.OnFireStoreResult.RemoveListener(HandleUsernameResponse);
        
        if ((bool)FirebaseManager.instance.Data["Sign-In"] == false)
        {
            Debug.Log("Existing user " + (string)FirebaseManager.instance.Data["Username"] + " found!");
            FirebaseManager.instance.RemoveData("Username");
            EnableNewPassword();
        }
        else
        {
            newUsernameInput.text = "";
            newUsernameInput.placeholder.GetComponent<Text>().text = "Username has been taken!";
            newUsernameInput.placeholder.GetComponent<Text>().color = new Color(0.67f, 0, 0, 0.63f);
        }

        FirebaseManager.instance.RemoveData("Sign-In");
    }

    public void HandleLoginInput()
    {
        if (String.IsNullOrWhiteSpace(loginUsername.text) || String.IsNullOrWhiteSpace(loginPassword.text))
        {
            loginUsername.text = "";
            loginPassword.text = "";
            loginErrorMsg.text = "Please enter a username and password!";
            return;
        }
        FirebaseManager.instance.OnSigninFailed.AddListener(HandleSignInFailure);
        FirebaseManager.instance.OnSigninSuccessful.AddListener(HandleSignInSuccessful);
        FirebaseManager.instance.SignIn(loginUsername.text, loginPassword.text);
    }
    void HandleSignInFailure()
    {
        loginUsername.text = "";
        loginPassword.text = "";
        loginErrorMsg.text = "Wrong username or password!";
        FirebaseManager.instance.OnSigninFailed.RemoveListener(HandleSignInFailure);
        FirebaseManager.instance.OnSigninSuccessful.RemoveListener(HandleSignInSuccessful);
    }
    void HandleSignInSuccessful()
    {
        StartCoroutine(NextScene(user));
        FirebaseManager.instance.OnSigninFailed.RemoveListener(HandleSignInFailure);
        FirebaseManager.instance.OnSigninSuccessful.RemoveListener(HandleSignInSuccessful);
    }

    IEnumerator NextScene(bool user)
    {
        FirebaseManager.instance.EnterGame();

        yield return new WaitForSeconds(3);

        if (user) GameObject.Find("Canvas").GetComponent<Transitions>().ToLoadingScreen();
        else GameObject.Find("Canvas").GetComponent<Transitions>().ToCaregiverMenu();
    }
}

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
    [SerializeField] GameObject login;

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
        login.SetActive(false);
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

        StartCoroutine(NextScene());
    }
    public void UserLogin()
    {
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
            newUsernameInput.placeholder.GetComponent<Text>().text = "Username has already been taken!";
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
            newUsernameInput.placeholder.GetComponent<Text>().text = "Username has already been taken!";
            newUsernameInput.placeholder.GetComponent<Text>().color = new Color(0.67f, 0, 0, 0.63f);
        }

        FirebaseManager.instance.RemoveData("Sign-In");
    }

    IEnumerator NextScene()
    {
        FirebaseManager.instance.EnterGame();

        yield return new WaitForSeconds(3);

        GameObject.Find("Canvas").GetComponent<Transitions>().ToLoadingScreen();
    }
}

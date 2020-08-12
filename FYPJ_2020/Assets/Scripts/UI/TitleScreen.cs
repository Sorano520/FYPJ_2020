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
    [SerializeField] GameObject newAgain;
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
        newAgain.SetActive(false);
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
            newUsernameInput.placeholder.GetComponent<Text>().color = Color.red;
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
            Debug.Log("Existing user " + (string)FirebaseManager.instance.Data["Username"] + " found!");
            FirebaseManager.instance.RemoveData("Username");
            EnableNewPassword();
        }
        else
        {
            newUsernameInput.text = "";
            newUsernameInput.placeholder.GetComponent<Text>().text = "Username has already been taken!";
            newUsernameInput.placeholder.GetComponent<Text>().color = Color.red;
        }

        FirebaseManager.instance.RemoveData("Sign-In");
    }
}

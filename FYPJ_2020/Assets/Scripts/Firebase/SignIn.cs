using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SignIn : MonoBehaviour
{
    public TextMeshProUGUI username;
    public TextMeshProUGUI password;

    private void Start()
    {
        FirebaseManager.instance.OnLoginFailed.AddListener(HandleWrongPassword);
        username.text = password.text = "";
    }

    public void HandleSignIn()
    {
        if (String.IsNullOrWhiteSpace(username.text) || String.IsNullOrWhiteSpace(password.text))
        {
            Debug.Log("Please enter a username and password!");
            return;
        }

        FirebaseManager.instance.SignIn(username.text, password.text);
    }

    void HandleWrongPassword()
    {
        username.text = null;
        Debug.Log("Login failed!");
        Debug.Log("Please enter a correct password!");
    }
}

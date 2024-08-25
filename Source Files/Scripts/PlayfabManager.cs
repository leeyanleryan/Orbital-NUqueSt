using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;
using TMPro;

public class PlayfabManager : MonoBehaviour
{
    //buttons
    public GameObject loginPanel;
    public GameObject mmPanel;
    
    [Header("Login")]
    public TMP_InputField emailLoginField;
    public TMP_InputField usernameLoginField;
    public TMP_InputField passwordLoginField;
    public TMP_Text warningLoginText;
    public TMP_Text confirmLoginText;

    [Header("register")]
    public TMP_InputField usernameRegisterField;
    public TMP_InputField emailRegisterField;
    public TMP_InputField passwordRegisterField;
    public TMP_InputField passwordRegisterVerifyField;
    public TMP_Text warningRegisterText;

    public TMP_InputField emailResetField;


    public GameObject uiManager;
    public UIManager uiManagerScript;

    void Awake()
    {
        uiManagerScript = uiManager.GetComponent<UIManager>();
    }
    public void LoginButton()
    {
        var request = new LoginWithEmailAddressRequest
        {
            Email = emailLoginField.text,
            Password = passwordLoginField.text
        };
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnLoginError);
    }
    //Function for the register button
    public void RegisterButton()
    {
        if (passwordRegisterVerifyField.text == passwordRegisterField.text)
        {
            var request = new RegisterPlayFabUserRequest
            {
                Email = emailRegisterField.text,
                Password = passwordRegisterField.text,
                RequireBothUsernameAndEmail = false
            };
            PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnRegisterError);
        }
        else 
        {
            warningRegisterText.text = "Passwords do not match! Please double check your passwords to ensure that they match!";
        }
    }

    public void ResetPasswordButton()
    {
        var request = new SendAccountRecoveryEmailRequest
        {
            Email = emailResetField.text,
            TitleId = "EABA5"
        };
        PlayFabClientAPI.SendAccountRecoveryEmail(request, OnPasswordReset, OnRecoveryError);
    }

    void OnPasswordReset(SendAccountRecoveryEmailResult result)
    {
        warningLoginText.text = "Check your Email!";
    }
    void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        uiManagerScript.LoginScreen();
        confirmLoginText.text = "Account successfully created! Please Login!";
    }

    void OnLoginSuccess(LoginResult result)
    {
        warningLoginText.text = "";
        confirmLoginText.text = "Logged In";
        DatabaseManager.instance.userID = usernameLoginField.text;
        DataPersistenceManager.instance.LoadGame();
        LoginScreen();
    }

    void OnRegisterError(PlayFabError error)
    {
        warningRegisterText.text = error.ErrorMessage;
        if (passwordRegisterField.text.Length < 6)
        {
            warningRegisterText.text = "Password is too short! Please enter a Password of length at least 6 characters!";
            return;
        }
        else if (emailRegisterField.text.Length == 0)
        {
            warningRegisterText.text = "Email Register Field is empty! Please enter your email";
            return;
        }
        else if (passwordRegisterField.text.Length == 0)
        {
            warningRegisterText.text = "Password Register Field is empty! Please enter a password!";
            return;
        }
        else
        {
            warningRegisterText.text = error.GenerateErrorReport();
            print(error.GenerateErrorReport());
        }
        return;
    }
    void OnLoginError(PlayFabError error)
    {
        warningLoginText.text = error.ErrorMessage;
        if (passwordLoginField.text.Length < 6)
        {
            warningLoginText.text = "Password is too short! Please enter a Password of length at least 6 characters!";

            return;
        }
        else if (emailLoginField.text.Length == 0)
        {
            warningLoginText.text = "Email Register Field is empty! Please enter your email";
            return;
        }
        else if (passwordLoginField.text.Length == 0)
        {
            warningLoginText.text = "Password Register Field is empty! Please enter a password!";
            return;
        }
        else
        {
            warningLoginText.text = error.ErrorMessage;
        }
        return;
    }

    void OnRecoveryError(PlayFabError error)
    {
        Debug.Log(error.ToString());
    }
    public void LoginScreen()
    {
        mmPanel.SetActive(true);
        loginPanel.SetActive(false);
    }
}

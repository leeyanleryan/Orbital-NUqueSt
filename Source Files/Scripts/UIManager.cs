using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    public static UIManager instance { get; private set; }
    public GameObject startMenuUI;
    public GameObject loginUI;
    public GameObject registerUI;
    public GameObject PWResetMenu;
    
    public void BackButton()
    {
        loginUI.SetActive(true);
        startMenuUI.SetActive(true);
    }

    public void LoginScreen()
    {
        loginUI.SetActive(true);
    }

    public void RegisterScreen()
    {
        loginUI.SetActive(false);
        registerUI.SetActive(true);
    }

    public void OpenResetPWMenu()
    {
        if (!PWResetMenu.activeSelf)
        {
            PWResetMenu.SetActive(true);
            loginUI.SetActive(false);
            registerUI.SetActive(false);
        }
        else
        {
            PWResetMenu.SetActive(false);
            loginUI.SetActive(true);
            registerUI.SetActive(false);
        }
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMenuScreen : MonoBehaviour
{
    public GameObject startMenu;
    public GameObject loginMenu;
    public GameObject quitButton;
    public GameObject settingsButton;

    public GameObject startRegisterObj;
    public GameObject startLoginObj;

    public GameObject openSettings;

    public AudioSource audioSource;
    public Slider audioVolume;

    public void LoginButtonPressed()
    {
        startMenu.SetActive(false);
    }

    public void RegisterButtonPressed()
    {
        startMenu.SetActive(false);
        loginMenu.SetActive(false);
    }

    public void HideAllButtons()
    {
        startRegisterObj.SetActive(false);
        startLoginObj.SetActive(false);
        quitButton.SetActive(false);
        settingsButton.SetActive(false);
    }

    public void OpeningStartMenuScreenSettingsPage()
    {
        openSettings.SetActive(true);
    }

    public void CloseSettings()
    {
        openSettings.SetActive(false);
        startRegisterObj.SetActive(true);
        startLoginObj.SetActive(true);
        quitButton.SetActive(true);
        settingsButton.SetActive(true);

    }

    public void Quit()
    {
        Application.Quit();
    }

    public void SetVolume()
    {
        BGMManager.instance.gameObject.GetComponent<AudioSource>().volume = audioVolume.value;
        SFXManager.instance.gameObject.GetComponent<AudioSource>().volume = audioVolume.value;
    }
}

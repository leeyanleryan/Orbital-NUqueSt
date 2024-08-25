using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.Rendering;

public class MainMenuScript : MonoBehaviour
{
    public GameObject settingsPanel;
    public GameObject mmPanel;
    //public GameObject buttonsMM;
    public AudioMixer volumeMixer;
    public GameObject PWResetMenu;
    public AudioSource audioSource;
    public Slider audioVolume;
  
    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
    }

    public void DisableMainMenu()
    {
        mmPanel.SetActive(false);
    }

    public void EnableMainMenu()
    {
        mmPanel.SetActive(true);
    }

    public void OpenResetPWMenu()
    {
        if (!PWResetMenu.activeSelf)
        {
            PWResetMenu.SetActive(true);
        }
        else
        {
            PWResetMenu.SetActive(false);
        }
    }

    public void SetVolume()
    {
        BGMManager.instance.gameObject.GetComponent<AudioSource>().volume = audioVolume.value;
        SFXManager.instance.gameObject.GetComponent<AudioSource>().volume = audioVolume.value;
    }

    public void ExitGame()
    {
        Debug.Log("Quitting");
        Application.Quit();
    }
}

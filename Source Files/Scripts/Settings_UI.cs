using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Settings_UI : MonoBehaviour
{
    public GameObject settingsPanel;
    public GameObject controlPanel;
    public GameObject creditsPanel;
    public GameObject volumePanel;
    public GameObject optionPanel;

    public TextMeshProUGUI controlText;
    public Scrollbar controlScrollbar;
    public RectTransform controlContainer;
    public TextMeshProUGUI creditsText;
    public Scrollbar creditsScrollbar;
    public RectTransform creditsContainer;

    private PlayerMovement playerMovement;
    private PlayerItems playerItems;

    private bool settingsActive;

    public Slider slider;

    private void Start()
    {
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        playerItems = GameObject.Find("Player").GetComponent<PlayerItems>();
    }

    void Update()
    {
        if (!playerItems.disableToolbar && Input.GetKeyDown(KeyCode.Escape)) 
        {
            settingsPanel.SetActive(true);
            playerItems.disableToolbar = true;
            settingsActive = true;
            playerMovement.enabled = false;
            slider.value = BGMManager.instance.gameObject.GetComponent<AudioSource>().volume;
        }
        else if (playerItems.disableToolbar && settingsActive && Input.GetKeyDown(KeyCode.Escape))
        {
            ReturnToGame();
        }
    }

    public void ReturnToGame()
    {
        settingsPanel.SetActive(false);
        controlPanel.SetActive(false);
        creditsPanel.SetActive(false);
        volumePanel.SetActive(false);
        optionPanel.SetActive(false);
        playerItems.disableToolbar = false;
        settingsActive = false;
        playerMovement.enabled = true;
    }

    private void StartSizeSetup(TextMeshProUGUI text)
    {
        float bottomLength = text.rectTransform.offsetMin.y;
        text.rectTransform.offsetMin = new Vector2(text.rectTransform.offsetMin.x, bottomLength);
    }

    private void StartScrollSetup(TextMeshProUGUI text, Scrollbar scrollbar, RectTransform container)
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(text.rectTransform);
        Canvas.ForceUpdateCanvases();
        scrollbar.value = 1f;
        float textLength = text.textBounds.size.y;
        float bottomLength = text.rectTransform.offsetMin.y;
        float containerLength = container.rect.height;
        float panelLength = text.rectTransform.rect.height;
        if (textLength <= containerLength)
        {
            text.rectTransform.offsetMin = new
            Vector2(text.rectTransform.offsetMin.x, 0);
        }
        else
        {
            text.rectTransform.offsetMin = new Vector2(text.rectTransform.offsetMin.x, bottomLength + panelLength - textLength - 4);
        }
    }

    public void Options()
    {
        optionPanel.SetActive(true);
        settingsPanel.SetActive(false);
    }
    
    public void Volume()
    {
        volumePanel.SetActive(true);
        optionPanel.SetActive(false);
    }

    public void Controls()
    {
        controlPanel.SetActive(true);
        StartSizeSetup(controlText);
        controlText.text = "W A S D - Movement\nESC - Settings Panel/Close UI\nTAB - Inventory Panel\nQ - Quest Panel" +
            "\nE - Interact\nSpace Bar - Proceed Dialogue\nLeft Click - Use Item in Toolbar\nRight Click - Use Item in Toolbar";
        StartScrollSetup(controlText, controlScrollbar, controlContainer);
        optionPanel.SetActive(false);
    }

    public void BackOptions()
    {
        optionPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void BackControls()
    {
        controlPanel.SetActive(false);
        optionPanel.SetActive(true);
    }

    public void BackVolume()
    {
        volumePanel.SetActive(false);
        optionPanel.SetActive(true);
    }

    public void BackCredits()
    {
        creditsPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }
   
    public void Credits()
    {
        StartSizeSetup(creditsText);
        creditsText.text = "Game Developers: Thaw Tun Zan, Lee Yan Le Ryan\n\nProject Advisor: Eugene Tang Kang Jie\n\n" +
            "Game Testers: Edwin Zheng Yuan Yi, Toh Li Yuan, Brannon Aw Xu Wei, Sean William Bulawan Villamin, Project Sage, " +
            "Anders Foong Zer Hong, Danial Hisham" +
            "\n\nPeer Evaluators: Unmei no Farfalla, PestControl, GrassToucher";
        StartScrollSetup(creditsText, creditsScrollbar, creditsContainer);
        creditsPanel.SetActive(true);
        settingsPanel.SetActive(false);
    }

    public void ExitToMainMenu()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    public void ChangeVolume()
    {
        BGMManager.instance.gameObject.GetComponent<AudioSource>().volume = slider.value;
        SFXManager.instance.gameObject.GetComponent<AudioSource>().volume = slider.value;
    }
}

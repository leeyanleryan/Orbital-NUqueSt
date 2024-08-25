using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TownMayorHouse_UI : MonoBehaviour
{
    public PlayerTutorial playerTutorial;

    public GameObject tutorialPanel;
    public TextMeshProUGUI tutorialText;

    private bool openTMDialogueDone;
    public GameObject dialoguePanel;

    private bool progressTMDialogueDone;
    public GameObject openTMDialogueFirst;

    private bool openSecDialogueDone;

    private bool progressSecDialogueDone;
    public GameObject openSecDialogueFirst;

    private void Start()
    {
        playerTutorial = GameObject.Find("Player").GetComponent<PlayerTutorial>();
    }

    private void Update()
    {
        if (GameManager.instance.tutorialProgress == 0)
        {
            StartTutorial();
        }
        else if (GameManager.instance.tutorialProgress == 1)
        {
            Destroy(openTMDialogueFirst);
            Destroy(openSecDialogueFirst);
            CloseSecDialogueCheck();
        }
        else
        {
            Destroy(openTMDialogueFirst);
            Destroy(openSecDialogueFirst);
            Destroy(this.gameObject);
        }
    }

    private void StartTutorial()
    {
        if (!openTMDialogueDone)
        {
            OpenTMDialogueCheck();
        }
        else if (!progressTMDialogueDone)
        {
            ProgressTMDialogueCheck();
        }
        else if (!openSecDialogueDone)
        {
            OpenSecDialogueCheck();
        }
        else if (!progressSecDialogueDone)
        {
            ProgressSecDialogueCheck();
        }
    }

    private void OpenTMDialogueCheck()
    {
        if (dialoguePanel.activeSelf)
        {
            openTMDialogueDone = true;
            tutorialText.text = "Press SPACEBAR to progress the dialogue";
        }
    }

    private void ProgressTMDialogueCheck()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            progressTMDialogueDone = true;
            Destroy(openTMDialogueFirst);
            tutorialText.text = "";
            tutorialPanel.SetActive(false);
        }
    }

    private void OpenSecDialogueCheck()
    {
        if (!dialoguePanel.activeSelf)
        {
            openSecDialogueDone = true;
            tutorialText.text = "Meet the secretary in the living room";
            tutorialPanel.SetActive(true);
        }
    }

    private void ProgressSecDialogueCheck()
    {
        if (dialoguePanel.activeSelf)
        {
            progressSecDialogueDone = true;
            Destroy(openSecDialogueFirst);
            tutorialText.text = "";
            tutorialPanel.SetActive(false);
            playerTutorial.tutorialProgress = 1;
        }
    }

    private void CloseSecDialogueCheck()
    {
        if (!dialoguePanel.activeSelf)
        {
            tutorialText.text = "Head south of the village to see your house";
            tutorialPanel.SetActive(true);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WallPuzzle : MonoBehaviour
{
    public GameObject puzzlePanel;
    public TextMeshProUGUI puzzleText;
    public TMP_InputField puzzleInput;
    public Button puzzleButton;
    public GameObject puzzleClose;

    public GameObject puzzleActivated;
    public GameObject puzzleCorrect;
    public GameObject visualCue;

    public WallPuzzleTrigger puzzleTrigger;

    protected bool startBattle;
    protected bool inBattle;

    protected bool playerInRange;

    protected PlayerItems playerItems;
    protected PlayerMovement playerMovement;
    protected PlayerQuests playerQuests;

    protected bool hasQuest = true;

    protected virtual void Start()
    {
        playerItems = GameObject.Find("Player").GetComponent<PlayerItems>();
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        playerQuests = GameObject.Find("Player").GetComponent<PlayerQuests>();
        HideUI();
    }

    protected virtual void Update()
    {
        if (playerInRange && puzzleActivated.activeSelf)
        {
            TriggerNote();
        }
        if (inBattle)
        {
            CheckInBattle();
        }
    }

    private void TriggerNote()
    {
        if (!playerItems.disableToolbar && !puzzlePanel.activeSelf  && !puzzleCorrect.activeSelf && Input.GetKeyDown(KeyCode.E))
        {
            GetPuzzleAnswer();
            ChangePuzzleText();
            ShowUI();
        }
        else if (playerItems.disableToolbar && puzzlePanel.activeSelf
            && (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Escape)))
        {
            HideUI();
        }
    }

    protected virtual void ChangePuzzleText()
    {

    }

    protected virtual int GetPuzzleAnswer()
    {
        return 0;
    }

    protected virtual void SpawnEnemy()
    {

    }

    protected virtual void CheckInBattle()
    {
        
    }

    protected virtual void ChangeQuestProgress()
    {

    }

    public virtual void CheckQuestProgress(int questProgress)
    {

    }

    public virtual void CheckAnswer()
    {
        bool parseSuccess = int.TryParse(puzzleInput.text.Trim(), out int playerAnswer);
        if (parseSuccess)
        {
            if (playerAnswer == GetPuzzleAnswer())
            {
                puzzleText.text = "Correct!\n\nYou are now freed from this room.";
                puzzleClose.SetActive(true);
                puzzleInput.gameObject.SetActive(false);
                ChangeQuestProgress();
            }
            else
            {
                puzzleText.text = "Oh no, that is wrong...\n\nThere is a surprise waiting for you :)";
                puzzleClose.SetActive(true);
                puzzleInput.gameObject.SetActive(false);
                startBattle = true;
            }
        }
    }

    public void ShowUI()
    {
        puzzleButton.gameObject.SetActive(true);
        puzzleText.gameObject.SetActive(true);
        puzzleInput.gameObject.SetActive(true);
        puzzlePanel.SetActive(true);
        playerItems.disableToolbar = true;
        playerMovement.enabled = false;
    }

    public void HideUI()
    {
        puzzleButton.gameObject.SetActive(false);
        puzzlePanel.SetActive(false);
        puzzleText.gameObject.SetActive(false);
        puzzleInput.gameObject.SetActive(false);
        puzzleClose.SetActive(false);
        visualCue.SetActive(false);
        playerItems.disableToolbar = false;
        playerMovement.enabled = true;
        if (startBattle)
        {
            puzzleTrigger.inBattle = true;
            inBattle = true;
            puzzleActivated.SetActive(false);
            SpawnEnemy();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && puzzleActivated.activeSelf && !puzzleCorrect.activeSelf)
        {
            playerInRange = true;
            visualCue.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerInRange = false;
            visualCue.SetActive(false);
        }
    }
}

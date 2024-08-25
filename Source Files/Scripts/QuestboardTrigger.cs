using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestboardTrigger : MonoBehaviour
{
    private PlayerQuests playerQuests;
    private PlayerItems playerItems;
    private PlayerMovement playerMovement;

    public GameObject completedQuestPanel;
    public TextMeshProUGUI completedQuestText;
    public Scrollbar scrollbar;
    private bool playerInRange;
    public GameObject visualCue;

    private void Start()
    {
        playerQuests = GameObject.Find("Player").GetComponent<PlayerQuests>();
        playerItems = GameObject.Find("Player").GetComponent<PlayerItems>();
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (playerInRange)
        {
            QuestboardUI();
        }
    }

    private void QuestboardUI()
    {
        if (!playerItems.disableToolbar && !completedQuestPanel.activeSelf && Input.GetKeyDown(KeyCode.E))
        {
            playerItems.disableToolbar = true;
            playerMovement.enabled = false;
            completedQuestPanel.SetActive(true);
            UpdateText();
        }
        else if (playerItems.disableToolbar && completedQuestPanel.activeSelf
            && (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Escape)))
        {
            CloseQuestboard();
        }
    }

    private void UpdateText()
    {
        completedQuestText.rectTransform.offsetMin = new Vector2(completedQuestText.rectTransform.offsetMin.x, -795.7479f);
        string text = "";
        for (int i = 0; i < playerQuests.completedQuestDescs.Count; i++)
        {
            text += i + 1 + ". " + playerQuests.completedQuestNames[i] + ": " + playerQuests.completedQuestDescs[i] + "\n";
        }
        if (text.Length < 2)
        {
            text = "None yet. Start talking to a villager to start a quest!";
        }
        completedQuestText.text = text;
        LayoutRebuilder.ForceRebuildLayoutImmediate(completedQuestText.rectTransform);
        Canvas.ForceUpdateCanvases();
        scrollbar.value = 1f;
        float textLength = completedQuestText.textBounds.size.y;
        if (textLength <= 480)
        {
            completedQuestText.rectTransform.offsetMin = new
            Vector2(completedQuestText.rectTransform.offsetMin.x, 0);
        }
        else
        {
            float panelLength = 1325.748f;
            completedQuestText.rectTransform.offsetMin = new
                Vector2(completedQuestText.rectTransform.offsetMin.x, -795.7479f + panelLength - textLength - 4);
        }
    }

    public void CloseQuestboard()
    {
        playerItems.disableToolbar = false;
        playerMovement.enabled = true;
        completedQuestPanel.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
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

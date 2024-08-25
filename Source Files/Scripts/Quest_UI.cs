using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Quest_UI : MonoBehaviour
{
    public GameObject questPanel;

    private TextMeshProUGUI activeQuests;

    private PlayerQuests playerQuests;
    private PlayerItems playerItems;
    private PlayerMovement playerMovement;
    private PlayerTutorial playerTutorial;

    public List<QuestSlot_UI> questSlots = new List<QuestSlot_UI>();

    private Dictionary<string, int> tempDict = new Dictionary<string, int>();
    
    private void Start()
    {
        playerQuests = GameObject.Find("Player").GetComponent<PlayerQuests>();
        playerItems = GameObject.Find("Player").GetComponent<PlayerItems>();
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        playerTutorial = GameObject.Find("Player").GetComponent<PlayerTutorial>();

        activeQuests = GameObject.Find("ActiveQuests").GetComponent<TextMeshProUGUI>();

        foreach (string completedQuest in playerQuests.completedQuestNames)
        {
            tempDict.Add(completedQuest, 0);
        }
    }

    void Update()
    {
        ActiveQuestSetup();
        if (!playerItems.disableToolbar && Input.GetKeyDown(KeyCode.Q))
        {
            activeQuests.gameObject.SetActive(false);
            questPanel.SetActive(true);
            playerItems.disableToolbar = true;
            playerMovement.enabled = false;
            Setup();
        }
        else if (playerItems.disableToolbar && questPanel.activeSelf && (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.Escape)))
        {
            HideUI();
        }
    }

    public void HideUI()
    {
        activeQuests.gameObject.SetActive(true);
        questPanel.SetActive(false);
        playerItems.disableToolbar = false;
        playerMovement.enabled = true;
    }

    void Setup()
    {
        for (int i = 0; i < questSlots.Count; i++)
        {
            if (playerQuests.questList.questSlots[i].count == 1)
            {
                questSlots[i].SetQuest(playerQuests.questList.questSlots[i]);
            }
            else
            {
                questSlots[i].SetEmpty();
            }
        }
    }

    void ActiveQuestSetup()
    {
        if (playerQuests.endingProgress == 1||playerQuests.endingProgress == 2 
            || playerQuests.endingProgress == 5 || playerQuests.endingProgress == 6)
        {
            activeQuests.text = "";
            return;
        }
        else if (playerTutorial.tutorialProgress >= 3)
        {
            activeQuests.text = "Active Quests:\n";
        }
        string tempQuests = "";
        for (int i = 0; i < questSlots.Count; i++)
        {
            if (playerQuests.questList.questSlots[i].count == 1)
            {
                questSlots[i].QuestHandler(playerQuests.questList.questSlots[i]);
                tempQuests += playerQuests.questList.questSlots[i].questName + " - ";
                if (!playerQuests.questList.questSlots[i].done)
                {
                    tempQuests += "Not ";
                }
                else if (!tempDict.ContainsKey(playerQuests.questList.questSlots[i].questName))
                {
                    tempDict.Add(playerQuests.questList.questSlots[i].questName, 0);
                    playerQuests.completedQuestNames.Add(playerQuests.questList.questSlots[i].questName);
                    playerQuests.completedQuestDescs.Add(playerQuests.questList.questSlots[i].questDescription);
                }
                tempQuests += "Done\n";
            }
        }
        activeQuests.text += tempQuests;
    }
}

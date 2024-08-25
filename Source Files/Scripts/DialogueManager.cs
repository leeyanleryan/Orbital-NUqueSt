using Ink.Parsed;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Ink.Runtime;
using Story = Ink.Runtime.Story;
using Choice = Ink.Runtime.Choice;
using UnityEngine.EventSystems;
using UnityEditor;
using JetBrains.Annotations;
using UnityEngine.SceneManagement;
//using Ink.UnityIntegration;

public class DialogueManager : MonoBehaviour, IDataPersistence
{
    [Header("Dialogue UI")]

    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private GameObject dialogueButton;

    [Header("Load Globals JSON")]
    [SerializeField] private TextAsset loadGlobalsJSON;

    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;

    private Story currentStory;
    private bool dialogueIsPlaying;

    private static DialogueManager instance;

    public PlayerMovement playerMovement;
    private PlayerItems playerItems;

    private DialogueVariables dialogueVariables;
    private PlayerQuests player;

    public Inventory inventory;
    public Inventory toolbar;

    public bool openShop;

    private bool choosingOption;

    public string localNPCName;
    private PlayerMoney playerMoney;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Found more than one Dialogue Manager in the scene");
            return;
        }
        instance = this;
        dialogueVariables = new DialogueVariables(loadGlobalsJSON);
    }

    public static DialogueManager GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        playerItems = GameObject.Find("Player").GetComponent<PlayerItems>();
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;

        foreach (GameObject choice in choices)
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
        inventory = GameObject.Find("Player").GetComponent<PlayerItems>().inventory;
        toolbar = GameObject.Find("Player").GetComponent<PlayerItems>().toolbar;
        playerMoney = GameObject.Find("Player").GetComponent<PlayerMoney>();

    }

    private void Update()
    {
        if (dialogueIsPlaying)
        {
            if (Input.GetKeyDown(KeyCode.Space) && !choosingOption)
            {
                ContinueStory();
            }
        }
    }
    
    /*
     * @brief To change ink variables based on the day setting and whether a quest is done.
     */
    public void CheckDate()
    {
        float currDay = float.Parse(currentStory.variablesState[localNPCName + "Day"].ToString());
        string questIsDone = currentStory.variablesState[localNPCName + "QuestDone"].ToString();
        
        // happens if player did not finish a quest across multiple days
        if (GameManager.instance.day != currDay && (questIsDone == "false" || questIsDone == "False"))
        {
            dialogueVariables.InkSetVariables(currentStory, localNPCName + "Day", GameManager.instance.day);
            dialogueVariables.InkSetVariables(currentStory, localNPCName + "ValidTime", true);
        }
        // happens if u finished a quest and its on the same day
        // the reason for not setting valid time to false is because it will be done when you hand over the quest
        else if (GameManager.instance.day == currDay && (questIsDone == "True" || questIsDone == "true"))
        {
        }
        // happens when it is different day and you did not hand over the quest previously
        else if (GameManager.instance.day != currDay && (questIsDone == "True" || questIsDone == "true"))
        {
            dialogueVariables.InkSetVariables(currentStory, localNPCName + "Day", GameManager.instance.day);
            dialogueVariables.InkSetVariables(currentStory, localNPCName + "ValidTime", true);
            dialogueVariables.InkSetVariables(currentStory, localNPCName + "QuestDone", false);
        }
        // happens if it is the same day and you have not finished a quest 
        // if you have not started on a quest, questIsDone will be true
        else if (GameManager.instance.day == currDay && (questIsDone == "false" || questIsDone == "False"))
        {
        }
    }

    public void EnterDialogueMode(TextAsset inkJSON)
    {
        currentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);
        playerMovement.enabled = false;
        playerItems.disableToolbar = true;
        dialogueVariables.StartListening(currentStory);
        player = GameObject.Find("Player").GetComponent<PlayerQuests>();
        if (localNPCName != "")
        {
            CheckDate();
            currentStory.BindExternalFunction("QuestCompleted", QuestCompleted);
            for (int i = 0; i < 6; i++)
            {
                // if the active quest of the NPC you are talking to is the same as any of the ones present in the quest UI
                if (player.questList.questSlots[i].questName == currentStory.variablesState[localNPCName + "QuestName"].ToString()
                    && player.questList.questSlots[i].questName != "")
                {
                    string questSTARTEDLOLOL = currentStory.variablesState[localNPCName + "QuestStarted"].ToString();
                    //to make questStarted false and questDone true when quest is completed
                    if (player.questList.questSlots[i].questName != "" && QuestIsDone(i) && (questSTARTEDLOLOL != "false" && questSTARTEDLOLOL != "False"))
                    {
                        // finished the quest 
                        dialogueVariables.InkSetVariables(currentStory, localNPCName + "QuestDone", true);
                        dialogueVariables.InkSetVariables(currentStory, localNPCName + "QuestStarted", false);
                        dialogueVariables.InkSetVariables(currentStory, localNPCName + "ValidTime", true);
                    }
                    else if (!QuestIsDone(i))
                    {
                        // quest is not finished while having it
                        dialogueVariables.InkSetVariables(currentStory, localNPCName + "QuestDone", false);
                        dialogueVariables.InkSetVariables(currentStory, localNPCName + "QuestStarted", true);
                        dialogueVariables.InkSetVariables(currentStory, localNPCName + "ValidTime", true);
                    }
                }
            }
        }
        else
        {
            currentStory.BindExternalFunction("QuestCompleted", QuestCompleted);
        }
        ContinueStory();
    }

    /*
     * @brief to remove the quest from the quest UI.
     * 
     * If the active quest of the NPC you are talking to has the same name is one of the quest in the quest UI, remove the quest from the questUI and handle the logic.
     * for what will happen when a quest is completed. For example, the GPA is reward to player.
     */
    public void QuestCompleted()
    {
        for (int i = 0; i < 6; i++)
        {
            if (player.questList.questSlots[i].questName == currentStory.variablesState[localNPCName + "QuestName"].ToString()
                && player.questList.questSlots[i].questName != "")
            {
                player.questList.RemoveItemFromPlayer(player.questList.questSlots[i].questItemRequired, 
                    player.questList.questSlots[i].questItemAmount);
                playerMoney.money += (int)player.questList.questSlots[i].gpaReward;
                player.questList.questSlots[i].RemoveInfo();
            }
        }
    }

    private void ExitDialogueMode()
    {
        dialogueVariables.StopListening(currentStory);
        currentStory.UnbindExternalFunction("QuestCompleted");

        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
        playerItems.disableToolbar = false;
        playerMovement.enabled = true;

        // save the variable states in a place holder string when you leave the dialogue
        DataPersistenceManager.instance.gameData.placeHolderStory = dialogueVariables.saveVariables();
    }

    /*
     * @brief to check if a quest is done.
     * @param x It is the index of a slot in the quest UI.
     * @return Returns true if the quest is completed.
     */
    private bool QuestIsDone(int x)
    {
        player = GameObject.Find("Player").GetComponent<PlayerQuests>();
        if (player.questList.questSlots[x].done && player.questList.questSlots[x].questName != "")
        {
            return true;
        }
        return false;
    }

    public void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            string currentLine = currentStory.Continue();
            dialogueText.text = currentLine;
            if (currentLine.Length < 2)
            {
                ContinueStory();
            }
            else if (currentLine.Contains("Great!") && localNPCName != "")
            {
                // referencing dictionary in DialogueVariables script which references variables from globals.ink file
                string questName = currentStory.variablesState[localNPCName + "QuestName"].ToString();
                string questDescription = currentStory.variablesState[localNPCName + "QuestDesc"].ToString();

                // add quest to Quest List under Player Quests component in Player via QuestList script
                player = GameObject.Find("Player").GetComponent<PlayerQuests>();
                player.questList.Add(questName, questDescription);
            }
            else if (currentLine.Contains("Sure. This is what we have in stock."))
            {
                openShop = true;
            }
            else if (currentLine.Contains("scroll") && currentLine.Contains("chest") && currentLine.Contains("house") && localNPCName != "")
            {
                player = GameObject.Find("Player").GetComponent<PlayerQuests>();
                player.questScrollNames.Add(localNPCName);
            }
            DisplayChoices();
        }
        else
        {
            ExitDialogueMode();
        }
    }

    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;

        if (currentChoices.Count > choices.Length)
        {
            Debug.LogError("More choices were given than the UI can support. Number of choices given: " + currentChoices.Count);
        }

        int index = 0;
        bool hasChoice = false;

        foreach (Choice choice in currentChoices)
        {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
            hasChoice = true;
        }

        for (int i = index; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }

        if (hasChoice)
        {
            choosingOption = true;
            dialogueButton.SetActive(false);
            EventSystem.current.SetSelectedGameObject(null);
        }
    }

    public void MakeChoice(int choiceIndex)
    {
        currentStory.ChooseChoiceIndex(choiceIndex);
        choosingOption = false;
        dialogueButton.SetActive(true);
        ContinueStory();
    }
    
    public void SaveData(GameData data)
    {
    }

    public void LoadData(GameData data)
    {
    }
}

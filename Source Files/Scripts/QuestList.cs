using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class QuestList
{
    [System.Serializable]
    public class QuestSlot
    {
        public int count;
        public string questName;
        public string questNPCName;
        public string questDescription;
        public string questSceneName;
        public bool done;
        public float gpaReward;

        public bool testing;

        // add more requirements here for different quests
        public int slimesRequired;
        public string questItemRequired;
        public int questItemAmount;
        public List<string> placesToVisit = new List<string>();

        public void AddQuest(string quest_name, string quest_description)
        {
            questName = quest_name;
            questDescription = quest_description;
            questSceneName = SceneManager.GetActiveScene().name;
            QuestHandler(quest_name);
            count++;
        }

        // add more quests here
        public void QuestHandler(string quest_name)
        {
            if (quest_name == "MA1511")
            {
                GameObject.Find("Player").GetComponent<PlayerQuests>().ma1511Progress = 0;
                GameObject.Find("MA1511Collider").SetActive(false);
                gpaReward = 10;
            }
            if (quest_name == "MA1512")
            {
                GameObject.Find("Player").GetComponent<PlayerQuests>().ma1512Progress = 0;
                GameObject.Find("MA1512Collider").SetActive(false);
                gpaReward = 10;
            }
            if (quest_name == "MA1508E")
            {
                GameObject.Find("Player").GetComponent<PlayerQuests>().ma1508EProgress = 0;
                gpaReward = 20;
            }
            if (quest_name == "HSA1000")
            {
                placesToVisit.Add("Cave_1");
                gpaReward = 20;
            }
            if (quest_name == "GESS1001")
            {
                placesToVisit.Add("Cave_1b");
                gpaReward = 20;
            }
            if (quest_name == "GEA1000")
            {
                placesToVisit.Add("DCave_2a");
                gpaReward = 30;
            }
            if (quest_name == "PC1101")
            {
                placesToVisit.Add("Village");
                gpaReward = 10;
            }
            if (quest_name == "PC1201")
            {
                gpaReward = 20;
            }
            if (quest_name == "CS1010")
            {
                gpaReward = 40;
            }
            if (quest_name == "CS1231")
            {
                gpaReward = 40;
            }
            if (quest_name == "CS2030")
            {
                gpaReward = 40;
            }
            if (quest_name == "CS2040")
            {
                gpaReward = 50;
            }
            if (quest_name == "CG1111A")
            {
                gpaReward = 30;
            }
            if (quest_name == "CG2111A")
            {
                GameObject.Find("Player").GetComponent<PlayerQuests>().cg2111aProgress = 0;
                gpaReward = 40;
            }
            if (quest_name == "EG1311")
            {
                GameObject.Find("Player").GetComponent<PlayerQuests>().eg1311Progress = 0;
                gpaReward = 30;
            }
            if (quest_name == "DTK1234")
            {
                GameObject.Find("Player").GetComponent<PlayerQuests>().dtk1234Collected[0] = 1;
                questItemRequired = "Leather Piece";
                questItemAmount = 3;
                gpaReward = 10;
            }
            if (quest_name == "HSI1000")
            {
                placesToVisit.Add("Desert");
                placesToVisit.Add("Cave_2a");
                placesToVisit.Add("NorthForest");
                gpaReward = 30;
            }
            if (quest_name == "HSS1000")
            {
                placesToVisit.Add("ArtistHouse");
                gpaReward = 10;
            }
            if (testing == false)
            {
                questNPCName = DialogueManager.GetInstance().localNPCName;
            }
        }

        public void RemoveInfo()
        {
            count = 0;
            questNPCName = "";
            questName = "";
            questDescription = "";
            questSceneName = "";
            done = false;
            gpaReward = 0;
            questItemRequired = "";
            questItemAmount = 0;
        }
    }

    public List<QuestSlot> questSlots = new List<QuestSlot>();

    public QuestList(int numSlots)
    {
        for (int i = 0; i < numSlots; i++)
        {
            QuestSlot questSlot = new QuestSlot();
            questSlots.Add(questSlot);
        }
    }

    public void Add(string questName, string questDescription)
    {
        foreach (QuestSlot questSlot in questSlots)
        {
            if (questSlot.count == 0)
            {
                questSlot.AddQuest(questName, questDescription);
                return;
            }
        }
    }

    public void RemoveItemFromPlayer(string itemName, int amountToRemove)
    {
        if (itemName == "")
        {
            return;
        }

        Inventory inventory = GameObject.Find("Player").GetComponent<PlayerItems>().inventory;
        Inventory toolbar = GameObject.Find("Player").GetComponent<PlayerItems>().toolbar;
        for (int i = 0; i < 21; i++)
        {
            if (inventory.slots[i].itemName == itemName)
            {
                inventory.Remove(i, amountToRemove);
                GameObject.Find("Inventory").GetComponent<Inventory_UI>().Refresh();
                return;
            }
        }
        for (int i = 0; i < 7; i++)
        {
            if (toolbar.slots[i].itemName == itemName)
            {
                toolbar.Remove(i, amountToRemove);
                GameObject.Find("Inventory").GetComponent<Inventory_UI>().Refresh();
                return;
            }
        }
    }
}

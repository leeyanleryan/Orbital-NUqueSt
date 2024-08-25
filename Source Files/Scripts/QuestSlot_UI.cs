using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class QuestSlot_UI : MonoBehaviour
{
    public TextMeshProUGUI questNameText;
    public TextMeshProUGUI questDescriptionText;
    public Image questNPCImage;
    public Scrollbar scrollbar;
    public GameObject questStatus;

    public void SetQuest(QuestList.QuestSlot questSlot)
    {
        if (questSlot != null)
        {
            questDescriptionText.rectTransform.offsetMin = new
                    Vector2(questDescriptionText.rectTransform.offsetMin.x, -330.06f);

            questNameText.text = questSlot.questName;
            questDescriptionText.text = questSlot.questDescription;
            questNPCImage.sprite = Resources.Load<Sprite>("Quest/" + questSlot.questNPCName);
            questNPCImage.color = new Color(1, 1, 1, 1);

            if (questSlot.done)
            {
                questStatus.SetActive(true);
                questDescriptionText.text = "Report back to the villager that gave the quest!";
            }

            questDescriptionText.text += "\n\nGPA Reward: " + questSlot.gpaReward;

            LayoutRebuilder.ForceRebuildLayoutImmediate(questDescriptionText.rectTransform);
            Canvas.ForceUpdateCanvases();
            scrollbar.value = 1f;
            float textLength = questDescriptionText.textBounds.size.y;
            if (textLength <= 225)
            {
                questDescriptionText.rectTransform.offsetMin = new
                Vector2(questDescriptionText.rectTransform.offsetMin.x, 0);
            }
            else
            {
                float panelLength = 550.06f;
                questDescriptionText.rectTransform.offsetMin = new
                    Vector2(questDescriptionText.rectTransform.offsetMin.x, -330.06f + panelLength - textLength - 4);
            }
        }
    }

    public void SetEmpty()
    {
        questDescriptionText.rectTransform.offsetMin = new
                    Vector2(questDescriptionText.rectTransform.offsetMin.x, 0f);
        questNameText.text = "";
        questDescriptionText.text = "";
        questNPCImage.sprite = null;
        questNPCImage.color = new Color(1, 1, 1, 0);
        questStatus.SetActive(false);
    }

    // add quests completion requirement here
    public void QuestHandler(QuestList.QuestSlot questSlot)
    {
        if (questSlot.questName == "HSA1000" || questSlot.questName == "PC1101" || questSlot.questName == "HSI1000"
            || questSlot.questName == "HSS1000" || questSlot.questName == "GEA1000" || questSlot.questName == "GESS1001")
        {
            if (questSlot.placesToVisit.Count == 0)
            {
                questSlot.done = true;
            }
        }
        if (questSlot.questName == "DTK1234")
        {
            Inventory inventory = GameObject.Find("Player").GetComponent<PlayerItems>().inventory;
            Inventory toolbar = GameObject.Find("Player").GetComponent<PlayerItems>().toolbar;
            bool check = false;
            // for inventory
            for (int i = 0; i < 21; i++)
            {
                if (inventory.slots[i].itemName == questSlot.questItemRequired && inventory.slots[i].count >= questSlot.questItemAmount)
                {
                    if (questSlot.questName == "DTK1234")
                    {
                        PlayerQuests playerQuests = GameObject.Find("Player").GetComponent<PlayerQuests>();
                        playerQuests.dtk1234Collected[0] = 0;
                    }
                    questSlot.done = true;
                    check = true;
                    break;
                }
            }
            // for toolbar
            for (int i = 0; i < 7; i++)
            {
                if (toolbar.slots[i].itemName == questSlot.questItemRequired && toolbar.slots[i].count >= questSlot.questItemAmount)
                {
                    if (questSlot.questName == "DTK1234")
                    {
                        PlayerQuests playerQuests = GameObject.Find("Player").GetComponent<PlayerQuests>();
                        playerQuests.dtk1234Collected[0] = 0;
                    }
                    questSlot.done = true;
                    check = true;
                    break;
                }
            }
            if (!check)
            {
                questSlot.done = false;
            }
        }
    }
}

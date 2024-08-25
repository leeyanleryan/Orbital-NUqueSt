using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckQuestScroll : MonoBehaviour
{
    public string npcName;
    public GameObject chestTrigger;

    private bool hasChecked;

    private PlayerQuests playerQuests;

    void Start()
    {
        playerQuests = GameObject.Find("Player").GetComponent<PlayerQuests>();
    }

    void Update()
    {
        if (!hasChecked)
        {
            for (int i = 0; i < playerQuests.questScrollNames.Count; i++)
            {
                if (playerQuests.questScrollNames[i] == "")
                {
                    break;
                }
                else if (playerQuests.questScrollNames[i] == npcName)
                {
                    chestTrigger.SetActive(true);
                    hasChecked = true;
                    break;
                }
            }
        }
    }
}

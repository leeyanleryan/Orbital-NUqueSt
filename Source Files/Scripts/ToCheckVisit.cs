using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToCheckVisit : MonoBehaviour
{
    public string questName;
    private string currLocation;
    private PlayerQuests playerQuests;

    public GameObject dialoguePanel;
    public GameObject dialogueTrigger;
    public GameObject visualCue;
    private bool playerInRange;
    private int questIndex;

    void Start()
    {
        currLocation = SceneManager.GetActiveScene().name;
        playerQuests = GameObject.Find("Player").GetComponent<PlayerQuests>();
    }

    private void Update()
    {
        if (playerInRange && dialoguePanel.activeSelf && dialogueTrigger.activeSelf)
        {
            playerQuests.questList.questSlots[questIndex].placesToVisit.Remove(currLocation);
            dialogueTrigger.SetActive(false);
            visualCue.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            for (int i = 0; i < 6; i++)
            {
                if (playerQuests.questList.questSlots[i].questName == questName && playerQuests.questList.questSlots[i].done == false)
                {
                    dialogueTrigger.SetActive(true);
                    playerInRange = true;
                    questIndex = i;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}

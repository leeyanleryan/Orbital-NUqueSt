using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueTrigger : MonoBehaviour
{
    public string npcName;

    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    private bool playerInRange;

    private PlayerItems playerItems;

    private void Awake()
    {
        playerInRange = false;
        visualCue.SetActive(false);
        playerItems = GameObject.Find("Player").GetComponent<PlayerItems>();
    }
    
    private void Update()
    {
        if (playerInRange)
        {
            visualCue.SetActive(true);
            if (!playerItems.disableToolbar && Input.GetKeyDown(KeyCode.E))
            {
                DialogueManager.GetInstance().localNPCName = npcName;
                DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
            }
        }
        else
        {
            visualCue.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            playerInRange = false;
        }
    }
}

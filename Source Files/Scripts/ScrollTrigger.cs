using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollTrigger : MonoBehaviour
{
    [SerializeField] private GameObject scrollPanel;
    [SerializeField] private SpriteRenderer stoneSprite;
    [SerializeField] private GameObject visualCue;
    [SerializeField] private Scroll_UI scrollUI;

    private PlayerItems playerItems;
    private PlayerQuests playerQuests;
    private PlayerMovement playerMovement;

    private int stoneIndex;

    private bool playerInRange;

    void Start()
    {
        playerItems = GameObject.Find("Player").GetComponent<PlayerItems>();
        playerQuests = GameObject.Find("Player").GetComponent<PlayerQuests>();
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        scrollUI = scrollPanel.transform.parent.gameObject.GetComponent<Scroll_UI>();

        string name = gameObject.transform.parent.gameObject.name;
        stoneIndex = name[name.Length - 1] - 48;
    }

    void Update()
    {
        if (!scrollPanel.activeSelf && playerQuests.questScrollInserted[stoneIndex] == 1)
        {
            stoneSprite.sprite = Resources.Load<Sprite>("Quest/StoneHasScroll");
            gameObject.SetActive(false);
        }
        if (playerInRange)
        {
            if (!playerItems.disableToolbar && Input.GetKeyDown(KeyCode.E))
            {
                scrollUI.stoneIndex = stoneIndex;
                playerItems.disableToolbar = true;
                playerMovement.enabled = false;
                scrollPanel.SetActive(true);
            }
            else if (playerItems.disableToolbar && scrollPanel.activeSelf
                && (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Escape)))
            {
                scrollUI.CloseUI();
            }
        }
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

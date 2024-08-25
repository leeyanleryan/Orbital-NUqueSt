using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSignTrigger : MonoBehaviour
{
    public GameObject mapSignPanel;

    private PlayerItems playerItems;
    private PlayerMovement playerMovement;

    private bool playerInRange;
    public GameObject visualCue;

    private void Start()
    {
        playerItems = GameObject.Find("Player").GetComponent<PlayerItems>();
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (playerInRange)
        {
            MapSignUI();
        }
    }

    private void MapSignUI()
    {
        if (!playerItems.disableToolbar && !mapSignPanel.activeSelf && Input.GetKeyDown(KeyCode.E))
        {
            playerMovement.enabled = false;
            playerItems.disableToolbar = true;
            mapSignPanel.SetActive(true);
        }
        else if (playerItems.disableToolbar && mapSignPanel.activeSelf && 
            (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Escape)))
        {
            playerMovement.enabled = true;
            playerItems.disableToolbar = false;
            mapSignPanel.SetActive(false);
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

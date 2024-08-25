using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChestTrigger : MonoBehaviour
{
    public GameObject chestPanel;
    public GameObject inventoryPanel;
    public GameObject helpPanel;
    public GameObject slotBlocker;
    public GameObject dropPanel;
    public GameObject visualCue;

    private Inventory_UI inventoryInCanvas;
    private Inventory_UI chestInCanvas;
    private ChestItems chestItems;
    private PlayerItems playerItems;
    private PlayerMovement playerMovement;
    private GameObject inventoryNewPosition;
    private GameObject inventoryOriginPosition;

    private bool playerInRange;

    private void Start()
    {
        inventoryInCanvas = GameObject.Find("Inventory").GetComponent<Inventory_UI>();
        chestInCanvas = GameObject.Find("ChestInv").GetComponent<Inventory_UI>();
        chestItems = gameObject.transform.parent.gameObject.GetComponent<ChestItems>();
        playerItems = GameObject.Find("Player").GetComponent<PlayerItems>();
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        inventoryNewPosition = GameObject.Find("InventoryNewPosition");
        inventoryOriginPosition = GameObject.Find("InventoryOriginPosition");
    }

    private void Update()
    {
        if (playerInRange)
        {
            ToggleChestInventory();
        }
    }

    private void ToggleChestInventory()
    {
        if (!playerItems.disableToolbar && Input.GetKeyDown(KeyCode.E))
        {
            inventoryPanel.transform.position = inventoryNewPosition.transform.position;
            inventoryInCanvas.inventoryByName.Add(chestItems.chestName, chestItems.chestInventory);
            chestInCanvas.inventoryName = chestItems.chestName;
            chestPanel.SetActive(true);
            inventoryPanel.SetActive(true);
            playerItems.disableToolbar = true;
            inventoryInCanvas.Refresh();
            chestItems.ChestRefresh();
            playerMovement.enabled = false;
        }
        else if (playerItems.disableToolbar && chestPanel.activeSelf
            && (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Escape)))
        {
            inventoryPanel.transform.position = inventoryOriginPosition.transform.position;
            inventoryInCanvas.inventoryByName.Remove(chestItems.chestName);
            chestInCanvas.inventoryName = null;
            helpPanel.SetActive(false);
            dropPanel.SetActive(false);
            inventoryPanel.SetActive(false);
            chestPanel.SetActive(false);
            slotBlocker.SetActive(false);
            playerItems.disableToolbar = false;
            playerMovement.enabled = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            playerInRange = true;
            visualCue.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            playerInRange = false;
            visualCue.SetActive(false);
        }
    }
}

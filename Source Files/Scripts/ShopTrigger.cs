using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopTrigger : MonoBehaviour
{
    public GameObject shopPanel;
    public GameObject inventoryPanel;
    public GameObject helpPanel;
    public GameObject dropPanel;
    public GameObject shopAmountPanel;
    public GameObject slotBlocker;

    public GameObject dialoguePanel;

    private Inventory_UI inventoryInCanvas;
    private Inventory_UI shopInCanvas;
    private ShopItems shopItems;
    private PlayerItems playerItems;
    private PlayerMovement playerMovement;
    private GameObject inventoryNewPosition;
    private GameObject inventoryOriginPosition;

    private void Start()
    {
        inventoryInCanvas = GameObject.Find("Inventory").GetComponent<Inventory_UI>();
        shopInCanvas = GameObject.Find("Shop").GetComponent<Inventory_UI>();
        shopItems = gameObject.GetComponent<ShopItems>();
        playerItems = GameObject.Find("Player").GetComponent<PlayerItems>();
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        inventoryNewPosition = GameObject.Find("InventoryNewPosition");
        inventoryOriginPosition = GameObject.Find("InventoryOriginPosition");
    }

    private void Update()
    {
        if (DialogueManager.GetInstance() != null && DialogueManager.GetInstance().openShop && !dialoguePanel.activeSelf)
        {
            inventoryPanel.transform.position = inventoryNewPosition.transform.position;
            inventoryInCanvas.inventoryByName.Add(shopItems.shopName, shopItems.shopInventory);
            shopInCanvas.inventoryName = shopItems.shopName;
            inventoryInCanvas.Refresh();
            shopItems.ShopRefresh();
            shopPanel.SetActive(true);
            inventoryPanel.SetActive(true);
            playerItems.disableToolbar = true;
            playerMovement.enabled = false;
            DialogueManager.GetInstance().openShop = false;
        }
        else if (shopPanel.activeSelf && (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.E)))
        {
            inventoryPanel.transform.position = inventoryOriginPosition.transform.position;
            inventoryInCanvas.inventoryByName.Remove(shopItems.shopName);
            shopInCanvas.inventoryName = null;
            shopAmountPanel.SetActive(false);
            shopPanel.SetActive(false);
            helpPanel.SetActive(false);
            dropPanel.SetActive(false);
            inventoryPanel.SetActive(false);
            slotBlocker.SetActive(false);
            inventoryInCanvas.ItemDescDisable();
            playerItems.disableToolbar = false;
            playerMovement.enabled = true;
        }
    }
}

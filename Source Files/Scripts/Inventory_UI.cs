using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Inventory;
using UnityEngine.SceneManagement;

public class Inventory_UI : MonoBehaviour
{
    public GameObject inventoryPanel;

    public string inventoryName;

    public GameObject chestPanel;

    public GameObject slotBlocker;

    public List<Slot_UI> slots = new List<Slot_UI>();

    [Header("Item Description Components")]
    public TextMeshProUGUI itemNameText;
    public TextMeshProUGUI itemDescText;
    public Scrollbar itemDescScrollbar;
    public Button buyButton;
    public Button sellButton;
    public Button trashButton;
    public Button helpButton;

    [Header("Shop Components")]
    public GameObject shopPanel;
    public GameObject shopAmountPanel;
    public TextMeshProUGUI headerAmountText;
    public Button buyAmountButton;
    public Button sellAmountButton;
    public TMP_InputField shopAmountText;

    [Header("Drop Panel Components")]
    public GameObject dropPanel;
    public TMP_InputField dropText;

    [Header("Help Panel Components")]
    public GameObject helpPanel;
    public TextMeshProUGUI helpNameText;
    public Image helpImage;
    public TextMeshProUGUI helpDescText;
    public Scrollbar helpDescScrollbar;


    public Dictionary<string, Inventory> inventoryByName = new Dictionary<string, Inventory>();

    private Canvas canvas;

    private Slot_UI clickedSlot;

    private Slot_UI draggedSlot;
    private Image draggedIcon;

    private PlayerItems playerItems;
    private PlayerMovement playerMovement;

    private PlayerMoney playerMoney;

    private Inventory_UI inventoryInCanvas;
    private Inventory_UI toolbarInCanvas;
    private Inventory_UI chestInCanvas;
    private Inventory_UI shopInCanvas;

    private void Start()
    {
        canvas = FindObjectOfType<Canvas>();

        playerItems = GameObject.Find("Player").GetComponent<PlayerItems>();
        inventoryByName.Add("Inventory", playerItems.inventory);
        inventoryByName.Add("Toolbar", playerItems.toolbar);

        playerMoney = GameObject.Find("Player").GetComponent<PlayerMoney>();

        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();

        inventoryInCanvas = GameObject.Find("Inventory").GetComponent<Inventory_UI>();
        toolbarInCanvas = GameObject.Find("Toolbar").GetComponent<Inventory_UI>();
        chestInCanvas = GameObject.Find("ChestInv").GetComponent<Inventory_UI>();
        shopInCanvas = GameObject.Find("Shop").GetComponent<Inventory_UI>();

        SetupSlots();
        Refresh();
    }

    void Update()
    {
        if (inventoryPanel != null)
        {
            ToggleInventory();
        }
    }

    public void ToggleInventory()
    {
        if (!playerItems.disableToolbar && !shopPanel.activeSelf && !chestPanel.activeSelf && Input.GetKeyDown(KeyCode.Tab))
        {
            inventoryPanel.SetActive(true);
            playerItems.disableToolbar = true;
            playerMovement.enabled = false;
            Refresh();
        }
        else if (playerItems.disableToolbar && !shopPanel.activeSelf && !chestPanel.activeSelf 
            && (inventoryPanel.activeSelf || dropPanel.activeSelf) && (Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.Escape)))
        {
            helpPanel.SetActive(false);
            slotBlocker.SetActive(false);
            dropPanel.SetActive(false);
            inventoryPanel.SetActive(false);
            ItemDescDisable();
            playerItems.disableToolbar = false;
            playerMovement.enabled = true;
        }
    }

    /*
     * The first for loop is for setting up inventory. The second for loop is for setting up toolbar.
     * If the player's inventory/toolbar has white squares then that means the inventory/toolbar is not properly setup here.
     * This function is called whenever a player enters a new scene, or when the player opens the inventory by pressing TAB.
     * This Refresh needs to happen as there are two different inventories. One inventory is the inventory UI, and the other 
     * inventory is the player's actual inventory (in script). The Refresh will get the items from the player inventory and 
     * make it visible on the inventory UI. Same goes for toolbar. The third if statement is for refreshing chest UI.
     */
    public void Refresh()
    {
        for (int i = 0; i < inventoryInCanvas.slots.Count; i++)
        {
            if (playerItems.inventory.slots[i].itemName != "")
            {
                inventoryInCanvas.slots[i].SetItem(playerItems.inventory.slots[i]);
            }
            else
            {
                inventoryInCanvas.slots[i].SetEmpty();
            }
        }
        for (int i = 0; i < toolbarInCanvas.slots.Count; i++)
        {
            if (playerItems.toolbar.slots[i].itemName != "")
            {
                toolbarInCanvas.slots[i].SetItem(playerItems.toolbar.slots[i]);
            }
            else
            {
                toolbarInCanvas.slots[i].SetEmpty();
            }
        }
        if (chestPanel != null && chestPanel.activeSelf)
        {
            ChestItems chestItems = GameObject.Find(chestInCanvas.inventoryName).GetComponent<ChestItems>();
            chestItems.ChestRefresh();
        }
        if (shopPanel != null && shopPanel.activeSelf)
        {
            ShopItems shopItems = GameObject.Find(shopInCanvas.inventoryName).GetComponent<ShopItems>();
            shopItems.ShopRefresh();
        }
    }

    /*
     * @brief This function is called when a player clicks on the trash button in the item description panel. It opens up a panel confirming
     * whether the player wants to trash the item or not.
     */
    public void RemoveAmountUI()
    {
        if (clickedSlot != null)
        {
            Inventory fromInventory = inventoryByName[clickedSlot.inventoryName];
            Item itemToDrop = ItemManager.instance.GetItemByName(fromInventory.slots[clickedSlot.slotID].itemName);
            if (itemToDrop != null)
            {
                slotBlocker.SetActive(true);
                dropPanel.SetActive(true);
            }
        }
    }

    /*
     * @brief This function is called when player clicks OK on the trash panel. It handles the item to remove.
     */
    public void Remove()
    {
        Inventory fromInventory = inventoryByName[clickedSlot.inventoryName];
        string text = dropText.text;
        bool parseSuccess = int.TryParse(text.Trim(), out int amountToDrop);
        if (parseSuccess && amountToDrop <= fromInventory.slots[clickedSlot.slotID].count && amountToDrop >= 0)
        {
            fromInventory.Remove(clickedSlot.slotID, amountToDrop);
            Refresh();
        }
        ItemDescDisable();
        slotBlocker.SetActive(false);
        dropPanel.SetActive(false);
        clickedSlot = null;
    }

    /* 
     * @brief These SetTo functions refer to the triple up arrows and triple down arrows on the drop panel
     */ 
    public void SetToMax()
    {
        Inventory fromInventory = inventoryByName[clickedSlot.inventoryName];
        dropText.text = fromInventory.slots[clickedSlot.slotID].count.ToString();
    }

    public void SetToMin()
    {
        dropText.text = "0";
    }

    public void ShopSetToMax()
    {
        Inventory fromInventory = inventoryByName[clickedSlot.inventoryName];
        shopAmountText.text = fromInventory.slots[clickedSlot.slotID].count.ToString();
    }

    public void ShopSetToMin()
    {
        shopAmountText.text = "0";
    }

    public void ClosePanelButton()
    {
        dropPanel.SetActive(false);
        shopAmountPanel.SetActive(false);
        slotBlocker.SetActive(false);
        if (!inventoryPanel.activeSelf)
        {
            playerItems.disableToolbar = false;
            playerMovement.enabled = true;
        }
    }

    /*
     * The four functions below that start with "Slot" are Event Triggers found in every Slot
     * Each Slot has their own slotID, which is done by SetupSlots() function.
     */
    public void SlotBeginDrag(Slot_UI slot)
    {
        if (slot != null)
        {
            draggedSlot = slot;
            SlotClick(draggedSlot);
            draggedIcon = Instantiate(draggedSlot.itemIcon);
            draggedIcon.transform.SetParent(canvas.transform);
            draggedIcon.raycastTarget = false;
            draggedIcon.rectTransform.sizeDelta = new Vector2(50, 50);
            MoveToMousePosition(draggedIcon.gameObject);
        }
    }

    public void SlotDrag()
    {
        MoveToMousePosition(draggedIcon.gameObject);
    }

    public void SlotEndDrag()
    {
        Destroy(draggedIcon.gameObject);
        draggedIcon = null;
    }

    public void SlotDrop(Slot_UI slot)
    {
        if (draggedSlot != null)
        {
            Inventory fromInventory = inventoryByName[draggedSlot.inventoryName];
            Inventory toInventory = inventoryByName[slot.inventoryName];
            MoveSlot(draggedSlot.slotID, fromInventory, slot.slotID, toInventory);
            Refresh();
            SlotClick(slot);
        }
    }

    public void MoveSlot(int fromIndex, Inventory fromInventory, int toIndex, Inventory toInventory)
    {
        Slot fromSlot = fromInventory.slots[fromIndex];
        Slot toSlot = toInventory.slots[toIndex];
        int itemCount = fromSlot.count;
        Item fromSlotItem = ItemManager.instance.GetItemByName(fromSlot.itemName);
        if (toSlot.IsEmpty || toSlot.CanAddItem(fromSlot.itemName))
        {
            for (int i = 0; i < itemCount; i++)
            {
                toSlot.AddItem(fromSlotItem);
                fromSlot.RemoveItem();
            }
        }
    }

    // Item Description, Also used in Shop
    public void SlotClick(Slot_UI slot)
    {
        clickedSlot = slot;
        itemDescText.rectTransform.offsetMin = new Vector2(itemDescText.rectTransform.offsetMin.x, -160);
        if (clickedSlot.itemName != null)
        {
            itemNameText.text = clickedSlot.itemName;
            itemDescText.text = clickedSlot.itemDesc;
            itemDescScrollbar.interactable = true;
            if (clickedSlot.itemType != "General")
            {
                helpButton.interactable = true;
            }
            else
            {
                buyButton.interactable = false;
                sellButton.interactable = false;
                helpButton.interactable = false;
                trashButton.interactable = false;
            }
            if (shopPanel.activeSelf)
            {
                if (clickedSlot.inventoryName.Substring(0, 4) == "Shop")
                {
                    if (playerMoney.money >= clickedSlot.itemBuyCost)
                    {
                        buyButton.interactable = true;
                    }
                    else
                    {
                        buyButton.interactable = false;
                    }
                    sellButton.interactable = false;
                    trashButton.interactable = false;
                }
                else
                {
                    buyButton.interactable = false;
                    if (clickedSlot.itemType != "General")
                    {
                        sellButton.interactable = true;
                        trashButton.interactable = true;
                    }
                }
            }
            else
            {
                if (SceneManager.GetActiveScene().name != "IntroTutorial" && clickedSlot.itemType != "General")
                {
                    trashButton.interactable = true;
                }
            }
            itemDescText.text += "\n\nPer buy cost: ";
            if (clickedSlot.itemBuyCost != 0)
            {
                itemDescText.text += clickedSlot.itemBuyCost;
            }
            else
            {
                itemDescText.text += "-";
            }
            itemDescText.text += "\n\nPer sell price: ";
            if (clickedSlot.itemSellCost != 0)
            {
                itemDescText.text += clickedSlot.itemSellCost;
            }
            else
            {
                itemDescText.text += "-";
            }
            itemDescText.fontSize = 22;
            LayoutRebuilder.ForceRebuildLayoutImmediate(itemDescText.rectTransform);
            Canvas.ForceUpdateCanvases();
            itemDescScrollbar.value = 1f;
            float textLength = itemDescText.textBounds.size.y;
            float panelLength = 304.5788f;
            itemDescText.rectTransform.offsetMin = new 
                Vector2(itemDescText.rectTransform.offsetMin.x, -160 + panelLength - textLength - 4);
        }
        else
        {
            itemDescScrollbar.value = 1f;
            itemDescText.rectTransform.offsetMin = new Vector2(itemDescText.rectTransform.offsetMin.x, 0);
            ItemDescDisable();
        }
    }

    public void ItemDescDisable()
    {
        clickedSlot = null;
        buyButton.interactable = false;
        sellButton.interactable = false;
        trashButton.interactable = false;
        helpButton.interactable = false;
        itemDescScrollbar.interactable = false;
        itemNameText.text = null;
        itemDescText.text = null;
    }

    public void ClickedBuy()
    {
        Inventory fromShop = inventoryByName[clickedSlot.inventoryName];
        Item boughtItem = ItemManager.instance.GetItemByName(clickedSlot.itemName);
        if (boughtItem.data.maxAllowed == 1)
        {
            playerMoney.money -= clickedSlot.itemBuyCost;
            fromShop.Remove(clickedSlot.slotID);
            playerItems.inventory.Add(boughtItem);
            ItemDescDisable();
            Refresh();
        }
        else
        {
            headerAmountText.text = "Type amount to buy";
            buyAmountButton.gameObject.SetActive(true);
            sellAmountButton.gameObject.SetActive(false);
            slotBlocker.SetActive(true);
            shopAmountPanel.SetActive(true);
        }
    }

    public void BuyFromShop()
    {
        Inventory fromShop = inventoryByName[clickedSlot.inventoryName];
        Item itemToBuy = ItemManager.instance.GetItemByName(clickedSlot.itemName);
        string text = shopAmountText.text;
        bool parseSuccess = int.TryParse(text.Trim(), out int amountToBuy);
        if (parseSuccess && playerMoney.money >= amountToBuy * itemToBuy.data.itemBuyCost && amountToBuy >= 0
            && amountToBuy <= fromShop.slots[clickedSlot.slotID].count)
        {
            playerItems.inventory.Add(itemToBuy, amountToBuy);
            fromShop.Remove(clickedSlot.slotID, amountToBuy);
            playerMoney.money -= amountToBuy * itemToBuy.data.itemBuyCost;
            Refresh();
        }
        shopAmountPanel.SetActive(false);
        slotBlocker.SetActive(false);
        ItemDescDisable();
    }

    public void ClickedSell()
    {
        Inventory fromInventory = inventoryByName[clickedSlot.inventoryName];
        Item soldItem = ItemManager.instance.GetItemByName(clickedSlot.itemName);
        if (soldItem.data.maxAllowed == 1)
        {
            playerMoney.money += clickedSlot.itemBuyCost;
            fromInventory.Remove(clickedSlot.slotID);
            ItemDescDisable();
            Refresh();
        }
        else
        {
            headerAmountText.text = "Type amount to sell";
            buyAmountButton.gameObject.SetActive(false);
            sellAmountButton.gameObject.SetActive(true);
            slotBlocker.SetActive(true);
            shopAmountPanel.SetActive(true);
        }
    }

    public void ClickedHelp()
    {
        helpDescText.rectTransform.offsetMin = new Vector2(helpDescText.rectTransform.offsetMin.x, -380.4354f);
        helpPanel.SetActive(true);
        string itemType = clickedSlot.itemType;
        string usageStatement = "To use a " + itemType + ", drag the " + itemType + " into the toolbar at the bottom of the " +
            "screen. After placing it in your toolbar, click on the hotkey that the " + itemType + " was placed at. For " +
            "example, if the " + itemType + " was placed in the second slot, then press the 2 key on your keyboard to equip " +
            "\nthe " + itemType + ". Make sure you close your inventory first.";
        string strengthStatement = "From strongest to weakest: Diamond, Gold, Iron, Copper, Stone";
        if (itemType == "Pickaxe")
        {
            helpNameText.text = itemType + "s";
            helpImage.sprite = Resources.Load<Sprite>("Help/HelpPickaxe");
            helpDescText.text = "Pickaxes are used in mining rocks. Rocks can only be mined in the cave. Pickaxes of " +
                "different materials e.g. Stone vs Iron have different mining strengths and will break rocks faster if " +
                "it is made of a stronger material. Rocks mined will drop ores." +
                "\n\n" + strengthStatement +
                "\n\n" + usageStatement +
                "\n\nAfter doing the above, try left clicking near a rock to mine it.";
        }
        else if (itemType == "Sword")
        {
            helpNameText.text = itemType + "s";
            helpImage.sprite = Resources.Load<Sprite>("Help/HelpSword");
            helpDescText.text = "Swords are used in combat against enemies. Swords of different materials e.g. Stone vs Iron " +
                "have different hitting strengths and will kill enemies faster if it is made of a stronger material." +
                "\n\n" + strengthStatement +
                "\n\n" + usageStatement +
                "\n\nAfter doing the above, try left clicking near enemies to attack them.";
        }
        else if (itemType == "Hoe")
        {
            helpNameText.text = itemType + "s";
            helpImage.sprite = Resources.Load<Sprite>("Help/HelpHoe");
            helpDescText.text = "Hoes are used in tilling grass and harvesting crops. Hoes can only be used outside your house. " +
                "It will not work in any other places. Hoes of different materials e.g. Stone vs Iron have different " +
                "radiuses in how far the tool can reach and will reach further if it is made of a stronger material." +
                "\n\n" + strengthStatement +
                "\n\n" + usageStatement + 
                "\n\nAfter doing the above, try left clicking grass close to you to till them, and right click on fully grown " +
                "crops to harvest them. They are fully grown if the tile is highlighted in green.";
        }
        else if (itemType == "Seed")
        { 
            helpNameText.text = itemType + "s";
            helpImage.sprite = Resources.Load<Sprite>("Help/HelpHoe");
            helpDescText.text = "Seeds are used in growing crops. Seeds can only be grown on tilled grass outside your house. " +
                "It will not work in any other places." +
                "\n\n" + usageStatement + 
                "\n\nAfter doing the above, try left clicking the seed on a tilled grass to plant it.";
        }
        else if (itemType == "Ore")
        {
            helpNameText.text = itemType + "s";
            helpImage.sprite = Resources.Load<Sprite>("Help/HelpPickaxe");
            helpDescText.text = "Ores are obtained from mining rocks. Its main purpose is to provide a source of income via GPA " +
                "by selling the ores at shops. Ores can only be obtained from the rocks in caves." +
                "\n\nOres do not have a usage unlike other items. Putting it in your toolbar and left clicking or " +
                "right clicking does not do anything to the ore." +
                "\n\nWe were gonna make a crafting system but had no time :(";
        }
        else if (itemType == "Food")
        {
            helpNameText.text = itemType;
            helpImage.sprite = Resources.Load<Sprite>("Help/HelpHoe");
            helpDescText.text = "Food is used to regenerate health. Food can only be obtained from growing crops. These crops " +
                "come from seeds planted on grass tilled by a hoe." +
                "\n\n" + usageStatement + 
                "\n\nAfter doing the above, try left clicking when you're damaged to heal for a portion of your health.";
        }
        helpDescText.fontSize = 22;
        LayoutRebuilder.ForceRebuildLayoutImmediate(helpDescText.rectTransform);
        Canvas.ForceUpdateCanvases();
        helpDescScrollbar.value = 1f;
        float textLength = helpDescText.textBounds.size.y;
        helpDescText.rectTransform.offsetMin = new
            Vector2(helpDescText.rectTransform.offsetMin.x, -354.2301f + 494.2294f - textLength - 4);
    }

    public void CloseHelp()
    {
        helpNameText.text = null;
        helpDescScrollbar.value = 1f;
        helpPanel.SetActive(false);
    }

    public void SellFromInventory()
    {
        Item itemToSell = ItemManager.instance.GetItemByName(clickedSlot.itemName);
        Inventory fromInventory = inventoryByName[clickedSlot.inventoryName];
        string text = shopAmountText.text;
        bool parseSuccess = int.TryParse(text.Trim(), out int amountToSell);
        if (parseSuccess && fromInventory.slots[clickedSlot.slotID].count >= amountToSell && amountToSell >= 0)
        {
            fromInventory.Remove(clickedSlot.slotID, amountToSell);
            playerMoney.money += amountToSell * itemToSell.data.itemSellCost;
            Refresh();
        }
        shopAmountPanel.SetActive(false);
        slotBlocker.SetActive(false);
        ItemDescDisable();
    }

    /*
     * This function makes the image of the item being dragged to follow the cursor
     */
    private void MoveToMousePosition(GameObject toMove)
    {
        if(canvas != null)
        {
            Vector2 position;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform,
                Input.mousePosition, null, out position);

            toMove.transform.position = canvas.transform.TransformPoint(position);
        }
    }

    /*
     * This handles the slotID of the inventory and toolbar slots. slotID is used in dragging items. If there is an error in dragging
     * items, then something might be wrong in assigning the inventoryName of each slots. It may be null.
     */
    void SetupSlots()
    {
        int counter = 0;
        foreach (Slot_UI slot in slots)
        {
            slot.inventoryName = inventoryName;
            slot.slotID = counter;
            counter++;
        }
    }
}

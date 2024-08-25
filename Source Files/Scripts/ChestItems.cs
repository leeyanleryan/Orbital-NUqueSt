using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestItems : MonoBehaviour, IDataPersistence
{
    public string chestName;
    public Inventory chestInventory;

    public PlayerPositionSO startingPosition;

    private Inventory_UI chestInCanvas;

    private bool hasAddedToChest;

    private void Start()
    {
        gameObject.name = chestName;
        chestInCanvas = GameObject.Find("ChestInv").GetComponent<Inventory_UI>();
        if (startingPosition.transittedScene || startingPosition.playerDead)
        {
            hasAddedToChest = GameManager.instance.hasAddedToChest;
            if (!hasAddedToChest)
            {
                GameManager.instance.chest0 = new Inventory("Chest0", 21);
                GameManager.instance.chest1 = new Inventory("Chest1", 21);
                GameManager.instance.chest2 = new Inventory("Chest2", 21);
                GameManager.instance.chest3 = new Inventory("Chest3", 21);
                GameManager.instance.chest4 = new Inventory("Chest4", 21);
                GameManager.instance.chest5 = new Inventory("Chest5", 21);
                GameManager.instance.chest6 = new Inventory("Chest6", 21);
                GameManager.instance.chest7 = new Inventory("Chest7", 21);
                GameManager.instance.chest8 = new Inventory("Chest8", 21);
                GameManager.instance.chestList = new List<Inventory> 
                { 
                    GameManager.instance.chest0,
                    GameManager.instance.chest1,
                    GameManager.instance.chest2,
                    GameManager.instance.chest3,
                    GameManager.instance.chest4,
                    GameManager.instance.chest5,
                    GameManager.instance.chest6,
                    GameManager.instance.chest7,
                    GameManager.instance.chest8
                };
                GameManager.instance.chest0.Add(ItemManager.instance.GetItemByName("Stone Hoe"));
                GameManager.instance.chest0.Add(ItemManager.instance.GetItemByName("Tomato Seed"), 10);
                GameManager.instance.chest0.Add(ItemManager.instance.GetItemByName("Potato Seed"), 10);
                GameManager.instance.chest1.Add(ItemManager.instance.GetItemByName("Copper Sword"));
                GameManager.instance.chest2.Add(ItemManager.instance.GetItemByName("Stone Pickaxe"));
                GameManager.instance.chest3.Add(ItemManager.instance.GetItemByName("Scroll"));
                GameManager.instance.chest4.Add(ItemManager.instance.GetItemByName("Scroll"));
                GameManager.instance.chest5.Add(ItemManager.instance.GetItemByName("Scroll"));
                GameManager.instance.chest6.Add(ItemManager.instance.GetItemByName("Scroll"));
                GameManager.instance.chest7.Add(ItemManager.instance.GetItemByName("Scroll"));
                GameManager.instance.chest8.Add(ItemManager.instance.GetItemByName("Scroll"));
                hasAddedToChest = true;
            }
            chestInventory = new Inventory(chestName, 21);
            if (int.TryParse(chestName.Substring(chestName.Length - 1), out int lastDigit))
            {
                chestInventory = GameManager.instance.chestList[lastDigit];
            }
        }
    }

    private void Update()
    {
        GameManager.instance.hasAddedToChest = hasAddedToChest;
        if (int.TryParse(chestName.Substring(chestName.Length - 1), out int lastDigit))
        {
            GameManager.instance.chestList[lastDigit] = chestInventory;
        }
    }

    public void ChestRefresh()
    {
        for (int i = 0; i < chestInCanvas.slots.Count; i++)
        {
            chestInCanvas.slots[i].inventoryName = chestName;
            if (chestInventory.slots[i].itemName != "")
            {
                chestInCanvas.slots[i].SetItem(chestInventory.slots[i]);
            }
            else
            {
                chestInCanvas.slots[i].SetEmpty();
            }
        }
    }

    public void LoadData(GameData data)
    {
        hasAddedToChest = data.hasAddedToChest;
        chestInventory = new Inventory(chestName, 21);
        if (int.TryParse(chestName.Substring(chestName.Length - 1), out int lastDigit))
        {
            chestInventory = data.chestList[lastDigit];
        }
        foreach (Inventory.Slot slot in chestInventory.slots)
        {
            string itemName = slot.itemName;
            string directory = "";
            if (slot.itemType == "Seed" || slot.itemType == "Food")
            {
                directory = "Farming/";
            }
            else if (slot.itemType == "Ore")
            {
                directory = "Ores/";
            }
            else if (slot.itemType == "Sword" || slot.itemType == "Pickaxe" || slot.itemType == "Hoe")
            {
                directory = "Weapons/";
            }
            else if (slot.itemType == "General")
            {
                directory = "General/";
            }
            slot.icon = Resources.Load<Sprite>(directory + itemName.Replace(" ", "_"));
        }
    }

    public void SaveData(GameData data)
    {
        data.hasAddedToChest = hasAddedToChest;
        if (int.TryParse(chestName.Substring(chestName.Length - 1), out int lastDigit))
        {
            data.chestList[lastDigit] = chestInventory;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItems : MonoBehaviour, IDataPersistence
{
    public string shopName;
    public Inventory shopInventory;

    public PlayerPositionSO startingPosition;

    private Inventory_UI shopInCanvas;

    private bool dayChecker;

    private void Start()
    {
        gameObject.name = shopName;
        shopInCanvas = GameObject.Find("Shop").GetComponent<Inventory_UI>();
        if (startingPosition.transittedScene || startingPosition.playerDead)
        {
            // shop0: blacksmith
            // shop1: generalshop
            shopInventory = new Inventory(shopName, 21);
            UpdateShop();
        }
    }

    private void Update()
    {
        if (GameManager.instance.hours == 8f && GameManager.instance.minutes == 0f && !dayChecker)
        {
            EmptyShop(GameManager.instance.shop0);
            EmptyShop(GameManager.instance.shop1);
            ShopRestock();
            UpdateShop();
            dayChecker = true;
        }
        if (int.TryParse(shopName.Substring(shopName.Length - 1), out int lastDigit))
        {
            GameManager.instance.shopList[lastDigit] = shopInventory;
        }
    }
    
    private void EmptyShop(Inventory shop)
    {
        for (int i = 0; i < 21; i++)
        {
            if (shop.slots[i] == null)
            {
                break;
            }
            else
            {
                shop.Remove(i, shop.slots[i].count);
            }
        }
    }

    private void ShopRestock()
    {
        GameManager.instance.shop0.Add(ItemManager.instance.GetItemByName("Copper Hoe"));
        GameManager.instance.shop0.Add(ItemManager.instance.GetItemByName("Copper Sword"));
        GameManager.instance.shop0.Add(ItemManager.instance.GetItemByName("Copper Pickaxe"));
        GameManager.instance.shop0.Add(ItemManager.instance.GetItemByName("Iron Hoe"));
        GameManager.instance.shop0.Add(ItemManager.instance.GetItemByName("Iron Sword"));
        GameManager.instance.shop0.Add(ItemManager.instance.GetItemByName("Iron Pickaxe"));
        GameManager.instance.shop0.Add(ItemManager.instance.GetItemByName("Gold Hoe"));
        GameManager.instance.shop0.Add(ItemManager.instance.GetItemByName("Gold Sword"));
        GameManager.instance.shop0.Add(ItemManager.instance.GetItemByName("Gold Pickaxe"));
        GameManager.instance.shop0.Add(ItemManager.instance.GetItemByName("Diamond Hoe"));
        GameManager.instance.shop0.Add(ItemManager.instance.GetItemByName("Diamond Sword"));
        GameManager.instance.shop0.Add(ItemManager.instance.GetItemByName("Diamond Pickaxe"));
        GameManager.instance.shop1.Add(ItemManager.instance.GetItemByName("Tomato Seed"), 10);
        GameManager.instance.shop1.Add(ItemManager.instance.GetItemByName("Potato Seed"), 10);
    }

    private void UpdateShop()
    {
        if (int.TryParse(shopName.Substring(shopName.Length - 1), out int lastDigit))
        {
            shopInventory = GameManager.instance.shopList[lastDigit];
        }
    }

    public void ShopRefresh()
    {
        for (int i = 0; i < shopInCanvas.slots.Count; i++)
        {
            shopInCanvas.slots[i].inventoryName = shopName;
            if (shopInventory.slots[i].itemName != "")
            {
                shopInCanvas.slots[i].SetItem(shopInventory.slots[i]);
            }
            else
            {
                shopInCanvas.slots[i].SetEmpty();
            }
        }
    }

    public void LoadData(GameData data)
    {
        shopInventory = new Inventory(shopName, 21);
        if (int.TryParse(shopName.Substring(shopName.Length - 1), out int lastDigit))
        {
            shopInventory = data.shopList[lastDigit];
        }
        foreach (Inventory.Slot slot in shopInventory.slots)
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
        if (int.TryParse(shopName.Substring(shopName.Length - 1), out int lastDigit))
        {
            data.shopList[lastDigit] = shopInventory;
        }
    }
}

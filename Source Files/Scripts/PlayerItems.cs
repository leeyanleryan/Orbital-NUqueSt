using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Inventory;

public class PlayerItems : MonoBehaviour, IDataPersistence
{
    public bool disableToolbar;
    public Inventory inventory;
    public Inventory toolbar;

    public PlayerPositionSO startingPosition;

    private void Start()
    {
        if (startingPosition.transittedScene || startingPosition.playerDead)
        {
            inventory = new Inventory("Inventory", 21);
            inventory = GameManager.instance.inventory;
            toolbar = new Inventory("Toolbar", 7);
            toolbar = GameManager.instance.toolbar;
        }
    }

    private void Update()
    {
        GameManager.instance.inventory = inventory;
        GameManager.instance.toolbar = toolbar;
    }

    public void LoadData(GameData data)
    {
        inventory = new Inventory("Inventory", 21);
        toolbar = new Inventory("Toolbar", 7);
        inventory = data.inventory;
        toolbar = data.toolbar;
        foreach (Slot slot in inventory.slots)
        {
            LoadItemSprite(slot);
        }
        foreach (Slot slot in toolbar.slots)
        {
            LoadItemSprite(slot);
        }
    }

    public void LoadItemSprite(Slot slot)
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

    public void SaveData(GameData data)
    {
        data.inventory = inventory;
        data.toolbar = toolbar;
    }
}

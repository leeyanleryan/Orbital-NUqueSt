using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class Inventory
{
    [System.Serializable]
    public class Slot
    {
        public string itemType;
        public string itemName;
        public string itemDesc;
        public int itemBuyCost;
        public int itemSellCost;
        public int itemHealAmount;
        public float itemSwordDamage;
        public float itemPickaxeDamage;
        public int itemHoeRange;
        public int count;
        public int maxAllowed;
        public Sprite icon;
        public string iconName;
        public string inventoryName;

        public bool IsEmpty
        {
            get
            {
                if (itemName == "" && count == 0)
                {
                    return true;
                }
                return false;
            }
        }

        public Slot()
        {
            itemName = "";
            count = 0;
            maxAllowed = 32;
            icon = null;
            iconName = null;
        }

        public bool CanAddItem(string itemName)
        {
            if(this.itemName == itemName && count < maxAllowed)
            {
                return true;
            }
            return false;
        }

        public void AddItem(Item item)
        {
            this.itemType = item.data.itemType;
            this.itemName = item.data.itemName;
            this.itemDesc = item.data.itemDesc;
            this.itemBuyCost = item.data.itemBuyCost;
            this.itemSellCost = item.data.itemSellCost;
            this.itemHealAmount = item.data.itemHealAmount;
            this.itemSwordDamage = item.data.itemSwordDamage;
            this.itemPickaxeDamage = item.data.itemPickaxeDamage;
            this.itemHoeRange = item.data.itemHoeRange;
            this.maxAllowed = item.data.maxAllowed;
            this.icon = item.data.icon;
            count++;
        }

        public void RemoveItem()
        {
            if (count>0)
            {
                count--;

                if (count == 0)
                {
                    icon = null;
                    itemType = "";
                    itemName = "";
                    itemDesc = "";
                    itemBuyCost = 0;
                    itemSellCost = 0;
                    itemHealAmount = 0;
                    itemSwordDamage = 0;
                    itemPickaxeDamage = 0;
                    itemHoeRange = 0;
                    maxAllowed = 32;
                }
            }
        }

        public void AfterDeserialization(string iconNAME)
        {
            string path = "Prefab/" + iconName;
            icon = Resources.Load<Sprite>(iconNAME);
        }

    }

    public List<Slot> slots = new List<Slot>();

    public Inventory(string inventoryName, int numSlots)
    {
        for(int i = 0; i < numSlots; i++)
        {
            Slot slot = new Slot();
            slot.inventoryName = inventoryName;
            slots.Add(slot);
        }
    }

    // To fix the collectable edge bug, made this function return bool instead of void.
    // If the item has been added to the inventory, then it returns true. If not, then returns false.
    public bool Add(Item item)
    {
        foreach(Slot slot in slots)
        {
            if(slot.itemName == item.data.itemName && slot.CanAddItem(item.data.itemName))
            {
                slot.AddItem(item);
                return true;
            }
        }

        foreach(Slot slot in slots)
        {
            if(slot.itemName == "")
            {
                slot.AddItem(item);
                return true;
            }
        }

        return false;
    }

    public void Add(Item item, int numToAdd)
    {
        for (int i = 0; i < numToAdd; i++)
        {
            Add(item);
        }
    }

    public void Remove(int index)
    {
        slots[index].RemoveItem();
    }

    public void Remove(int index, int numToRemove)
    {
        if (slots[index].count >= numToRemove)
        {
            for (int i = 0; i < numToRemove; i++)
            {
                Remove(index);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class Slot_UI : MonoBehaviour
{
    public string inventoryName;
    public int slotID;
    public Image itemIcon;
    public TextMeshProUGUI quantityText;

    public string itemType;
    public string itemName;
    public string itemDesc;
    public int itemBuyCost;
    public int itemSellCost;
    public int itemHealAmount;
    public float itemSwordDamage;
    public float itemPickaxeDamage;
    public int itemHoeRange;

    [SerializeField] private GameObject highlight;

    /*
     * SetItem updates the sprite and quantity text in the UI by matching it to the corresponding slot in the player's inventory
     */
    public void SetItem(Inventory.Slot slot)
    {
        if(slot != null)
        {
            itemType = slot.itemType;
            itemName = slot.itemName; 
            itemDesc = slot.itemDesc;
            itemBuyCost = slot.itemBuyCost;
            itemSellCost = slot.itemSellCost;
            itemHealAmount = slot.itemHealAmount;
            itemSwordDamage = slot.itemSwordDamage;
            itemPickaxeDamage = slot.itemPickaxeDamage;
            itemHoeRange = slot.itemHoeRange;
            itemIcon.sprite = slot.icon;
            itemIcon.color = new Color(1, 1, 1, 1);
            if (slot.maxAllowed == 1)
            {
                quantityText.text = "";
            }
            else
            {
                quantityText.text = slot.count.ToString();
            }
        }
    }

    public void SetEmpty()
    {
        itemType = null;
        itemName = null;
        itemDesc = null;
        itemBuyCost = 0;
        itemSellCost = 0;
        itemHealAmount = 0;
        itemPickaxeDamage = 0;
        itemHoeRange = 0;
        itemIcon.sprite = null;
        itemIcon.color = new Color(1, 1, 1, 0);
        quantityText.text = "";
    }

    public void SetHighlight(bool isOn)
    {
        highlight.SetActive(isOn);
    }
}

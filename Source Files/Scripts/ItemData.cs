using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item Data", menuName = "Item Data", order = 50)]
public class ItemData : ScriptableObject
{
    public string itemType = "Item Type";
    public string itemName = "Item Name";
    public string itemDesc = "Item Description";
    public int itemBuyCost = 0;
    public int itemSellCost = 0;
    public int itemHealAmount = 0;
    public float itemSwordDamage = 0;
    public float itemPickaxeDamage = 0;
    public int itemHoeRange = 0;
    public int maxAllowed;
    public Sprite icon;
}

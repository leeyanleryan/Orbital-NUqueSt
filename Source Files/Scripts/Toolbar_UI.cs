using PlayFab.EconomyModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;
using static Inventory;

public class Toolbar_UI : MonoBehaviour
{
    [SerializeField] private List<Slot_UI> toolbarSlots = new List<Slot_UI>();

    private Slot_UI selectedSlot;

    private GameObject player;
    private PlayerItems playerItems;
    private PlayerMovement playerMovement;
    private Health playerHealth;
    private SwordAttack swordAttack;

    private Vector3 mousePosition;

    private void Start()
    {
        player = GameObject.Find("Player");
        playerItems = player.GetComponent<PlayerItems>();
        playerMovement = player.GetComponent<PlayerMovement>();
        playerHealth = GameObject.Find("PlayerHitBox").GetComponent<Health>();
        swordAttack = player.GetComponent<SwordAttack>();
        SelectSlot(0);
    }

    private void Update()
    {
        if (!playerItems.disableToolbar)
        {
            CheckAlphaNumericKeys();
            HoldItemFromToolbar(selectedSlot.slotID);
            CheckItemUse();
        }
    }

    public void SelectSlot(int index)
    {
        if(toolbarSlots.Count == 7)
        {
            if (selectedSlot != null)
            {
                selectedSlot.SetHighlight(false);
            }
            selectedSlot = toolbarSlots[index];
            selectedSlot.SetHighlight(true);
        }
    }

    private void CheckAlphaNumericKeys()
    {
        for (int i = 0; i < toolbarSlots.Count; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                SelectSlot(i);
            }
        }
    }

    private void CheckItemUse()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            LeftClickItemFromToolbar(selectedSlot.slotID);
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            RightClickItemFromToolbar(selectedSlot.slotID);
        }
    }

    void Refresh()
    {
        for (int i = 0; i < gameObject.GetComponent<Inventory_UI>().slots.Count; i++)
        {
            if (playerItems.toolbar.slots[i].itemName != "")
            {
                gameObject.GetComponent<Inventory_UI>().slots[i].SetItem(playerItems.toolbar.slots[i]);
            }
            else
            {
                gameObject.GetComponent<Inventory_UI>().slots[i].SetEmpty();
            }
        }
    }

    private void HoldItemFromToolbar(int index)
    {
        Inventory.Slot slot = playerItems.toolbar.slots[index];
        if (!slot.IsEmpty)
        {
            if (slot.itemType == "Hoe")
            {
                HightlightTilemap(slot.itemHoeRange, slot.itemType);
            }
            else if (slot.itemType == "Seed")
            {
                HightlightTilemap(1, slot.itemType);
            }
        }
        else
        {
            RemoveHighlightTilemap();
        }
    }

    private void HightlightTilemap(int maxReach, string highlightType)
    {
        if (TileManager.instance != null)
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            TileManager.instance.HighlightTilemap(mousePosition, maxReach, highlightType);
        }
    }

    private void RemoveHighlightTilemap()
    {
        if (TileManager.instance != null)
        {
            TileManager.instance.RemoveHighlightTilemap();
        }
    }

    private void LeftClickItemFromToolbar(int index)
    {
        Inventory.Slot slot = playerItems.toolbar.slots[index];
        if (!slot.IsEmpty)
        {
            if (slot.itemType == "Food")
            {
                EatFood(index, slot.itemHealAmount);
            }
            else if (slot.itemType == "Seed")
            {
                PlantSeed(index, 1, slot.itemName);
            }
            else if (slot.itemType == "Sword" || slot.itemType == "Pickaxe")
            {
                SwingTool(slot.itemSwordDamage, slot.itemPickaxeDamage, slot.itemType, slot.itemName.Split(" ")[0]);
            }
            else if (slot.itemType == "Hoe")
            {
                SwingTool(slot.itemSwordDamage, slot.itemPickaxeDamage, slot.itemType, slot.itemName.Split(" ")[0]);
                UseHoeAddDirt();
            }
            Refresh();
        }
    }

    private void EatFood(int index, int amountToHeal)
    {
        if (playerHealth.health != playerHealth.maxHealth)
        {
            playerHealth.health = Math.Min(playerHealth.health + amountToHeal, playerHealth.maxHealth);
            playerItems.toolbar.Remove(index, 1);
        }
    }

    private void PlantSeed(int index, float hoursToGrow, string seedName)
    {
        if (TileManager.instance != null)
        {
            if (TileManager.instance.PlantSeed(mousePosition, GameManager.instance.day, hoursToGrow, seedName))
            {
                playerItems.toolbar.Remove(index, 1);
            }
        }
    }

    private void SwingTool(float swordDamage, float pickaxeDamage, string itemType, string itemRarity)
    {
        swordAttack.swordDamage = swordDamage;
        swordAttack.pickaxeDamage = pickaxeDamage;
        playerMovement.AnimateToolAttack(itemType, itemRarity);
    }

    private void UseHoeAddDirt()
    {
        if (TileManager.instance != null)
        {
            TileManager.instance.UseHoeAddDirt(mousePosition);
        }
    }

    private void RightClickItemFromToolbar(int index)
    {
        Inventory.Slot slot = playerItems.toolbar.slots[index];
        if (!slot.IsEmpty)
        {
            if (slot.itemType == "Hoe")
            {
                SwingTool(slot.itemSwordDamage, slot.itemPickaxeDamage, slot.itemType, slot.itemName.Split(" ")[0]);
                UseHoeRemoveDirt(slot.itemHoeRange);
            }
            Refresh();
        }
    }

    private void UseHoeRemoveDirt(int maxReach)
    {
        if (TileManager.instance != null)
        {
            TileManager.instance.UseHoeRemoveDirt(mousePosition, maxReach);
        }
    }
}

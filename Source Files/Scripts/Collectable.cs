using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Item))]
public class Collectable : MonoBehaviour
{
    // gameObject gets destroyed only if the item has been added to the inventory or toolbar.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerItems playerItems = collision.GetComponent<PlayerItems>();

        if (playerItems)
        {
            Item item = GetComponent<Item>();

            if (item != null)
            {
                if (playerItems.inventory.Add(item))
                {
                    Destroy(this.gameObject);
                }
                else if (playerItems.toolbar.Add(item))
                {
                    GameObject.Find("Inventory").GetComponent<Inventory_UI>().Refresh();
                    Destroy(this.gameObject);
                }
            }
        }
    }
}
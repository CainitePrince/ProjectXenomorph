using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public GameObject Item;

    private bool triggered = false;

    void OnCollisionEnter(Collision col)
    {
        // Prevent running multiple times.
        if (triggered) return;

        var go = col.gameObject;
        var faction = go.GetComponentInParent<AssignedFaction>();
        
        if (faction.Faction == Faction.Player || faction.Faction == Faction.Pirate)
        {
            triggered = true;

            var inventory = go.GetComponentInParent<Inventory>();
            inventory.Items.Add(Item);
            Destroy(gameObject);
        }
    }
}

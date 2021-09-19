using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : Collectable
{
    public Item item;

    protected override void OnCollect()
    {
        Pickup();
    }

    void Pickup()
    {
        Debug.Log("picking " + item.name);
        bool wasPickedUp = Inventory.instance.Add(item);
        if(wasPickedUp)

            Destroy(gameObject);
    }
}

using System;
using UnityEngine;

public class ItemPickup : Interactable
{
    public Item item;
    private Equipment equipment;

    private bool wasPickedUp;

    public override void Interact()
    {
        base.Interact();

        PickUp();
    }

    void PickUp()
    {
        CheckTypeOfItem();
        if (wasPickedUp)
        {
            Destroy(gameObject);    
        }            
    }

    private void CheckTypeOfItem()
    {
        if (item.isEquipment == false)
        {
            wasPickedUp = Inventory.instance.Add(item);
        }
        else
        {
            wasPickedUp = EquipmentInventory.instance.Add(item);
        }       
    }

}

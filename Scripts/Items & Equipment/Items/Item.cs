using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    new public string name = "New Item";
    public string description = "No item description available!";
    public Sprite icon = null;
    public bool isDefaultItem = false;
    public bool isEquipment = false;
    
    public virtual void Use()
    {
        //use the item

    }
    public virtual void Effect(Vector3 hit)
    {
        //use the item effect
    }

    public void RemoveFromInventory()
    {
        if (this.isEquipment == false)
        {
            Inventory.instance.Remove(this);
        }
        else
        {
           EquipmentInventory.instance.Remove(this);
        }
    }
}

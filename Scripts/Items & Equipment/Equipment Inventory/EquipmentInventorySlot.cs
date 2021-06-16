using UnityEngine;
using UnityEngine.UI;

public class EquipmentInventorySlot : MonoBehaviour
{
    [SerializeField] Item item;
    public Image icon;

    public void AddItem(Item newItem)
    {
        if (newItem != null)
        {
            item = newItem;
            icon.sprite = item.icon;
            icon.enabled = true;
        }    
    }

    public void ClearSlot ()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;

    }

    public void OnRemoveButton()
    {
        EquipmentInventory.instance.Remove(item);
    }

    public void UseItem()
    {
        if(item != null)
        {
            item.Use();
        }
    }
}

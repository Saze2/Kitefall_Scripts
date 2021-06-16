using UnityEngine;

public class CurrentEquipmentUI : MonoBehaviour
{
    public GameObject equipmentInventoryUI;
    public Transform itemsParent;
    EquipmentManager equipmentManager;
    EquipmentInventory equipmentInventory;
    CurrentEquipmentSlot[] slots;

    void Start()
    {
        equipmentManager = EquipmentManager.instance;
        equipmentInventory = EquipmentInventory.instance;
        slots = itemsParent.GetComponentsInChildren<CurrentEquipmentSlot>();


    }
 
    //Updating UI

  /*
    public void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].CheckSlotForItem() == true)
            {
                slots[i].AddItem(equipmentManager.currentEquipment[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
  */
    

    public void AddIcon(Equipment newItem, int index)
    {
        slots[index].AddItem(newItem);
    }

    public void RemoveIcon(int index)
    {
        slots[index].ClearSlot();
    }
}
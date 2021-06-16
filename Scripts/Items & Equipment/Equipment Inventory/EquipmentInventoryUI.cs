using System.Collections;
using UnityEngine;

public class EquipmentInventoryUI : MonoBehaviour
{
    public Transform itemsParent;
    EquipmentInventory equipmentInventory;
    EquipmentInventorySlot[] slots;

    // Start is called before the first frame update
    void Start()
    {
        equipmentInventory = EquipmentInventory.instance;
        equipmentInventory.onItemchangedCallback += UpdateUI;
        

        //searches for slots (if not static this needs to be in the update functioni
        slots = itemsParent.GetComponentsInChildren<EquipmentInventorySlot>();

    }


    //Updating UI
    public void UpdateUI()
    {
        for (int i = 0; i<slots.Length; i++)
        {
            if (i < equipmentInventory.items.Count)
            {
                slots[i].AddItem(equipmentInventory.items[i]);
            }
            else
            {
                slots[i].ClearSlot();   
            }
        }
    

        //UnityEngine.Debug.Log("UPDATING Equipment UI");
    }
    
}

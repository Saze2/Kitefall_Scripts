using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class EquipmentManager : MonoBehaviour
{
    #region Singleton
    public static EquipmentManager instance;
    public Equipment[] currentEquipment;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        int numSlots = 6;
        currentEquipment = new Equipment[numSlots];
    }

    #endregion

    
    EquipmentInventory inventory;
    CurrentEquipmentUI currentEquipmentUI;
   

     public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
     public OnEquipmentChanged onEquipmentChanged;

    
    public int _oldIndexSlot;
    public int _newIndexSlot;

    void Start()
    {
        //int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;        
        inventory = EquipmentInventory.instance;
        currentEquipmentUI = InventoryUserInterface.instance.currentEquipmentUI;
    }

    public void Equip(Equipment newItem)
    {
        // get index
        //int slotIndex = (int)newItem.equipmentSlot;
        int[] array = newItem.GetIndexSlotArray(newItem);
        int slotIndex = 0;

        foreach (int i in array)
        {
            if (currentEquipment[i] == null)
            {
                slotIndex = i;
                break;
            }
        }
          
        Equipment oldItem = null;

        if (currentEquipment[slotIndex] != null)
        {
            oldItem = currentEquipment[slotIndex];
            inventory.Add(oldItem);
        }

        if (onEquipmentChanged != null)
        {
            onEquipmentChanged.Invoke(newItem, oldItem);
            currentEquipmentUI.AddIcon(newItem, slotIndex);
        }
        currentEquipment[slotIndex] = newItem;
    }

    public void Unequip(int slotIndex)
    {
        if (currentEquipment[slotIndex] == null) return;       
        currentEquipmentUI.RemoveIcon(slotIndex);       
        Equipment oldItem = currentEquipment[slotIndex];
        inventory.Add(oldItem);
       
        currentEquipment[slotIndex] = null;     
        if (onEquipmentChanged != null)
        {
            onEquipmentChanged.Invoke(null, oldItem);
        }
    }

    public void UnequipAll()
    {
        for (int i = 0; i < currentEquipment.Length; i++)
        {
            Unequip(i);
        }
    }

    public void MoveEquipment ()
    {
        //get the item in the slot
        Equipment newItem = currentEquipment[_oldIndexSlot];
        int[] possibleEquipmentSlotsForType = newItem.GetIndexSlotArray(newItem);
        if (possibleEquipmentSlotsForType.Contains(_newIndexSlot) == false) return;

        currentEquipment[_oldIndexSlot] = null;
        currentEquipmentUI.RemoveIcon(_oldIndexSlot);

        if (currentEquipment[_newIndexSlot] != null)
        {
            Equipment oldItem = currentEquipment[_newIndexSlot];
            currentEquipmentUI.RemoveIcon(_newIndexSlot);

            currentEquipment[_oldIndexSlot] = oldItem;
            currentEquipmentUI.AddIcon(oldItem, _oldIndexSlot);
        }

        currentEquipment[_newIndexSlot] = newItem;
        currentEquipmentUI.AddIcon(newItem, _newIndexSlot);

    }
}


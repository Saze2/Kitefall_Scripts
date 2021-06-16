using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUserInterface : MonoBehaviour
{
    public static InventoryUserInterface instance;

    public InventoryUI inventory;
    public EquipmentInventoryUI equipmentInventory;
    public CurrentEquipmentUI currentEquipmentUI;
    public GameObject inventoryCanvas;
        
    public CurrentEquipmentSlot equipmentSlot1;
    public CurrentEquipmentSlot equipmentSlot2;
    public CurrentEquipmentSlot equipmentSlot3;
    public CurrentEquipmentSlot equipmentSlot4;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            //Debug.LogWarning("More than 1 instance of inventoryUI found!");
        }

        
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }
}

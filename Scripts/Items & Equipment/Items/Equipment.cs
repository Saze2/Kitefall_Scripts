using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item
{
    public EquipmentSlot equipmentSlot;
   
    public int armorModifier;
    public int damageModifier;
    public int rangeModifier;
    public int attackSpeedModifier;
    public int healthRegenModifier;
    public int cooldown = 0;
    public int speedBurstModifier;

    public override void Use()
    {
        base.Use();

        EquipmentManager.instance.Equip(this);

        RemoveFromInventory();
    }

    public int[] GetIndexSlotArray(Equipment newItem)
    {
        int[] indexArray;


        switch ((int)newItem.equipmentSlot)
        {
            case 0:
                {
                    indexArray = new int[] { 0, 1 };
                    break;
                }

            case 1:
                {
                    indexArray = new int[] { 2, 3, 4, 5 };                   
                    break;
                }

            default:
                {
                    indexArray = new int[] { 0 };
                    break;
                }

        }
        return indexArray;
    }

       
}
public enum EquipmentSlot { Passive, Ability}
//public enum EquipmentSlot { Passive, Ability1, Ablitly2, Ability3, Ability4 }
//public enum EquipmentSlot { Passive1, Passive2, Ability1, Ablitly2, Ability3, Ability4}


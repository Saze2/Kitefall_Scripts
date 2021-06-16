using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityBarIcon : MonoBehaviour
{
    [SerializeField] private int slotIndex = 0;
    private Image icon;

    private void Awake()
    {
        icon = GetComponent<Image>();
    }
    void Start()
    {
       
        if (slotIndex >= 0 && slotIndex < EquipmentManager.instance.currentEquipment.Length)
        {
            if (EquipmentManager.instance.currentEquipment[slotIndex] == null) return;
            icon.sprite = EquipmentManager.instance.currentEquipment[slotIndex].icon;
        }

    }

}

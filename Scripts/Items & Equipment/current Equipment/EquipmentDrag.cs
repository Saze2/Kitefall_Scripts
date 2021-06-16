using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EquipmentDrag : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    CurrentEquipmentSlot slot;
    EquipmentManager man;
    private RectTransform iconRectTransform;
    [SerializeField] Canvas canvas;
    private Vector2 _pos;


    private void Start()
    {
        slot = GetComponentInParent<CurrentEquipmentSlot>();
        man = EquipmentManager.instance;
        iconRectTransform = slot.icon.GetComponent<RectTransform>();
        _pos = iconRectTransform.anchoredPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {       
        if (slot.CheckSlotForItem() == false) return;
        man._oldIndexSlot = slot.GetSlotIndex();     
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (slot.CheckSlotForItem() == false) return;

        iconRectTransform.anchoredPosition += eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        iconRectTransform.anchoredPosition = _pos;
        if (slot.CheckSlotForItem() == false) return;
        man.MoveEquipment();
        
    }

    public void OnDrop(PointerEventData eventData)
    {       
        man._newIndexSlot = GetComponentInParent<CurrentEquipmentSlot>().GetSlotIndex();
        

    }

    //display item name and description

    public void OnPointerEnter(PointerEventData eventData)
    {
        //add a short delay?

        if (slot.GetItemName() == "empty") return;
        slot.nameText.SetText(slot.GetItemName());
        slot.descriptionText.SetText(slot.GetItemDescription());

        //move the info box
        slot.infoBox.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        slot.infoBox.SetActive(false);
        slot.nameText.SetText("Slot is empty!");
        slot.descriptionText.SetText("No item description available!");
    }
}
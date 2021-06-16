using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;


public class CurrentEquipmentSlot : MonoBehaviour
{
    private Equipment item;
    [SerializeField] private int slotIndex = 0;
    public Image icon;
    //public Image secondaryIcon;
    public Sprite defaultIcon;

    public GameObject infoBox;
    public TMP_Text nameText;
    public TMP_Text descriptionText;

    private void Awake()
    {
        if (CheckSlotForItem() == false) return;

    }

    public void AddItem(Equipment newItem)
    {
        if (newItem != null)
        {
            item = newItem;

            icon.sprite = item.icon;
            icon.enabled = true;

            //secondaryIcon.sprite = item.icon;
            PlayerManager.instance.secondaryIcon[slotIndex].sprite = item.icon;

        }

    }

    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;

        PlayerManager.instance.secondaryIcon[slotIndex].sprite = defaultIcon;
    }

    public void UseItem(Vector3 hitPoint)
    {
        if (item != null)
        {
            item.Effect(hitPoint);
        }
    }

    public void UnequipCurrentItem()
    {
        EquipmentManager.instance.Unequip(slotIndex);
    }

    public bool CheckSlotForItem()
    {
        if (item == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public int GetSlotIndex()
    {
        return slotIndex;
    }

    public string GetItemName()
    {
        if (item == null) return "empty";
        return item.name;
    }

    public string GetItemDescription()
    {
        if (item == null) return "No item description available!";
        return item.description;
    }


}

using System.Collections.Generic;
using UnityEngine;

public class EquipmentInventory : MonoBehaviour
{
    #region Singleton
    public static EquipmentInventory instance;
    public delegate void onItemchanged();
    public onItemchanged onItemchangedCallback;
    public int space = 24;
    public List<Item> items = new List<Item>();


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #endregion


    public bool Add (Item item)
    {
        if (!item.isDefaultItem)
        {
            if(items.Count >= space)
            {
                Debug.Log("Not enough room in the inventory");

                return false;
            }

            items.Add(item);

            if(onItemchangedCallback != null)
            {
                onItemchangedCallback.Invoke();
            }            
        }
            
        return true;
    }

    public void Remove(Item item)
    {
        items.Remove(item);
        
        if (onItemchangedCallback != null)
        {
            onItemchangedCallback.Invoke();
        }  
        
    }
}

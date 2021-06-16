using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singleton
    public static Inventory instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            //Debug.LogWarning("More than 1 instance of inventory found!");
        }      
    }

    #endregion

    public delegate void onItemchanged();
    public onItemchanged onItemchangedCallback;

    public int space = 20;

    public List<Item> items = new List<Item>();

    public bool Add (Item item)
    {
        if (!item.isDefaultItem)
        {
            if(items.Count >= space)
            {
                UnityEngine.Debug.Log("Not enough room in the inventory");
                //display it on screen?

                return false;
            }
            items.Add(item);

            if(onItemchangedCallback != null)
                onItemchangedCallback.Invoke();
        }
            
        return true;
    }

    public void Remove(Item item)
    {
        items.Remove(item);

        if (onItemchangedCallback != null)
            onItemchangedCallback.Invoke();
    }

}

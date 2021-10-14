using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    public List<Item> items = new List<Item>();
    public int space = 20;
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;


    private void Awake()
    {
        if (instance != null)
        {
            //Destroy(gameObject);
            Debug.Log("already created");
        }
        instance = this;
    }
    public bool Add(Item item)
    {
        if (!item.isDefaultItem)
        {
            if (items.Count >= space)
            {
                Debug.Log("not enpugh space");
                return false;
            }
            items.Add(item);
            if (onItemChangedCallback != null)
                onItemChangedCallback.Invoke();
        }
        return true;

    }
    public void Remove(Item item)
    {
        items.Remove(item);
        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }
    }
}

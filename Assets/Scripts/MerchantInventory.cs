using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchantInventory : MonoBehaviour
{
    public static MerchantInventory instance;
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

    public void Remove(Item item)
    {
        items.Remove(item);
        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    Inventory inventory;
    public Transform itemsParent;
    InventorySlot[] slots;
    public GameObject inventoryUI;
    //public static InventoryUI instance;
    // Start is called before the first frame update
    void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUI;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.I))
        {
            if (inventoryUI.activeSelf == true)
            {
                inventoryUI.SetActive(false);
            }
            else
                inventoryUI.SetActive(true);
        }
    }
    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if(i< inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);
                //Debug.Log("add");
            }
            else
            {
                slots[i].ClearSlot();
                //Debug.Log("clear");
            }

        }
    }
}

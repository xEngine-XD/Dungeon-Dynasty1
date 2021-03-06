using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    Inventory inventory;
    public Transform itemsParent;
    InventorySlot[] slots;
    public GameObject inventoryUI;
    public GameObject characterUI;


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
        if (GameManager.instance.player.canMove)
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
            if (Input.GetKeyDown(KeyCode.C))
            {
                if (characterUI.activeSelf == true)
                {
                    characterUI.SetActive(false);
                }
                else
                {
                    characterUI.SetActive(true);
                    characterUI.GetComponent<PlayerStatUI>().ShowStats();
                }
            }
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

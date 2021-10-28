using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EquipmentManager : MonoBehaviour
{
    public Equipment[] currentEquipment;
    public static EquipmentManager instance;
    Inventory inventory;

    public PlayerStatUI statUI;
    public InventorySlot[] playerEquipedItems;

    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
    public OnEquipmentChanged onEquipmentChanged;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;

    }
    void Start()
    {
        inventory = Inventory.instance;
        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new Equipment[numSlots];

    }

    public void Equip(Equipment newItem)
    {
        int slotIndex = (int)newItem.equipSlot;
        

        Equipment oldItem = null;
        if(currentEquipment[slotIndex] != null)
        {
            oldItem = currentEquipment[slotIndex];
            inventory.Add(oldItem);
        }
        if(onEquipmentChanged != null)
        {
            onEquipmentChanged.Invoke(newItem, oldItem);
            statUI.ShowStats();
        }
        currentEquipment[slotIndex] = newItem;
        playerEquipedItems[slotIndex].AddItem(newItem);
        GameManager.instance.sounds.ItemEquip();
        if(slotIndex == 3)
        {
            GameManager.instance.playerWeapon.GetComponent<SpriteRenderer>().sprite = newItem.icon;
        }
        
    }
    public void Unequip(int slotIndex)
    {
        if(currentEquipment[slotIndex] != null)
        {
            Equipment oldItem = currentEquipment[slotIndex];
            inventory.Add(oldItem);
            currentEquipment[slotIndex] = null;
            GameManager.instance.sounds.ItemEquip();
            playerEquipedItems[slotIndex].ClearSlot();
            if(slotIndex == 3)
            {
                GameManager.instance.playerWeapon.GetComponent<SpriteRenderer>().sprite = GameManager.instance.defaultWeapon;
            }
            if (onEquipmentChanged != null)
            {
                onEquipmentChanged.Invoke(null, oldItem);
                statUI.ShowStats();
            }
        }
    }
}

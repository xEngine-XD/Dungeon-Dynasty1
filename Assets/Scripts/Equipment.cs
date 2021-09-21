using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item
{
    public int armorModifier;
    public int damageModifier;
    public int poisonModifier;
    public int poisonDeflectModifier;
    public float piercemodifier;
    public EquipmentSlot equipSlot;
    public EquipmentType equipmentType;

    public override void Use()
    {
        //base.Use();
        EquipmentManager.instance.Equip(this);
        RemoveFromInventory();

    }
}
public enum EquipmentSlot
{
    Head,
    Chest,
    Legs,
    Weapon,
    Feet

}
public enum EquipmentType
{
    Piercing,
    Poisoning,
    Sharp,
    Magical

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item
{
    public float armorModifier;
    public float damageModifier;
    public float poisonModifier;
    public float poisonChance;
    public float poisonLength;
    public float poisonDeflectModifier;
    public float piercemodifier;
    public float criticalModifier;
    public float criticalMultiplier;
    public EquipmentSlot equipSlot;
    public EquipmentType equipmentType;

    public override void Use()
    {

        EquipmentManager.instance.Equip(this);
        Inventory.instance.Remove(this);

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

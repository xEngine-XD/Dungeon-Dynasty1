using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{

    // Start is called before the first frame update
    void Start()
    {
        EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged;
    }
    void OnEquipmentChanged(Equipment newItem, Equipment oldItem)
    {
        if (newItem != null)
        {
            armor.AddModifier(newItem.armorModifier);
            damage.AddModifier(newItem.damageModifier);
            poisonDamage.AddModifier(newItem.poisonModifier);
            poisonDeflect.AddModifier(newItem.poisonDeflectModifier);
        }
        if(oldItem != null)
        {
            armor.RemoveModifier(oldItem.armorModifier);
            damage.RemoveModifier(oldItem.damageModifier);
            poisonDamage.RemoveModifier(newItem.poisonModifier);
            poisonDeflect.RemoveModifier(newItem.poisonDeflectModifier);
        }
    }
    private void Update()
    {

    }

}

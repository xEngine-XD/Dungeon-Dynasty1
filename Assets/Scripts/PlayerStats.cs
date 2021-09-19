using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    //public int poisonResistance = 2;
    /*public List<StatEffectBase> effectModifiers = new List<StatEffectBase>();
    public void addEffect(StatEffectBase newEffect)
    {
        if (EffectCheck(newEffect.name) == false)
        {
            effectModifiers.Add(newEffect);
        }
    }
    public bool EffectCheck(string newEffect)
    {
        foreach(StatEffectBase effect in effectModifiers)
        {
            if(effect.name == newEffect)
            {
                return true;
            }
        }
        return false;
    }*/
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
        /*foreach (StatEffectBase effect in effectModifiers)
        {
            if(effect.callOnUpdate == true)
            {
                effect.onUpdate();
                
            }
        }*/
    }

}

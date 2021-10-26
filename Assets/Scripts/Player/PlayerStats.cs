using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    public Animator anim;
    public Stat criticalChance;
    public Stat criticalMultiplier;
    public bool isPoisoned = false;
    public bool poisonEffect;
    private float poisonTemp;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        poisonTemp = poisonLength.GetValue();
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
            poisonChance.AddModifier(newItem.poisonChance);
            poisonLength.AddModifier(newItem.poisonLength);
            criticalChance.AddModifier(newItem.criticalModifier);
            criticalMultiplier.AddModifier(newItem.criticalMultiplier);
            piercingChance.AddModifier(newItem.piercemodifier);
        }
        if(oldItem != null)
        {
            armor.RemoveModifier(oldItem.armorModifier);
            damage.RemoveModifier(oldItem.damageModifier);
            poisonDamage.RemoveModifier(oldItem.poisonModifier);
            poisonDeflect.RemoveModifier(oldItem.poisonDeflectModifier);
            poisonChance.RemoveModifier(oldItem.poisonChance);
            poisonLength.RemoveModifier(oldItem.poisonLength);
            criticalChance.RemoveModifier(oldItem.criticalModifier);
            criticalMultiplier.RemoveModifier(oldItem.criticalMultiplier);
            piercingChance.RemoveModifier(oldItem.piercemodifier);
        }
    }
    private void Update()
    {

    }
    public bool CritDamage()
    {
        float randomValue = Random.value;
        if (randomValue >= (1f - criticalChance.GetValue()))
        {
            return true;

        }
        else
            return false;
    }
    public bool PierceDamage()
    {
        float randomValue = Random.value;
        if (randomValue >= (1f - piercingChance.GetValue()))
        {
            return true;

        }
        else 
        {
            return false;

        }
    }
    public bool PoisonProc()
    {
        float randomValue = Random.value;
        if (randomValue >= (1f - poisonChance.GetValue()))
        {
            return true;

        }
        else 
        {

            return false;
        }
    }
    public override void Die()
    {
        GameManager.instance.sounds.PlayerDie();
        Time.timeScale = 0;
        GameManager.instance.dieWindow.SetActive(true);
    }
}

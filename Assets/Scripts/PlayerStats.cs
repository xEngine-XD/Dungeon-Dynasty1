using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    public Animator anim;
    private BoxCollider2D weaponBoxCollider;
    private Weapon weapon;
    public List<EnemyStats> enemiesCollided = new List<EnemyStats>();
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        weaponBoxCollider = FindObjectOfType<Weapon>().GetComponent<BoxCollider2D>();
        weapon = FindObjectOfType<Weapon>();
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
        Attack();
    }
    public void Attack()
    {
        if (Input.GetMouseButton(0))
        {
            anim.SetBool("attack", true);
            if(weapon.hit == true)
            {
                Debug.Log("enemy is hit");
            }
        }
        else if(!Input.GetMouseButtonUp(0))
            anim.SetBool("attack", false);
    }


}

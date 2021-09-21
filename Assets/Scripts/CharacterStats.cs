
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public Stat damage;
    public Stat armor;
    public Stat poisonDamage;
    public Stat poisonDeflect;
    public Stat magicDamage;
    public Stat magicResist;
    public Stat piersingChance;
    public float maxHealth;
    public float currentHealth { get; private set; }
    void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        damage -= armor.GetValue();
        damage = Mathf.Clamp(damage, 0, int.MaxValue);
        currentHealth -= damage;
        Debug.Log(transform.name + " takes " + damage + " damage");
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    public void TakePoisonDamage(float damage)
    {
        damage -= poisonDeflect.GetValue();
        damage = Mathf.Clamp(damage, 0, int.MaxValue);
        currentHealth -= damage;
        Debug.Log(transform.name + " takes " + damage + " damage");
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    public void TakePiercingDamage(float damage)
    {
        damage = Mathf.Clamp(damage, 0, int.MaxValue);
        currentHealth -= damage;
        Debug.Log(transform.name + " takes " + damage + " damage");
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    public void TakeMagicDamage(float damage)
    {
        damage -= magicResist.GetValue();
        damage = Mathf.Clamp(damage, 0, int.MaxValue);
        currentHealth -= damage;
        Debug.Log(transform.name + " takes " + damage + " damage");
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    public virtual void Die()
    {

    }
}

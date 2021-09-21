using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemies/Enemy")]

public class Enemy : ScriptableObject
{
    //enemy variables
    public float health;
    public float damage;
    public float moveSpeed;
    public float armor;
    //stats modifiers variables
        //attack speed 
    public float attackTime;
    public float attackRate;
        //poison modifiers
    public int poisonDebuffLength;
    public float poisonDamage;
    public float poisonResist;
        //pierce modifiers
    public float pierceChance;
        //magic modifiers
    public float magicDamage;
    public float magicResist;
    
    

    //enums 
    public EnemyType enemyType;
    public AttackModifiers attackModifiers;
    public Resistances resistances;
    public EnemyBehavior enemyBehavior;

    //attack distance for melee or range
    public float attackMeleeDistance;
    public float attackRangeDistance;

    public enum Resistances
    {
        Magical,
        Poisoning,
        None
    }
    public enum AttackModifiers
    {
        Piercing,
        Magical,
        Poisoning,
        None
    }
    public enum EnemyType
    {
        Range,
        Melee
    }
    public enum EnemyBehavior
    {
        Roam,
        Wait,
        Attack
    }


}

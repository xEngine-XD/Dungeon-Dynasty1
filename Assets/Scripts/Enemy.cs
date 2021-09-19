using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemies/Enemy")]

public class Enemy : ScriptableObject
{
    public int health;
    public float damage;
    public float moveSpeed;
    //public Player player;
    //public PlayerStats character;
    public EnemyType enemyType;
    public AttackModifiers attackModifiers;
    public Resistances resistances;


    public enum Resistances
    {
        Piercing,
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


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData 
{
    public int level;
    public float health;
    public float[] position;
    public float pDamage;
    public float pArmor;
    public float pPoisonDmg;
    public float pPoisonDeflect;
    public float pPoisonChance;
    public float pCritChance;
    public float pCritMul;
    public float pPiercing;
    public float pMagicDmg;
    public float pMagicResist;
   // public List<Equipment> invItems = new List<Equipment>();
    //public PlayerController player;
    //public PlayerStats stats;
    //public EquipmentManager equipment;
    //public Inventory inventory;


    public PlayerData(PlayerController player)//, Inventory inventory)
    {
        //stats
        health = GameManager.instance.playerStats.maxHealth;
        pDamage = GameManager.instance.playerStats.damage.GetValue();
        pArmor = GameManager.instance.playerStats.armor.GetValue();
        pPoisonDmg = GameManager.instance.playerStats.poisonDamage.GetValue();
        pPoisonDeflect = GameManager.instance.playerStats.poisonDeflect.GetValue();
        pPoisonChance = GameManager.instance.playerStats.poisonChance.GetValue();
        pCritChance = GameManager.instance.playerStats.criticalChance.GetValue();
        pCritMul = GameManager.instance.playerStats.criticalMultiplier.GetValue();
        pPiercing = GameManager.instance.playerStats.piercingChance.GetValue();
        pMagicDmg = GameManager.instance.playerStats.magicDamage.GetValue();
        pMagicResist = GameManager.instance.playerStats.magicDamage.GetValue();
        //position
        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = 0;
        

    }
}

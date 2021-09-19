using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonEffect : StatEffectBase
{
    PlayerStats player;
    float poisonTimer = 2.0f;
    int damage = 10;
    public float timeOfPoison = 5;
    private void Awake()
    {
        callOnUpdate = true;
        player = FindObjectOfType<PlayerStats>();
        name = "poison";
    }
    public override void onUpdate()
    {
        base.onUpdate();
        poisonTimer -= Time.deltaTime;
        if(poisonTimer<= 0)
        {
            //damage -= player.poisonResistance;
            player.TakeDamage(damage);
            poisonTimer = 2.0f;
            damage = 10;
        }
    }
}

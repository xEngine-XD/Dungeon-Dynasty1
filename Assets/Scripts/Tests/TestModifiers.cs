using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestModifiers : Collidable
{
    public PoisonEffect poison;
    public PlayerStats player;
    protected override void Start()
    {
        base.Start();
        if (poison == null)
        {
            poison = this.gameObject.AddComponent<PoisonEffect>();
        }
    }

    protected override void OnCollide(Collider2D coll)
    {
        //base.OnCollide(coll);
        if(coll.name == "Player")
        {
            Debug.Log("poisoned");
            //player.addEffect(poison);
        }
    }
}

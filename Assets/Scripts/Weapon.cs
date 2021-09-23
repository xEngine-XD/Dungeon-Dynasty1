using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Collidable
{
    public bool hit = false;
    protected override void OnCollide(Collider2D coll)
    {
        //base.OnCollide(coll);
        if(coll.name == "Enemy")
        {
            Debug.Log("hit");
            hit = true;
        }
    }
}

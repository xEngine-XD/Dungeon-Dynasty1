using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatEffectBase : MonoBehaviour
{
    public string name = "";
    public bool callOnUpdate = false;
    public virtual float Poisonffect()
    {
        return 0.0f;
    }
    public virtual void onUpdate()
    {
        Debug.Log("damaging");
    }
}

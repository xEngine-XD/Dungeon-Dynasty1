using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveAvailability : MonoBehaviour
{
    public bool wasUsed = false;
    public int number;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Save()
    {
        //GameManager.instance.SavePlayer(number.ToString());
    }
    public void Load()
    {
        //GameManager.instance.LoadPlayer(number.ToString());
    }
}

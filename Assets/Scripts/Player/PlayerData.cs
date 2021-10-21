using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData 
{
    public int level;
    public int health;
    public float[] position;

   // public List<Equipment> invItems = new List<Equipment>();
    //public PlayerController player;
    //public PlayerStats stats;
    //public EquipmentManager equipment;
    //public Inventory inventory;


    public PlayerData(PlayerController player)//, Inventory inventory)
    {
        //level = player.level;
        //health = player.health;
        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = 0;
        //invItems = inventory.items;
        //stats = GameManager.instance.playerStats;
        //inventory = GameManager.instance.gameObject.GetComponent<Inventory>();
        //equipment = GameManager.instance.gameObject.GetComponent<EquipmentManager>();

    }
}

using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;



public static class SaveSystem 
{
    //private SaveGlob saveGlob;
    public static void SavePlayer(PlayerController player, string number)//, Inventory inventory)//, SaveGlob saveGlob)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player" + number +".save";

        FileStream stream = new FileStream(path, FileMode.Create);


        /*saveGlob = new SaveGlob();
        
        if (GameManager.instance != null)
        {
            saveGlob.player = GameManager.instance.player;
            saveGlob.stats = GameManager.instance.playerStats;
            saveGlob.inventory = GameManager.instance.gameObject.GetComponent<Inventory>();
            saveGlob.equipment = GameManager.instance.gameObject.GetComponent<EquipmentManager>();
        }*/


        PlayerData data = new PlayerData(player);//, inventory);
        //PlayerData data = new PlayerData(saveGlob);
        formatter.Serialize(stream, data);
        //formatter.Serialize(stream, saveGlob);
        stream.Close();
    }
    public static PlayerData LoadPlayer(string number)
    {
        string path = Application.persistentDataPath + "/player" + number + ".save";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data =  formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;

        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }

}

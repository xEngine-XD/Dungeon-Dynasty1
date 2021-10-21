using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public bool wasUsed = false;
    public int number;

    public void Save()
    {
        //GameManager.instance.SavePlayer(number.ToString());
    }
    public void Load()
    {
        //GameManager.instance.LoadPlayer(number.ToString());
        SceneManager.LoadScene("Hub");
        SaveHandler.instance.Test();
    }
}

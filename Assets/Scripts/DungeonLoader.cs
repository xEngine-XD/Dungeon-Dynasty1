using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonLoader : MonoBehaviour
{
    public GameObject winWindow;
    public bool isDone = false;
    private void Start()
    {
        GameManager.instance.LoadPlayer("1");
        Debug.Log("stats saved");
    }
    private void Update()
    {
        if (isDone)
        {
            winWindow.SetActive(true);
        }
    }
}

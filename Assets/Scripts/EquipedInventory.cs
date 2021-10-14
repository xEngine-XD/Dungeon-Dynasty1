using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipedInventory : MonoBehaviour
{
    public static EquipedInventory instance;
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Awake()
    {
        if (instance != null)
        {
            //Destroy(gameObject);
            Debug.Log("already created");
        }
        instance = this;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

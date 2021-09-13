using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDontDestroy : MonoBehaviour
{
    public static UIDontDestroy instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

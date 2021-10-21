using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
public class SaveHandler : MonoBehaviour
{
    public static SaveHandler instance;
    public Button[] saveButtons = new Button[3];
    public Button[] loadButtons = new Button[3];
    public LoadButton[] loads;
    public Button saveButton;
    public Button loadButton;

    // Start is called before the first frame update
    void Awake()
    {
        if(SaveHandler.instance != null)
        {
            Destroy(this);
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);
        //UpdateUI();
        Test();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void UpdateUI()
    {

        int counter = 0;
        for (int i = 0; i < saveButtons.Length; i++)
        {
            if(saveButtons[i].GetComponent<SaveAvailability>().wasUsed == true)
            {
                loadButtons[i].interactable = true;
                counter += 1;
            }
        }
        if(counter > 0)
        {
            loadButton.interactable = true;

        }
    }
    public void Test()
    {
        loads = FindObjectsOfType<LoadButton>(true);
        int counter = 0;
        
        for (int i = 0; i < loads.Length; i++)
        {

            if(File.Exists(Application.persistentDataPath + "/player" + ((i+1).ToString()) + ".save"))
            {
                loads[i].GetComponent<Button>().interactable = true;
                counter += 1;
            }
        }
        if (counter > 0)
        {
            loadButton.interactable = true;

        }
        Debug.Log(counter);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LibrarianNPC : MonoBehaviour
{
    public GameObject libraryUI;
    public GameObject button;
    public Button exitButton;
    bool isEnabled = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        OpenLibrary();
    }
    void OpenLibrary()
    {
        float distance = Vector2.Distance(this.transform.position, GameManager.instance.player.transform.position);
        if (distance < 2.0f)
        {
            button.SetActive(true);
            
        }
        else
        {
            button.SetActive(false);
  
        }



        if (button.activeSelf)
        {
            if (!libraryUI.activeSelf)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    libraryUI.SetActive(true);
                    GameManager.instance.player.canMove = false;
                    GameManager.instance.sounds.OpenLibrary();
                }
            }
            else if (libraryUI.activeSelf)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    libraryUI.SetActive(false);
                    GameManager.instance.player.canMove = true;
                    GameManager.instance.sounds.OpenLibrary();
                }
            }

        }
        

    }
    public void CloseLibrary()
    {
        libraryUI.SetActive(false);
        GameManager.instance.player.canMove = true;
    }
}

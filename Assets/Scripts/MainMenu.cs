using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void StartNewGame()
    {
        SceneManager.LoadScene("Hub");
        SaveHandler.instance.Test();
        Time.timeScale = 1;

    }
    public void Pause()
    {
        Time.timeScale = 0;
    }
    public void Resume()
    {
        Time.timeScale = 1;
    }
    public void Mainmenu()
    {
        SceneManager.LoadScene("Main Menu");
        SaveHandler.instance.Test();
    }
    public void SaveProgress()
    {

    }
    public void LoadProgress()
    {

    }
}

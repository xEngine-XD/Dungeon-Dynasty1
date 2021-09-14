using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public List<Sprite> playerSprites;
    public List<Sprite> weaponsSprites;
    public int experience;
    public Player player;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void Awake()
    {
        if(GameManager.instance != null)
        {
            Destroy(this.gameObject);
        }
        instance = this;
        //SceneManager.sceneLoaded += LoadState;
        DontDestroyOnLoad(this.gameObject);

    }
    public void SavePlayer()
    {
        SaveSystem.SavePlayer(player);
    }
    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        //player.level = data.level;
        //player.health = data.health;
        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        player.transform.position = position;

    }
    /*public void SaveState()
    {
        string s = "";
        s += experience.ToString() + "|";
        PlayerPrefs.SetString("SaveState", s);
        Debug.Log("SaveState");
    }*/
    /*public void LoadState(Scene scene, LoadSceneMode mode)
    {
        if (!PlayerPrefs.HasKey("SaveState"))
            return;
        string[] data = PlayerPrefs.GetString("SaveState").Split('|');
        experience = int.Parse(data[1]);
        Debug.Log("LoadState");
    }*/
}

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
    public PlayerController player;
    public PlayerStats playerStats;
    public float poisonDebufTimer = 1f;
    HealthBar healthBar;
    // Start is called before the first frame update
    void Start()
    {
        //healthBar = FindObjectOfType<HealthBar>();
        //healthBar.SetMaxHealth(playerStats.maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.SetHealth(playerStats.currentHealth);
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
        healthBar = FindObjectOfType<HealthBar>();
        healthBar.SetMaxHealth(playerStats.maxHealth);
    }
    public void SavePlayer(string number)
    {
        SaveSystem.SavePlayer(player, number);
    }

    public void LoadPlayer(string number)
    {
        PlayerData data = SaveSystem.LoadPlayer(number);
        //player.level = data.level;
        //player.health = data.health;
        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        player.transform.position = position;

    }

}

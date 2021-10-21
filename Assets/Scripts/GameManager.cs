using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;



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
    public TMP_Text healthNum;
    public Transform dmgPrefab;
    public GameObject pauseMenu;
    public Inventory inv;
    //private SaveGlob saveGlob;
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
        healthNum.text = playerStats.currentHealth + "/" + playerStats.maxHealth;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseMenu.activeSelf == true)
            {
                pauseMenu.SetActive(false);
            }
            else
                pauseMenu.SetActive(true);
        }
    }
    private void Awake()
    {
        if(GameManager.instance != null)
        {
            Destroy(this.gameObject);
        }
        instance = this;
        //SceneManager.sceneLoaded += LoadState;
        //DontDestroyOnLoad(this.gameObject);
        healthBar = FindObjectOfType<HealthBar>();
        healthBar.SetMaxHealth(playerStats.maxHealth);
    }
    public void SavePlayer(string number)
    {
        //SaveGlob save = CreateData();

        SaveSystem.SavePlayer(player, number);
    }

    public void LoadPlayer(string number)
    {
        PlayerData data = SaveSystem.LoadPlayer(number);
        //player.level = data.level;
        //player.health = data.health;
        //SceneManager.LoadScene("Hub");
        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        
        player.transform.position = position;

        //playerStats = data.stats;
        //gameObject.GetComponent<Inventory>().items = data.inventory.items;
        //gameObject.GetComponent<EquipmentManager>().currentEquipment = data.equipment.currentEquipment;

        Time.timeScale = 1;
    }
    

}

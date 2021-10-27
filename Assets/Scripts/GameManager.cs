using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;



public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    //public List<Sprite> playerSprites;
    //public List<Sprite> weaponsSprites;
    //public int experience;
    public PlayerController player;
    public PlayerStats playerStats;
    public float poisonDebufTimer = 1f;
    HealthBar healthBar;
    public TMP_Text healthNum;
    public Transform dmgPrefab;
    public GameObject pauseMenu;
    public Inventory inv;
    public GameObject[] objectsToActivate;
    public GameObject dieWindow;

    public SFX sounds;
    //private SaveGlob saveGlob;
    // Start is called before the first frame update
    void Start()
    {
        //healthBar = FindObjectOfType<HealthBar>();
        //healthBar.SetMaxHealth(playerStats.maxHealth);
        //SetGameManager();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerStats.currentHealth > 0)
        {
            healthBar.SetHealth(playerStats.currentHealth);
        }
        else
            playerStats.currentHealth = 0;

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
        playerStats = FindObjectOfType<PlayerStats>();
        //SceneManager.sceneLoaded += LoadState;
        //DontDestroyOnLoad(this.gameObject);
        sounds = FindObjectOfType<SFX>();
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
        //exporting binary data
        PlayerData data = SaveSystem.LoadPlayer(number);
        //position set
        //Vector3 position;
        //position.x = data.position[0];
        //position.y = data.position[1];
        //position.z = data.position[2];
        //player.transform.position = position;
        //stats set
        playerStats.maxHealth = data.health;
        playerStats.damage.baseValue = data.pDamage;
        playerStats.armor.baseValue = data.pArmor;
        playerStats.poisonChance.baseValue = data.pPoisonChance;
        playerStats.poisonDamage.baseValue = data.pPoisonDmg;
        playerStats.poisonDeflect.baseValue = data.pPoisonDeflect;
        playerStats.criticalChance.baseValue = data.pCritChance;
        playerStats.criticalMultiplier.baseValue = data.pCritMul;
        playerStats.piercingChance.baseValue = data.pPiercing;
        playerStats.magicDamage.baseValue = data.pMagicDmg;
        playerStats.magicResist.baseValue = data.pMagicResist;



        Time.timeScale = 1;
    }
    public void SetGameManager()
    {
        player = FindObjectOfType<PlayerController>();
        playerStats = FindObjectOfType<PlayerStats>();
        healthNum = GameObject.FindGameObjectWithTag("HealthValue").GetComponent<TMP_Text>();
        //pauseMenu = FindObjectOfType<MainMenu>() as GameObject;
    }
    public void ActivateObjects()
    {
        objectsToActivate[0].SetActive(true);
        objectsToActivate[1].SetActive(true);
        healthBar = FindObjectOfType<HealthBar>();
        healthBar.SetMaxHealth(playerStats.maxHealth);
    }
    

}

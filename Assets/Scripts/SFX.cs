using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX : MonoBehaviour
{
    public AudioSource buttons;
    public AudioSource enemyHit;
    public AudioSource playerWalk;
    public AudioSource playerAttack;
    public AudioSource playerHit;
    public AudioSource playerDie;
    public AudioSource itemEquip;
    public AudioSource backgroundMain;
    public AudioSource backgroundDungeon;
    public AudioSource openLibrary;
    //public static SFX instance;

    private void Awake()
    {
        /*if (instance != null)
        {
            Destroy(this.gameObject);
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);*/
    }

    public void ButtonSFX()
    {
        buttons.Play();
    }
    public void EnemyHit()
    {
        enemyHit.Play();
    }
    public void PlayerHit()
    {
        playerHit.Play();
    }
    public void PlayerAttack()
    {
        playerAttack.Play();
    }
    public void PlayerDie()
    {
        playerDie.Play();
    }
    public void ItemEquip()
    {
        itemEquip.Play();
    }
    public void OpenLibrary()
    {
        openLibrary.Play();
    }
}

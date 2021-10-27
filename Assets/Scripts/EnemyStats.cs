using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using CodeMonkey.Utils;
public class EnemyStats : CharacterStats
{
    public Enemy enemy;
    [SerializeField] Transform target;
    private NavMeshAgent agent;

    private int poisonTemp;
    public bool canAttack = true;
    public float attackTime;
    public float attackRate;
    public bool isPoisoned = false;
    public bool amIPoisoned = false;
    private bool poisonEffect = true;
    public float poisonedTimer;
    public bool isHit = false;
    public SpriteRenderer sprite;
    public Animator anim;
    public bool isMoving = false;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        sprite = gameObject.GetComponent<SpriteRenderer>();
        attackTime = enemy.attackTime;
        attackRate = enemy.attackRate;
        poisonTemp = enemy.poisonDebuffLength;
        damage.baseValue = enemy.damage;
        armor.baseValue = enemy.armor;
        poisonChance.baseValue = enemy.poisonChance;
        poisonedTimer = GameManager.instance.playerStats.poisonLength.GetValue();
        ChooseResistanceType();
        ChooseAttackType();
        isMoving = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(isMoving == true)
            Move();
        Hit();
        if(isHit == true)
        {
            StartCoroutine("Pushback");
            StartCoroutine("ChangeColorOnHit");
            Debug.Log("ddd");
        }
        if (GameManager.instance.playerStats.isPoisoned == true)
        {
            StartCoroutine("PoisonDamage");
            
        }
        if(isPoisoned == true)
        {
            StartCoroutine("GetPoisonDamage");
            
        }
        if(currentHealth <= 0)
        {
            anim.SetBool("isDead", true);
            isMoving = false;
            canAttack = false;
        }
    }
    public void ChooseAttackType()
    {
        switch (enemy.attackModifiers)
        {
            case Enemy.AttackModifiers.Poisoning:
                poisonDamage.baseValue = enemy.poisonDamage;
                break;
            case Enemy.AttackModifiers.Piercing:
                piercingChance.baseValue = enemy.pierceChance;
                break;
            case Enemy.AttackModifiers.Magical:
                magicDamage.baseValue = enemy.magicDamage;
                break;
            case Enemy.AttackModifiers.None: 
                break;
        }
    }
    public void AttackType()
    {
        switch (enemy.attackModifiers)
        {
            case Enemy.AttackModifiers.Poisoning:
                bool poison = PoisonProc();
                if (poison == true)
                {
                    if (GameManager.instance.playerStats.isPoisoned == false)
                        GameManager.instance.playerStats.isPoisoned = true;
                }
                else if (poison == false || GameManager.instance.playerStats.isPoisoned == true)
                    DefaultAttack();

                break;
            case Enemy.AttackModifiers.Piercing:
                PierceAttack();
                break;
            case Enemy.AttackModifiers.Magical:
                MagicalAttack();
                break;
            case Enemy.AttackModifiers.None:
                DefaultAttack();
                break;
        }
        GameManager.instance.sounds.PlayerHit();

    }
    public void ChooseResistanceType()
    {
        switch (enemy.resistances)
        {
            case Enemy.Resistances.None:
                break;
            case Enemy.Resistances.Magical:
                magicResist.baseValue = enemy.magicResist;
                break;
            case Enemy.Resistances.Poisoning:
                poisonDeflect.baseValue = enemy.poisonResist;
                break;

        }
    }
    public void Move()
    {
        float distance = Vector3.Distance(transform.position, target.transform.position);
        if (enemy.enemyType == Enemy.EnemyType.Melee)
        {
            if (distance > enemy.attackMeleeDistance)
            {
                agent.SetDestination(target.position);
                anim.SetBool("isAttacking", true);
            }
            else
                anim.SetBool("isAttacking", false);
        }
    }
    public void Hit()
    {
        if (canAttack)
        {
            
            
            float distance = Vector3.Distance(transform.position, target.transform.position);
            switch (enemy.enemyType)
            {
                case Enemy.EnemyType.Melee:

                    if (distance <= enemy.attackMeleeDistance)
                    {
                        isMoving = false;
                        anim.SetTrigger("Attack");
                        string dir = AngleDir();
                        if(dir == "left")
                        {
                            anim.SetInteger("attackDirection", -1);
                        }
                        else if (dir == "right")
                        {
                            anim.SetInteger("attackDirection", 1);
                        }
                        else
                        {
                            anim.SetInteger("attackDirection", -1);
                        }
                    }
                    break;
                case Enemy.EnemyType.Range:

                    if (distance <= enemy.attackRangeDistance)
                    {
                        AttackType();
                    }
                    break;
            }
            canAttack = false;
            isMoving = true;
            anim.SetBool("readyToAttack", false);
        }
        else
        {
            attackTime -= Time.deltaTime;
            if (attackTime <= 0)
            {
                canAttack = true;
                attackTime = attackRate;
            }
        }

    }
    IEnumerator PoisonDamage()
    {

        if (poisonEffect && enemy.poisonDebuffLength > 0)
        {
            GameManager.instance.playerStats.TakePoisonDamage(poisonDamage.baseValue);
            
            poisonEffect = false;
            yield return new WaitForSeconds(GameManager.instance.poisonDebufTimer);

            enemy.poisonDebuffLength -= 1;
            poisonEffect = true;
        }
        else if (enemy.poisonDebuffLength == 0)
        {
            GameManager.instance.playerStats.isPoisoned = false;
            enemy.poisonDebuffLength = poisonTemp;
        }
    }
    public bool PoisonProc()
    {
        float randomValue = Random.value;
        if (randomValue >= (1f - poisonChance.GetValue()))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    IEnumerator GetPoisonDamage()
    {
        if (amIPoisoned == false && poisonedTimer > 0)
        {
            TakePoisonDamage(GameManager.instance.playerStats.poisonDamage.GetValue());
            DamageUI.Create(transform.position, GameManager.instance.playerStats.poisonDamage.GetValue(), false, true);
            amIPoisoned = true;
            yield return new WaitForSeconds(GameManager.instance.poisonDebufTimer);
            poisonedTimer -= 1;
            amIPoisoned = false;
        }
        else if (poisonedTimer == 0)
        {
            isPoisoned = false;
            poisonedTimer = GameManager.instance.playerStats.poisonLength.GetValue();
        }
    }
    public void PierceAttack()
    {
        float randomValue = Random.value;
        if (randomValue >= (1f - enemy.pierceChance))
        {
            GameManager.instance.playerStats.TakePiercingDamage(enemy.damage);
        }
        else if (randomValue < (1f - enemy.pierceChance))
        {
            GameManager.instance.playerStats.TakeDamage(enemy.damage);
        }
    }
    public void DefaultAttack()
    {
        GameManager.instance.playerStats.TakeDamage(enemy.damage);
    }
    public void MagicalAttack()
    {
        GameManager.instance.playerStats.TakeMagicDamage(enemy.magicDamage);
    }
    public  void TakeDamageFromPlayer()
    {
        float damage = GameManager.instance.playerStats.damage.GetValue();
        bool isCrit = false;
        if (GameManager.instance.playerStats.CritDamage() == true)
        {
            damage *= GameManager.instance.playerStats.criticalMultiplier.GetValue();
            isCrit = true;
        }
        if (damage != 0)
        {
            
            if(GameManager.instance.playerStats.PierceDamage() == true)
            {
                TakePiercingDamage(damage);
            }
            else if(GameManager.instance.playerStats.PierceDamage() == false)
            {
                TakeDamage(damage);
            }
        }
        if(GameManager.instance.playerStats.magicDamage.GetValue() > 0)
        {
            TakeMagicDamage(GameManager.instance.playerStats.magicDamage.GetValue());
        }
        if (target.GetComponent <PlayerStats>().poisonDamage.GetValue() != 0)
        {
            if(target.GetComponent<PlayerStats>().PoisonProc() == true && isPoisoned == false)
            {
                isPoisoned = true; 
            }
        }
        isHit = true;
        DamageUI.Create(transform.position, damage, isCrit, false);
        GameManager.instance.sounds.EnemyHit();
    }

    IEnumerator Pushback()
    {
        string dir = AngleDir();
        //string color = UtilsClass.GetColorFromString(this.gameObject.GetComponent<SpriteRenderer>().color)
        if (isHit == true)
        {
            if (dir == "right")
            {
                transform.position += new Vector3(-10 * Time.deltaTime * GameManager.instance.player.pushback, 0, 0) ;
            }
            if(dir == "left")
            {
                transform.position += new Vector3(10 * Time.deltaTime * GameManager.instance.player.pushback, 0, 0);
            }
            if(dir == "under")
            {
                transform.position += new Vector3(0, 19 * Time.deltaTime * GameManager.instance.player.pushback, 0);
            }
            if(dir == "above")
            {
                transform.position += new Vector3(0, -19 * Time.deltaTime * GameManager.instance.player.pushback, 0);
            }
            //this.gameObject.GetComponent<SpriteRenderer>().color = new Color(220, 84, 84, 150);
        }
        yield return new WaitForSeconds(0.1f);

        isHit = false;
    }
    public IEnumerator ChangeColorOnHit()
    {
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sprite.color = Color.white;
    }
    public string AngleDir()
    {
        Vector2 relativePoint;
        relativePoint = transform.InverseTransformPoint(target.position);
        if (relativePoint.x < 0f && Mathf.Abs(relativePoint.x) > Mathf.Abs(relativePoint.y))
        {
            Debug.Log("Left");
            return "left";
        }
        if (relativePoint.x > 0f && Mathf.Abs(relativePoint.x) > Mathf.Abs(relativePoint.y))
        {
            Debug.Log("Right");
            return "right";
        }
        if (relativePoint.y > 0 && Mathf.Abs(relativePoint.x) < Mathf.Abs(relativePoint.y))
        {
            Debug.Log("Above");
            return "above";
        }
        if (relativePoint.y < 0 && Mathf.Abs(relativePoint.x) < Mathf.Abs(relativePoint.y))
        {
            Debug.Log("Under");
            return "under";
        }
        else
            return null;
    }
    public void DieEnemy()
    {
        Destroy(this.gameObject);
        DungeonLoader dungeon = FindObjectOfType<DungeonLoader>();
        dungeon.isDone = true;

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
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
    bool poisonEffect = true;
    private float poisonTimer = 1.5f;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        attackTime = enemy.attackTime;
        attackRate = enemy.attackRate;
        poisonTemp = enemy.poisonDebuffLength;
        damage.baseValue = enemy.damage;
        armor.baseValue = enemy.armor;
        ChooseResistanceType();
        ChooseAttackType();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Hit();
        if (GameManager.instance.playerStats.isPoisoned == true)
        {
            StartCoroutine("PoisonDamage");
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
                if (GameManager.instance.playerStats.isPoisoned == false)
                    GameManager.instance.playerStats.isPoisoned = true;

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
            }
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
                        AttackType();
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
            //isPoisoned = false;
            enemy.poisonDebuffLength = poisonTemp;
        }
    }
    public void PierceAttack()
    {
        float randomValue = Random.value;
        //Debug.Log(randomValue);
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
        if (GameManager.instance.playerStats.CritDamage() == true)
        {
            damage *= GameManager.instance.playerStats.criticalMultiplier.GetValue();
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
            if(target.GetComponent<PlayerStats>().PoisonProc() == true)
            {
                TakePoisonDamage(target.GetComponent<PlayerStats>().poisonDamage.GetValue());
            }
        }
    }
}

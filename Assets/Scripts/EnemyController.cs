using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public Enemy enemy;
    public int poisonDamage;
    public float poisonTimer;
    [SerializeField] Transform target;
    private NavMeshAgent agent;
    public bool isPoisoned = false;
    bool poisonEffect = true;
    public int poisonDebuffLength;
    private int poisonTemp;
    public float pierceChance;
    public float magicDamage;
    public float attackTime;
    public bool canAttack = true;
    public float attackRate;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        poisonTemp = poisonDebuffLength;
        //ChooseAttackType();
        
    }

    // Update is called once per frame
    void Update()
    {
        //agent.SetDestination(target.position);
        //agent.Move(target.position);
        //agent.destination = target.position;
        Move();
        Hit();
        //ChooseAttackType();
        if (isPoisoned)
        {
            StartCoroutine("PoisonDamage");
        }
        //poisonTimer -= Time.deltaTime;
    }
    public void ChooseAttackType()
    {
        switch (enemy.attackModifiers)
        {
            case Enemy.AttackModifiers.Poisoning:
                if (isPoisoned == false)
                    isPoisoned = true;

                break;
            case Enemy.AttackModifiers.Piercing:
                PierceAttack();
                break;
            case Enemy.AttackModifiers.Magical:
                MagicalAttack();
                break;
            case Enemy.AttackModifiers.None:
                Attack();
                break;
        }
    }
    public void Move()
    {
        float distance = Vector3.Distance(transform.position, target.transform.position);
        if(enemy.enemyType == Enemy.EnemyType.Melee)
        {
            if(distance > enemy.attackMeleeDistance)
            {
                agent.SetDestination(target.position);
            }
        }
    }

    IEnumerator PoisonDamage()
    {
        if (poisonEffect && poisonDebuffLength > 0)
        {
            GameManager.instance.playerStats.TakePoisonDamage(poisonDamage);
            poisonEffect = false;
            yield return new WaitForSeconds(poisonTimer);
            poisonDebuffLength -= 1;
            poisonEffect = true;
        }
        else if (poisonDebuffLength == 0)
        {
            isPoisoned = false;
            poisonDebuffLength = poisonTemp;
        }
    }
    public void PierceAttack()
    {
        float randomValue = Random.value;
        Debug.Log(randomValue);
        if (randomValue >= (1f - pierceChance))
        {
            GameManager.instance.playerStats.TakePiercingDamage(enemy.damage);

        }
        else if (randomValue < (1f - pierceChance))
        {
            GameManager.instance.playerStats.TakeDamage(enemy.damage);

        }
    }
    public void Attack()
    {
        GameManager.instance.playerStats.TakeDamage(enemy.damage);
    }
    public void MagicalAttack()
    {
        GameManager.instance.playerStats.TakeMagicDamage(magicDamage);
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
                        ChooseAttackType();
                    }
                    break;
                case Enemy.EnemyType.Range:

                    if (distance <= enemy.attackRangeDistance)
                    {
                        ChooseAttackType();
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

}

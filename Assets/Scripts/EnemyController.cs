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
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        ChooseAttackType();
        
    }

    // Update is called once per frame
    void Update()
    {
        //agent.SetDestination(target.position);
        //agent.Move(target.position);
        //agent.destination = target.position;
        ChooseAttackType();
        //poisonTimer -= Time.deltaTime;
    }
    public void ChooseAttackType()
    {
        switch (enemy.attackModifiers)
        {
            case Enemy.AttackModifiers.Poisoning:
                PoisonAttack();
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
    public void PoisonAttack()
    {
        //Debug.Log("Attack is poisoned");
        poisonTimer -= Time.deltaTime;
        if (poisonTimer <= 0)
        {

            GameManager.instance.playerStats.TakePoisonDamage(poisonDamage);
            poisonTimer = 2.0f;
            poisonDamage = 10;
        }
       
    }
    public void PierceAttack()
    {

    }
    public void Attack()
    {

    }
    public void MagicalAttack()
    {

    }
    public void Hit()
    {

    }

}

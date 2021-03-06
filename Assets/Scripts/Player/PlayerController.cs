using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using CodeMonkey.Utils;
public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    float horizontalMove = 0f;
    float verticalMove = 0;
    public float moveSpeed = 10f;
    private Vector3 moveVector;
    private Vector2 tempMove;
    private RaycastHit2D hit;
    private BoxCollider2D boxCollider;
    public Animator anim;
    bool isIdle;

    //attack variables
    public Transform attackPoint;
    public float attackRange = 1f;
    public float attackLength = 2f;
    public LayerMask enemyLayer;
    public float attackRate = 2f;
    private float nextAttackTime = 0;

    private PlayerStats playerStats;
    public bool canMove = true;

    public float pushback = 2f;


    private Vector2 movement;
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        playerStats = GetComponent<PlayerStats>();

    }

    // Update is called once per frame
    void Update()
    {
        Move();
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                canMove = false;
                anim.SetTrigger("Attack");
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
        
        
    }
    private void FixedUpdate()
    {

        if (canMove)
        {
            movement.Normalize();
            transform.Translate(movement.x * moveSpeed * Time.fixedDeltaTime, movement.y * moveSpeed * Time.fixedDeltaTime, 0);
        }

    }
    public void Move()
    {
        if (canMove)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
            anim.SetFloat("Horizontal", movement.x);
            anim.SetFloat("Vertical", movement.y);
            anim.SetFloat("Speed", movement.sqrMagnitude);
            if (movement.x != 0)
            {
                tempMove.x = movement.x;
                anim.SetFloat("IdleX", tempMove.x);
                tempMove.y = 0;
                anim.SetFloat("IdleY", tempMove.y);
            }
            if (movement.y != 0)
            {
                tempMove.y = movement.y;
                anim.SetFloat("IdleY", tempMove.y);
                tempMove.x = 0;
                anim.SetFloat("IdleX", tempMove.x);
            }
        }
    }
    void Move2()
    {
        if (canMove)
        {
            horizontalMove = Input.GetAxisRaw("Horizontal");
            verticalMove = Input.GetAxisRaw("Vertical");
            moveVector = new Vector3(horizontalMove, verticalMove, 0).normalized;

            hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveVector.x, 0), Mathf.Abs(moveVector.x * Time.deltaTime * moveSpeed), LayerMask.GetMask("NPCs", "Blockings"));
            if (hit.collider == null)
            {
                transform.Translate(moveVector.x * Time.deltaTime * moveSpeed, 0, 0);
            }
            hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveVector.y), Mathf.Abs(moveVector.y * Time.deltaTime * moveSpeed), LayerMask.GetMask("NPCs", "Blockings"));
            if (hit.collider == null)
            {
                transform.Translate(0, moveVector.y * Time.deltaTime * moveSpeed, 0);
            }

            isIdle = horizontalMove == 0 && verticalMove == 0;
            if (isIdle == false)
            {
                //GameManager.instance.sounds.playerWalk.Play();
                anim.SetFloat("IdleX", 0);
                anim.SetFloat("IdleY", 0);
                if (horizontalMove != 0)
                {
                    //verticalMove = 0;
                    tempMove.x = horizontalMove;
                    tempMove.y = 0;
                    anim.SetFloat("SpeedX", horizontalMove);
                    anim.SetBool("RunX", true);
                }
                if (verticalMove != 0)
                {
                    //horizontalMove = 0;
                    tempMove.y = verticalMove;
                    tempMove.x = 0;
                    anim.SetFloat("SpeedY", verticalMove);
                    anim.SetBool("RunY", true);
                }
                horizontalMove = 0;
                verticalMove = 0;
            }
            if (isIdle)
            {

                anim.SetFloat("SpeedX", 0);
                anim.SetFloat("SpeedY", 0);
                anim.SetBool("RunY", false);
                anim.SetBool("RunX", false);
                if (tempMove.x != 0)
                {
                    if (tempMove.x > 0)
                        anim.SetFloat("IdleX", 1);
                    else
                        anim.SetFloat("IdleX", -1);
                }
                if (tempMove.y != 0)
                {
                    if (tempMove.y > 0)
                        anim.SetFloat("IdleY", 1);
                    else
                        anim.SetFloat("IdleY", -1);
                }
            }

        }
        
        //Debug.Log(moveVector + "   horizontal=" + horizontalMove + "   vertical=" + verticalMove);
    }

    void Attack2()
    {

        anim.SetTrigger("Attack");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);
        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("hit " + enemy.name);
        }
    }
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
    void Attack()
    {
        GameManager.instance.sounds.PlayerAttack();

        Ray ray = new Ray(transform.position, moveVector * attackRange);
        Debug.DrawRay(ray.origin, ray.direction, Color.cyan);
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, attackRange, transform.TransformDirection(tempMove), attackLength, enemyLayer);
        if(hit)
        {
            hit.transform.GetComponent<EnemyStats>().TakeDamageFromPlayer();
            Debug.Log("hit" + hit.transform.name);
            GameManager.instance.sounds.EnemyHit();
            
        }
        
    }
    void CanMove()
    {
        canMove = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    private Vector3 moveDelta;
    public float movespeed = 10;
    private RaycastHit2D hit;
    public Animator anim;
    public Vector3 lastMoveDir;
    //public Vector3 moveDir;
    //public float moveY = 0;
    //public float moveX = 0;
    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        //moveDir = new Vector3(moveX, moveY);
    }
    private void FixedUpdate()
    {
        moveDelta = Vector3.zero;
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        moveDelta = new Vector3(x, y, 0).normalized;
        if (moveDelta.x > 0)
        {
            transform.localScale = Vector3.one;
            anim.SetFloat("dirMoveX", +1);

        }
        else if (moveDelta.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            anim.SetFloat("dirMoveX", -1);
        }
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime * movespeed), LayerMask.GetMask("NPCs", "Blockings"));
        if(hit.collider == null)
        {
            transform.Translate(0, moveDelta.y * Time.deltaTime * movespeed ,0);// * movespeed,0);
            if(moveDelta.y > 0)
            {
                anim.SetFloat("dirMoveY", +1);
            }
            if(moveDelta.y < 0)
            {
                anim.SetFloat("dirMoveY", -1);
            }

        }
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x,0), Mathf.Abs(moveDelta.x * Time.deltaTime * movespeed), LayerMask.GetMask("NPCs", "Blockings"));
        if (hit.collider == null)
        {
            transform.Translate(moveDelta.x * Time.deltaTime* movespeed, 0, 0);// * movespeed, 0, 0);// ; ;

        }
        bool isIdle = x == 0 && y == 0;
        if (isIdle)
        {
            anim.SetFloat("dirMoveY", 0);
            anim.SetBool("idleDefault", true);
            anim.SetFloat("dirMoveX", 0);

        }
        else if(isIdle == false)
        {
            anim.SetBool("idleDefault", false);
        }

        Debug.Log(moveDelta + " " + isIdle);
        //transform.Translate(moveDelta * Time.deltaTime * movespeed);
    }
    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.IsTouchingLayers(LayerMask.GetMask("NPCs", "Blockings"))){
            transform.Translate(0, moveDelta.y * Time.deltaTime * movespeed, 0);
            transform.Translate(moveDelta.x * Time.deltaTime * movespeed, 0, 0);
            print("smth");
        }
    }*/
    // Update is called once per frame
    void Update()
    {
        //Movement();
    }
    public void Movement()
    {
        float moveX = 0f;
        float moveY = 0f;
        if (Input.GetKey(KeyCode.W))
        {
            moveY = +1f;
            anim.SetFloat("dirMoveY", +1);
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveY = -1f;
            anim.SetFloat("dirMoveY", -1);
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveX = -1f;
            anim.SetFloat("dirMoveY", -1);
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveX = +1f;
            anim.SetFloat("dirMoveY", +1);
        }
        bool isIdle = moveX == 0 & moveY == 0;
        if (isIdle)
        {
            anim.SetFloat("dirMoveY", 0);
            anim.SetBool("idleDefault", true);
            anim.SetFloat("dirMoveX", 0);
            
        }
        else
        {
            anim.SetBool("idleDefault", false);
            Vector3 moveDir = new Vector3(moveX, moveY).normalized;
            transform.position += moveDir * movespeed * Time.deltaTime;

            
        }
        //Debug.Log(moveX + " " + moveY + " " + isIdle);
    }
}

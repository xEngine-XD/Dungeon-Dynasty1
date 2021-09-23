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

    

    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();

    }
    private void FixedUpdate()
    {
        Movement();
    }
    // Update is called once per frame
    void Update()
    {

    }
    public void Movement()
    {
        moveDelta = Vector3.zero;
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        moveDelta = new Vector3(x, y, 0).normalized;
        /*if (moveDelta.x > 0)
        {
            transform.localScale = Vector3.one;
            anim.SetFloat("dirMoveX", +1);
        }
        else if (moveDelta.x < 0)
        {
            //transform.localScale = new Vector3(-1, 1, 1);
            anim.SetFloat("dirMoveX", -1);
        }*/
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime * movespeed), LayerMask.GetMask("NPCs", "Blockings"));
        if (hit.collider == null)
        {
            transform.Translate(0, moveDelta.y * Time.deltaTime * movespeed, 0);
            if (moveDelta.y > 0)
            {
                anim.SetFloat("dirMoveY", +1);
            }
            if (moveDelta.y < 0)
            {
                anim.SetFloat("dirMoveY", -1);
                anim.SetFloat("speed", 1);
            }
        }
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x, 0), Mathf.Abs(moveDelta.x * Time.deltaTime * movespeed), LayerMask.GetMask("NPCs", "Blockings"));
        if (hit.collider == null)
        {
            transform.Translate(moveDelta.x * Time.deltaTime * movespeed, 0, 0);
            if (moveDelta.x > 0)
            {
                anim.SetFloat("dirMoveX", +1);
            }
            if (moveDelta.x < 0)
            {
                anim.SetFloat("dirMoveX", -1);
            }
        }

        bool isIdle = x == 0 && y == 0;
        if (isIdle)
        {
            anim.SetFloat("dirMoveY", 0);
            anim.SetFloat("dirMoveX", 0);
            anim.SetBool("idleDir", false);
            lastMoveDir.Normalize();
            if (lastMoveDir.x != 0 || lastMoveDir.y != 0)
            {
                anim.SetBool("idleDir", true);
            }
        }
        else if (isIdle == false)
        {
            anim.SetBool("idleDir", false);
            lastMoveDir = moveDelta;
        }
    }




}

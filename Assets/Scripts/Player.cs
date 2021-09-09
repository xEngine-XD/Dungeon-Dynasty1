using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    private Vector3 moveDelta;
    public float movespeed = 10;
    private RaycastHit2D hit;
    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }
    private void FixedUpdate()
    {
        moveDelta = Vector3.zero;
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        moveDelta = new Vector3(x, y, 0);
        if (moveDelta.x > 0)
        {
            transform.localScale = Vector3.one;
        }
        else if (moveDelta.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime * movespeed), LayerMask.GetMask("NPCs", "Blockings"));
        if(hit.collider == null)
        {
            transform.Translate(0, moveDelta.y * Time.deltaTime * movespeed ,0);// * movespeed,0);

        }
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x,0), Mathf.Abs(moveDelta.x * Time.deltaTime * movespeed), LayerMask.GetMask("NPCs", "Blockings"));
        if (hit.collider == null)
        {
            transform.Translate(moveDelta.x * Time.deltaTime* movespeed, 0, 0);// * movespeed, 0, 0);// ; ;

        }
        
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
        
    }
}

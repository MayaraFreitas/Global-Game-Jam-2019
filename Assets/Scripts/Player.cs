using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float speed = 10f;
    public float jumpForce = 200f;
    public Transform groundCheck;
    public LayerMask whatIsGround;
    public float radius = 0.2f;

    bool isJumping = false;
    bool isOnFloor = false;
    bool paddle = false;

    Rigidbody2D body;
    CapsuleCollider2D collider;
    SpriteRenderer sprite;
    Animator anim;

    private Vector3 baseColliderSize;
    private Vector3 paddleColliderSize;

    // Use this for initialization
    void Start () {
        body = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        collider = GetComponent<CapsuleCollider2D>();

        baseColliderSize = collider.size;
        paddleColliderSize = new Vector3(collider.size.x, (collider.size.y / 2f));

    }
	
	// Update is called once per frame
	void Update () {
        //isOnFloor = Physics2D.Linecast(transform.position, groundCheck.position, whatIsGround);
        isOnFloor = Physics2D.OverlapCircle(groundCheck.position, radius, whatIsGround);

        if (Input.GetButtonDown("Jump") && isOnFloor == true)
        {
            isJumping = true;
        }

        PlayerAnimation();
        /*
        RaycastHit2D hit = Physics2D.Raycast(transform.Find("paddle").position, Vector2.down, 5.0f);
        if (hit.collider != null)
        {
            print("tag : " + hit.collider.transform.tag);

            if (hit.collider.transform.tag == "WoodInWater")
            {
                print("WoodInWater : " + hit.distance);
                //collider.size = paddleColliderSize;
            }
            else
            {
                //collider.size = baseColliderSize;
            }
        }
        */
    }

    void FixedUpdate(){
        float move = Input.GetAxis("Horizontal");

        body.velocity = new Vector2(move * speed, body.velocity.y);

        if((move > 0 && sprite.flipX == true) || (move < 0 && sprite.flipX == false))
        {
            Flip();
        }

        if (isJumping)
        {
            body.AddForce(new Vector2(0f, jumpForce));
            isJumping = false;
        }
    }

    void Flip()
    {
        sprite.flipX = !sprite.flipX;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, radius);
    }

    void PlayerAnimation()
    {

        if (paddle)
        {
            anim.Play("remo");
        }
        else if(body.velocity.x == 0 && body.velocity.y == 0)
        {
            anim.Play("idle");
        }
        else if(body.velocity.x != 0 &&body.velocity.y == 0)
        {
            anim.Play("walk");
        }
        else if(body.velocity.y != 0)
        {
            anim.Play("jump");
        }
    }

    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "WoodInWater")
        {
            //transform.parent = collision.transform;
            paddle = true;
            //collider.size = paddleColliderSize;
        }
    }

    public virtual void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag == "WoodInWater")
        {
            //transform.parent = null;

            paddle = false;
            //caollider.size = baseColliderSize;
        }
    }
}

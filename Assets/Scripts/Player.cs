using UnityEngine;

public class Player : MonoBehaviour {

    #region Properties

    public float Speed = 10f;
    public float JumpForce = 200f;
    public Transform GroundCheck;
    public LayerMask WhatIsGround;
    public float Radius = 0.2f;
    public GameObject Water;

    private bool isJumping = false;
    private bool isOnFloor = false;
    private bool paddle = false;
    private bool isDie = false;

    private Rigidbody2D body;
    private SpriteRenderer sprite;
    private Animator anim;

    private float lastWoodPosition;
    private float countTimeIsDie = 0;

    private Vector3 baseColliderSize;
    private Vector3 paddleColliderSize;

    #endregion Properties

    #region Methods

    public void Start () {
        body = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    public void Update () {
        //isOnFloor = Physics2D.Linecast(transform.position, groundCheck.position, whatIsGround);
        isOnFloor = Physics2D.OverlapCircle(GroundCheck.position, Radius, WhatIsGround);
        if (Input.GetButtonDown("Jump") && isOnFloor == true)
        {
            isJumping = true;
        }

        PlayerAnimation();
    }

    public void FixedUpdate(){

        if (isDie)
        {
            countTimeIsDie += 1f * Time.deltaTime;
            print("countTimeIsDie: " + countTimeIsDie);
            if (countTimeIsDie >= 1)
            {
                print("MORREU");
                GerenciadorDoGame.instancia.FinalizarJogo();
                countTimeIsDie = 0f;
            }
        }

        #region Move Player

        float move = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(move * Speed, body.velocity.y);

        if((move > 0 && sprite.flipX == true) || (move < 0 && sprite.flipX == false))
        {
            Flip();
        }

        if (isJumping)
        {
            body.AddForce(new Vector2(0f, JumpForce));
            isJumping = false;
        }

        #endregion Move Player

        #region Paddle wood - water collapse

        RaycastHit2D footHit = Physics2D.Raycast(transform.Find("paddle").position, Vector2.down, 0.0001f);
        if (footHit.collider != null && footHit.collider.transform.tag == "WoodInWater")
        {

            RaycastHit2D obstacleHit = Physics2D.Raycast(transform.Find("obstacle").position, Vector2.right, 1f);
            if (obstacleHit.collider == null || obstacleHit.collider.transform.tag != "Obstacle")
            {
                if (lastWoodPosition <= 0f)
                {
                    lastWoodPosition = footHit.collider.transform.position.x;
                }

                Vector2 newWoodPosition = new Vector2(footHit.collider.transform.position.x - lastWoodPosition, 0);
                transform.position = new Vector2(newWoodPosition.x + transform.position.x, transform.position.y);
                lastWoodPosition = transform.position.x;
                paddle = true;

                // Change Water 
                Water.GetComponent<BuoyancyEffector2D>().flowMagnitude = 60;
            }
            else
            {
                lastWoodPosition = 0f;
                paddle = false;
                isJumping = true;
            }
        }
        else
        {
            lastWoodPosition = 0f;
            paddle = false;

            // Change Water 
            Water.GetComponent<BuoyancyEffector2D>().flowMagnitude = 40;
        }

        RaycastHit2D joelhinhoHit = Physics2D.Raycast(transform.Find("joelhinho").position, Vector2.down, 0.0001f);
        if (joelhinhoHit.collider != null && joelhinhoHit.collider.transform.tag == "Water")
        {
            diePerson();
        }
        #endregion Paddle wood - calcule position
    }

    private void diePerson()
    {
        isDie = true;
        /*
        float counter = 0;
        float waitTime = 10;
        while (counter < waitTime)
        {
            print("counter: " + waitTime);
            counter += Time.deltaTime;
        }

        GerenciadorDoGame.instancia.FinalizarJogo();
        */
    }

    public void Flip()
    {
        sprite.flipX = !sprite.flipX;
    }

    private void PlayerAnimation()
    {
        print("Player Die: " + isDie);
        if (isDie)
        {
            anim.Play("die");
        }
        else if (paddle)
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

    void OnCollisionEnter2D(Collision2D colisor)
    {
        if (colisor.collider.transform.tag == "Casco")
        {
            GerenciadorDoGame.instancia.FinalizarJogo();
        }
    }

    #endregion
}

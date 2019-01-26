using UnityEngine;

public class Player : MonoBehaviour {

    #region Properties

    public float speed = 10f;
    public float jumpForce = 200f;
    public Transform groundCheck;
    public LayerMask whatIsGround;
    public float radius = 0.2f;

    private bool isJumping = false;
    private bool isOnFloor = false;
    private bool paddle = false;

    private Rigidbody2D body;
    private CapsuleCollider2D collider;
    private SpriteRenderer sprite;
    private Animator anim;

    private float lastWoodPosition;

    private Vector3 baseColliderSize;
    private Vector3 paddleColliderSize;

    #endregion Properties

    #region Methods

    public void Start () {
        body = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        collider = GetComponent<CapsuleCollider2D>();

        baseColliderSize = collider.size;
        paddleColliderSize = new Vector3(collider.size.x, (collider.size.y / 2f));

    }

    public void Update () {
        //isOnFloor = Physics2D.Linecast(transform.position, groundCheck.position, whatIsGround);
        isOnFloor = Physics2D.OverlapCircle(groundCheck.position, radius, whatIsGround);

        if (Input.GetButtonDown("Jump") && isOnFloor == true)
        {
            isJumping = true;
        }

        PlayerAnimation();
        
        RaycastHit2D hit = Physics2D.Raycast(transform.Find("paddle").position, Vector2.down, 0.0001f);
        if (hit.collider != null)
        {
            print("tag : " + hit.collider.transform.tag);

            if (hit.collider.transform.tag == "WoodInWater")
            {
                print("WoodInWater : " + hit.distance);
                //collider.size = paddleColliderSize;
            }
            else if (hit.collider.transform.tag == "Water")
            {
                print("ESTOU NA AGUA");
            }
            else

            {
                print("NAO DEU CERTO");
                //collider.size = baseColliderSize;
            }
        }
        
    }

    public void FixedUpdate(){

        #region Move Player

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

        #endregion Move Player

        #region Paddle wood - calcule position

        RaycastHit2D footHit = Physics2D.Raycast(transform.Find("paddle").position, Vector2.down, 0.0001f);
        if (footHit.collider != null && footHit.collider.transform.tag == "WoodInWater")
        {
            if (lastWoodPosition <= 0f)
            {
                lastWoodPosition = footHit.collider.transform.position.x;
            }

            Vector2 newWoodPosition = new Vector2(footHit.collider.transform.position.x - lastWoodPosition, 0);
            transform.position = new Vector2(newWoodPosition.x + transform.position.x, transform.position.y);
            lastWoodPosition = transform.position.x;
            paddle = true;
        }
        else
        {
            paddle = false;
        }

        RaycastHit2D joelhinhoHit = Physics2D.Raycast(transform.Find("joelhinho").position, Vector2.down, 0.0001f);
        if (joelhinhoHit.collider != null && joelhinhoHit.collider.transform.tag == "Water")
        {
            print("ESTOU NA AGUA");
        }
        #endregion Paddle wood - calcule position
    }

    public void Flip()
    {
        sprite.flipX = !sprite.flipX;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, radius);
    }

    private void PlayerAnimation()
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

    #endregion
}

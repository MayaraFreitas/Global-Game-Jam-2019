using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jacareMove : MonoBehaviour {

    public Rigidbody2D rb;
    public float speed = 2;
    public SpriteRenderer sr;


	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

	}
	
	// Update is called once per frame
	void FixedUpdate () {
        rb.velocity = new Vector2(-speed, 0);
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Flipping")
        {
            if(sr.flipX == false)
            {
                sr.flipX = true;
                speed = -2;
                return;
            }
            else
            {
                sr.flipX = false;
                speed = 2;
            }
            
        }
    }

}

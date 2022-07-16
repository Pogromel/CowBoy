using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D rb2d;
    private Animator animate;

    private float MoveSpeed;
    private float JumpForce;
    private bool isjumping;
    private float moveHorizontal;
    private float moveVertical;
    private bool faceingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        animate = gameObject.GetComponent<Animator>();

        MoveSpeed = 0.5f;
        JumpForce = 1f;
        isjumping = false;
    }

    // Update is called once per frame
    void Update()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveVertical = Input.GetAxisRaw("Vertical");

        animate.SetFloat("Speed", Mathf.Abs(moveHorizontal));

        if(moveHorizontal > 0 && !faceingRight)
        {
            Flip();
        }
        else if (moveHorizontal < 0 && faceingRight)
        {
            Flip();
        }
    }

    private void FixedUpdate()
    {
        if(moveHorizontal > 0.1f || moveHorizontal < -0.1f)
        {
            rb2d.AddForce(new Vector2(moveHorizontal * MoveSpeed, 0f), ForceMode2D.Impulse);
        }

        if (!isjumping && moveVertical > 0.1f )
        {
            rb2d.AddForce(new Vector2(0f, moveVertical * JumpForce), ForceMode2D.Impulse);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            isjumping = false;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            isjumping = true;
        }
    }

    void Flip()
    {
        faceingRight = !faceingRight;
        Vector2 currentScale = transform.localScale;
        currentScale.x *= -1;
        transform.localScale = currentScale;
    }

}

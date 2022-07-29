using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    private float slopeCheckDistance;

    private Rigidbody2D rb2d;
    private Animator animate;
    private Vector2 colliderSize;

    private float MoveSpeed;
    private float JumpForce;
    private bool isjumping;
    private float moveHorizontal;
    private float moveVertical;
    private bool faceingRight = true;

    private CapsuleCollider2D cc;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        animate = gameObject.GetComponent<Animator>();

        MoveSpeed = 1.5f;
        JumpForce = 40f;
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

        SlopeCheck();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            isjumping = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
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

    private void start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        cc = GetComponent<CapsuleCollider2D>();

        colliderSize = cc.size;
    }

    private void SlopeCheck()
    {
        Vector2 checkPos = transform.position - new Vector3(0.0f, colliderSize.y / 2);
    }

    private void SlopeCheckHorizontal(Vector2 checkPos)
    {

    }

    private void SlopeCheckVertical(Vector2 checkPos)
    {
        RaycastHit2D hit = Physics2D.Raycast(checkPos, Vector2.down, slopeCheckDistance, whatIsGround);
    }

}

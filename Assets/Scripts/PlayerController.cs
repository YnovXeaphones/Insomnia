using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Animator anim;
    [SerializeField] private Transform tr;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private bool isGrounded = false;
    [SerializeField] private bool isInDream = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        tr = GetComponent<Transform>();
    }
    // Update is called once per frame
    void Update()
    {
        // Horizontal movement
        float x = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(x * speed, rb.velocity.y);
        // Running animation
        if (x > 0)
        {
            sr.flipX = true;
            if (isGrounded == true) {
                anim.SetBool("isWalking", true);
                anim.Play("Walk");
            }
        }
        else if (x < 0)
        {
            sr.flipX = false;
            if (isGrounded == true) {
                anim.SetBool("isWalking", true);
                anim.Play("Walk");
            }
        }
        if (rb.velocity.x <= 0 && x == 0 && isGrounded == true)
        {
            anim.SetBool("isWalking", false);
            anim.Play("Idle");
        }
        // Jumping
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isGrounded = false;
            anim.SetBool("isJumping", true);
            anim.SetBool("isWalking", false);
            anim.Play("Jump");
        }
        // if (!isGrounded && rb.velocity.y < 0) {
        //     anim.SetBool("isJumping", false);
        //     anim.SetBool("isFalling", true);
        //     anim.Play("Fall");
        // }
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (isInDream == false)
            {
                isInDream = true;
                tr.position = new Vector3(tr.position.x, tr.position.y +(-11), tr.position.z);
            }
            else
            {
                isInDream = false;
                tr.position = new Vector3(tr.position.x, tr.position.y - (-12), tr.position.z);
            }
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground" && rb.velocity.y <= 0.5 && isGrounded == false)
        {
            isGrounded = true;
            anim.SetBool("isJumping", false);
            anim.SetBool("isFalling", false);
            if (rb.velocity.x > 0 || rb.velocity.x < 0)
            {
                anim.SetBool("isWalking", true);
                anim.Play("Walk");
            }
            else
            {
                anim.SetBool("isWalking", false);
                anim.Play("Idle");
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }
}
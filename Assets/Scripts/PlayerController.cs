using System.Collections;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator anim;
    private Transform tr;
    private TrailRenderer TR;
    [Header("Movement")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 5f;
    private float CoyoteTimeOG;
    [SerializeField] private float CoyoteTime = 0.2f;
    [Header("Dash")]
    [SerializeField] private float dashingPower = 5f;
    [SerializeField] private float dashingTime = 0.5f;
    [SerializeField] private float dashingCooldown = 1f;

    [Header("Checker")]
    public bool isGrounded = true;
    public bool doubleJump = true;
    [SerializeField] private bool falling = false;
    [SerializeField] private bool WallJump = false;
    [SerializeField] private bool WallJumped = false;
    [SerializeField] private bool dash = true;
    [SerializeField] private bool isInDream = false;
    [Header("Respawn")]
    public Vector3 respawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        tr = GetComponent<Transform>();
        TR = GetComponent<TrailRenderer>();
        CoyoteTimeOG = CoyoteTime;
    }
    // Update is called once per frame
    void Update()
    {
        // Horizontal movement
        float x = Input.GetAxisRaw("Horizontal");
        Movement(x);
        Jump();
        CheckGrounded();
        
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

        if (Input.GetKeyDown(KeyCode.LeftShift) && dash && rb.velocity.x != 0)
        {
            StartCoroutine(Dash());
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if ((collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Wall" ) && rb.velocity.y <= 0.5 && isGrounded == false && !WallJumped)
        {
            isGrounded = true;
            doubleJump = true;
            falling = false;
            CoyoteTime = CoyoteTimeOG;
            anim.SetBool("isJumping", false);
            anim.SetBool("isFalling", false);
            if (rb.velocity.x > 0 || rb.velocity.x < 0)
            {
                anim.SetBool("isWalking", true);
                anim.Play("Run");
            }
            else
            {
                anim.SetBool("isWalking", false);
                anim.Play("Idle");
            }
        }
        if (collision.gameObject.tag == "Wall" && rb.velocity.y <= 0.5)
        {
            WallJump = true;
        }
        if (collision.gameObject.tag == "Ground")
        {
            WallJump = false;
            WallJumped = false;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if ((collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Wall") && rb.velocity.y <= 0.5)
        {
            falling = true;
        }
    }

    private void Movement(float x)
    {
        rb.velocity = new Vector2(x * speed, rb.velocity.y);

        // Running animation
        if (x > 0)
        {
            sr.flipX = false;
            if (isGrounded == true) {
                anim.SetBool("isWalking", true);
                anim.Play("Run");
            }
        }
        else if (x < 0)
        {
            sr.flipX = true;
            if (isGrounded == true) {
                anim.SetBool("isWalking", true);
                anim.Play("Run");
            }
        }
        if (rb.velocity.x == 0 && isGrounded == true)
        {
            anim.SetBool("isWalking", false);
            anim.Play("Idle");
        }
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && (isGrounded || doubleJump || !WallJumped && WallJump))
        {
            if (!isGrounded && doubleJump)
            {
                doubleJump = false;
            }
            if (WallJump)
            {
                WallJumped = true;
            }
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isGrounded = false;
            anim.SetBool("isJumping", true);
            anim.SetBool("isWalking", false);
            anim.Play("Jump");
        }
        if (!isGrounded && rb.velocity.y < -5) {
            anim.SetBool("isJumping", false);
            anim.SetBool("isFalling", true);
            anim.Play("Fall");
        }
    }

    private void CheckGrounded() 
    {
        if (CoyoteTime > 0 && falling == true)
        {
                CoyoteTime -= Time.deltaTime;
        }
        else if (CoyoteTime <= 0 && falling == true)
        {
                isGrounded = false;
                falling = false;
        }
    }

    private IEnumerator Dash()
    {
        float x = Input.GetAxisRaw("Horizontal");
        dash = false;
        // isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(rb.velocity.x * dashingPower, 0f);
        TR.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        TR.emitting = false;
        rb.gravityScale = originalGravity;
        // isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        dash = true;
    }

    public void Death()
    {
        Destroy(gameObject);
    }

    public void Respawn()
    {
        tr.position = respawnPoint;
    }

    public void SetRespawnPoint()
    {
        respawnPoint = tr.position;
    }
}
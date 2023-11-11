using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    private Animator anim;

    public Quaternion playerRotation;
    public Vector3 playerPosition;
    public Vector3 playerDirection;

    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private int maxJumps = 2;
    private int jumpsLeft;

    [SerializeField] private float coyoteTime = 0.2f;
    private float coyoteTimeCounter;
    [SerializeField] private float jumpBufferTime = 0.2f;
    private float jumpBufferCounter;

    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 10f;

    public float knockbackForce;
    public float knockbackCounter;
    public float knockbackTotalTime;
    public bool knockFromRight;

    public bool flippedLeft;
    public bool facingRight;

    public bool activatingDrums;
    public bool isInvisible;

    public bool canDash = true;
    public bool isDashing;
    [SerializeField] private float dashingPower = 24f;
    public float dashingTime = 0.2f;

    private enum MovementState { idle, walking, jumping, falling, landing }

    [SerializeField] private AudioSource jumpSoundEffect;
    // Need to figure out how to fix landing issue

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        jumpsLeft = maxJumps;
        playerRotation = transform.rotation;
        playerPosition = transform.position;
        playerDirection = transform.forward;
        isInvisible = false;
    }

    // Update is called once per frame
   private void Update()
    {
        if (isDashing)
        {
            return;
        }

        if (IsGrounded())
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        if (knockbackCounter <= 0)
        {
            if (!activatingDrums)
            {
                dirX = Input.GetAxisRaw("Horizontal");
                rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);
            }
            else
            {
                dirX = 0f;
                rb.velocity = Vector2.zero;
            }
        }
        else
        {
            if (knockFromRight == true)
            {
                rb.velocity = new Vector2(-knockbackForce, knockbackForce);
            }
            if (knockFromRight == false)
            {
                rb.velocity = new Vector2(knockbackForce, knockbackForce);
            }

            knockbackCounter -= Time.deltaTime;
        }
        if (IsGrounded() && rb.velocity.y <= 0f)
        {
            jumpsLeft = maxJumps;
        }
        else if ((IsGrounded() == false) && jumpsLeft > 1)
        {
            jumpsLeft = 1;
        }

        if (jumpBufferCounter > 0f && jumpsLeft > 0)
        {
            if (coyoteTime > 0f)
            {
                jumpSoundEffect.Play();
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                jumpsLeft -= 1;
                jumpBufferCounter = 0f;
            }
            else if (jumpsLeft > 0)
            {
                jumpSoundEffect.Play();
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                jumpsLeft -= 1;
                jumpBufferCounter = 0f;
            }
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            coyoteTimeCounter = 0f;
        }

        rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -100f, 100f), Mathf.Clamp(rb.velocity.y, -16f, 16f));

        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        MovementState moveState;
        
        if (isInvisible)
        {
            anim.SetBool("IsInvisible", (bool)true);
        }

        if (!isInvisible)
        {
            anim.SetBool("IsInvisible", (bool)false);
        }

        if (dirX > 0f)
        {
            facingRight = true;
            Flip(true);
            moveState = MovementState.walking;
        }
        else if (dirX < 0f)
        {
            facingRight = false;
            Flip(false);
            moveState = MovementState.walking;
        }
        else
        {
            moveState = MovementState.idle;
        }

        if (rb.velocity.y > .1f)
        {
            moveState = MovementState.jumping;
        }
        else if (rb.velocity.y < -.1f)
        {
            moveState = MovementState.falling;
        }

        anim.SetInteger("MovementState", (int)moveState);
    }

    public bool IsGrounded()
    {
        bool isGrounded = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
        if (isGrounded)
        {
            canDash = true;
        }
        return isGrounded;
    }

    public void Flip (bool facingRight)
    {
        if (flippedLeft && facingRight)
        {
            transform.Rotate(0, -180, 0);
            flippedLeft = false;
        }
        if (!flippedLeft && !facingRight)
        {
            transform.Rotate(0, -180, 0);
            flippedLeft = true;
        }
    }

    public IEnumerator SaxDash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2((transform.localScale.x * dashingPower) *  (flippedLeft ? 1 : -1  ), 0f);
        yield return new WaitForSeconds(dashingTime);
        rb.gravityScale = originalGravity;
        isDashing = false;
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Accordion"))
        {
            jumpsLeft = 1;
        }
    }
}

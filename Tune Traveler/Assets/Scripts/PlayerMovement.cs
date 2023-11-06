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

    private float dirX = 0f;
   [SerializeField] private float moveSpeed = 7f;
   [SerializeField] private float jumpForce = 10f;

    public float knockbackForce;
    public float knockbackCounter;
    public float knockbackTotalTime;
    public bool knockFromRight;

    public bool flippedLeft;
    public bool facingRight;

    public bool isInvisible;

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
        if (knockbackCounter <= 0)
        {
            dirX = Input.GetAxisRaw("Horizontal");
            rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);
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

        if (Input.GetButtonDown("Jump") && jumpsLeft > 0)
        {
            jumpSoundEffect.Play();
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpsLeft -= 1;
        }

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
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }

    void Flip (bool facingRight)
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
}

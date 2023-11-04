using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rbProjectile;

    [SerializeField] private float speed;
    [SerializeField] private float projectileLife;
    [SerializeField] private float projectileCount;

    public PlayerMovement playerMovement;
    public bool facingRight;

    void Start()
    {
        projectileCount = projectileLife;
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        facingRight = playerMovement.facingRight;
        if (!facingRight)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    void Update()
    {
        projectileCount -= Time.deltaTime;
        if (projectileCount <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        if (facingRight)
        {
            rbProjectile.velocity = new Vector2(speed, rbProjectile.velocity.y);
        }
        else
        {
            rbProjectile.velocity = new Vector2(-speed, rbProjectile.velocity.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(collision.gameObject);
        }
        Destroy(gameObject);
    }
}

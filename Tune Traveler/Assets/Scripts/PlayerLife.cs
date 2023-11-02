using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.U2D.Animation;
using UnityEngine.UI;

public class PlayerLife : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private BoxCollider2D coll;

    [SerializeField] private AudioSource deathSoundEffect;
    [SerializeField] private AudioSource hurtSoundEffect;
    public int playerHealth = 0;
    public int maxHealth = 6;
    private bool gotHurt = false;
    private enum DamageState { hurt, dead, normal }
    DamageState damageState;
    private bool beingHurt;
    public PlayerMovement playerMovement;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
        playerHealth = maxHealth;

        damageState = DamageState.normal;
        anim.SetInteger("DamageState", (int)damageState);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Hostile") && beingHurt == false)
        {
            if (playerHealth == 1)
            {
                playerHealth = playerHealth - 1;
                Debug.Log("Player Died");
                Die();
            }
            else
            {
                beingHurt = true;
                playerHealth = playerHealth - 1;
                Debug.Log("Health Left: " + playerHealth);
                Hurt();
            }

            gotHurt = false;
        }
    }

    private void Hurt()
    {
        gotHurt = true;
        hurtSoundEffect.Play();

        damageState = DamageState.hurt;
        StartCoroutine(HurtWaitFrame());
        playerMovement.knockbackCounter = playerMovement.knockbackTotalTime;
        if (coll.transform.position.x <= transform.position.x)
        {
            playerMovement.knockFromRight = true;
        }
        if (coll.transform.position.x > transform.position.x)
        {
            playerMovement.knockFromRight = false;
        }

    }

    private void Die()
    {
        deathSoundEffect.Play();
        rb.bodyType = RigidbodyType2D.Static;

        damageState = DamageState.dead;
        StartCoroutine(DeathWait());
    }



    IEnumerator HurtWaitFrame()
    {
        anim.SetInteger("DamageState", (int)damageState);
        yield return new WaitForSeconds(0.1f);
        damageState = DamageState.normal;
        anim.SetInteger("DamageState", (int)damageState);
        beingHurt = false;
    }

    IEnumerator DeathWait()
    {
        anim.SetInteger("DamageState", (int)damageState);
        yield return new WaitForSeconds(2f);
        RestartLevel();
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

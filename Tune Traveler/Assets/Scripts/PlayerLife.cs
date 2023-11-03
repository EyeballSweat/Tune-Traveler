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
    public float playerHealth = 0f;
    public float maxHealth = 3f;
    private bool gotHurt = false;
    private enum DamageState { hurt, dead, normal }
    DamageState damageState;
    private bool beingHurt;
    public PlayerMovement playerMovement;

    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite halfHeart;
    [SerializeField] private Sprite emptyHeart;

    [SerializeField] Image[] hearts;

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
        if (collision.gameObject.CompareTag("Trap") && beingHurt == false)
        {
            if (playerHealth == 0.5f)
            {
                playerHealth = playerHealth - 0.5f;
                Debug.Log("Player Died");
                Die();
            }
            else
            {
                beingHurt = true;
                playerHealth = playerHealth - 0.5f;
                Debug.Log("Health Left: " + playerHealth);
                Hurt();
            }

            gotHurt = false;
        }
    }

    public void Hurt()
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

    public void Die()
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

    void Update()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < playerHealth)
            {
                if (i + 0.5f == playerHealth)
                {
                    hearts[i].sprite = halfHeart;
                }
                else
                {
                    hearts[i].sprite = fullHeart;
                }
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
        }
    }

}

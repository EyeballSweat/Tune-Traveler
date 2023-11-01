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

    [SerializeField] private AudioSource deathSoundEffect;
    [SerializeField] private AudioSource hurtSoundEffect;
    public int playerHealth = 0;
    public int maxHealth = 6;
    private bool gotHurt = false;
    private enum DamageState { hurt, dead }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        playerHealth = maxHealth;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Hostile"))
        {
            if (playerHealth == 1)
            {
                playerHealth = playerHealth - 1;
                Debug.Log("Player Died");
                Die();
            }
            playerHealth = playerHealth - 1;
            Debug.Log("Health Left: " + playerHealth);
            Hurt();
            gotHurt = false;
        }
    }

    private void Hurt()
    {
        gotHurt = true;
        hurtSoundEffect.Play();
        anim.SetTrigger("Hurt");
    }

    private void Die()
    {
        deathSoundEffect.Play();
        rb.bodyType = RigidbodyType2D.Static;
        anim.SetTrigger("Death");
    }

    private void UpdateDamageState()
    {
        DamageState damageState;

        if (gotHurt == true)
        {
            if (playerHealth >= 2)
            {
                damageState = DamageState.hurt;
            }
            else if (playerHealth <= 1)
            {
                damageState = DamageState.dead;
            }
        }

        // FIX THIS anim.SetInteger("DamageState", (int)damageState);
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

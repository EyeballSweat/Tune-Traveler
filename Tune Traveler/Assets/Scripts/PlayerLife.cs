using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.U2D.Animation;

public class PlayerLife : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    [SerializeField] private AudioSource deathSoundEffect;
    [SerializeField] private AudioSource hurtSoundEffect;
    public int playerHealth = 0;
    public int maxHealth = 6;

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
        }
    }

    private void Hurt()
    {
        hurtSoundEffect.Play();
        anim.SetTrigger("Hurt");
    }

    private void Die()
    {
        deathSoundEffect.Play();
        rb.bodyType = RigidbodyType2D.Static;
        anim.SetTrigger("Death");
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

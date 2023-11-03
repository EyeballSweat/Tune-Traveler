using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public PlayerLife playerLife;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (playerLife.playerHealth == 0.5f)
            {
                playerLife.playerHealth = playerLife.playerHealth - 0.5f;
                playerLife.Die();
            }
            else
            {
                playerLife.playerHealth = playerLife.playerHealth - 0.5f;
                playerLife.Hurt();
            }
        }
    }
}

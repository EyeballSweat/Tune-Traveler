using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstructionCheck : MonoBehaviour
{
    public EnemyMovement enemyMovement;
    private BoxCollider2D obstructionCheck;
    [SerializeField] private bool isLeft;

    void Start()
    {
        obstructionCheck = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Summoned Piano")
        {
            if (isLeft)
            {
                enemyMovement.ObstructionRedirectionLeft();
            }
            else if (!isLeft)
            {
                enemyMovement.ObstructionRedirectionRight();
            }
        }
    }
}

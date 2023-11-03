using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Transform[] patrolPoints;
    [SerializeField] private float moveSpeed;
    private int patrolDestination;
    private SpriteRenderer sprite;
    private Animator anim;
    private enum EnemyMovementState { state0, state1, state2 };
    private float dirX = 0f;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");

        if (patrolDestination == 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, patrolPoints[0].position, moveSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, patrolPoints[0].position) <.1f)
            {
                UpdateEnemyMovementState();
                sprite.flipX = true;
                patrolDestination = 1;
            }
        }

        if (patrolDestination == 1)
        {
            transform.position = Vector2.MoveTowards(transform.position, patrolPoints[1].position, moveSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, patrolPoints[1].position) < .1f)
            {
                UpdateEnemyMovementState();
                sprite.flipX = false;
                patrolDestination = 0;
            }
        }
    }

    // Kind of a quick-fix at the moment
    private void UpdateEnemyMovementState()
    {
        EnemyMovementState enemyMovementState;

        if (dirX > 0f)
        {
            enemyMovementState = EnemyMovementState.state1;
        }
        else if (dirX < 0f)
        {
            enemyMovementState = EnemyMovementState.state1;
        }
        else
        {
            enemyMovementState = EnemyMovementState.state1;
        }

        anim.SetInteger("EnemyMoveState", (int)enemyMovementState);
    }
}

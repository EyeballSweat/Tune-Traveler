using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class InstrumentMechanics : MonoBehaviour
{
    private SpriteRenderer sprite;
    private Animator anim;
    public PlayerMovement playerMovement;
    public itemCollector itemCollector;
    private enum AttackState { passive, sax, piano, banjo, drums}

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        UpdateAttackState();
    }

    private void UpdateAttackState()
    {
        AttackState attackState;

        if (playerMovement.IsGrounded())
        {
            if (Input.GetKeyDown(KeyCode.RightArrow) && itemCollector.hasSax == true)
            {
                attackState = AttackState.sax;
            }
            if (Input.GetKeyDown(KeyCode.DownArrow) && itemCollector.hasPiano == true)
            {
                attackState = AttackState.piano;
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow) && itemCollector.hasBanjo == true)
            {
                attackState = AttackState.banjo;
            }
            if (Input.GetKeyDown(KeyCode.UpArrow) && itemCollector.hasDrums == true)
            {
                attackState = AttackState.drums;
            }
            else
            {
                attackState = AttackState.passive;
            }
        }
        else
        {
            attackState = AttackState.passive;
        }

       anim.SetInteger("AttackState", (int)attackState);

    }
}

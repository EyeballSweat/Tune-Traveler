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
    private AttackState attackState = AttackState.passive;

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

        attackState = AttackState.passive;

            if (Input.GetKeyDown(KeyCode.RightArrow) && itemCollector.hasSax == true)
            {
                attackState = AttackState.sax;
                Debug.Log("Sax go toot");
            }
            if (Input.GetKeyDown(KeyCode.DownArrow) && itemCollector.hasPiano == true && playerMovement.IsGrounded())
            {
                attackState = AttackState.piano;
                Debug.Log("Piano go FTANG");
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow) && itemCollector.hasBanjo == true)
            {
                attackState = AttackState.banjo;
                Debug.Log("Banjo go brrr");
            }
            if (Input.GetKeyDown(KeyCode.UpArrow) && itemCollector.hasDrums == true)
            {
                attackState = AttackState.drums;
                Debug.Log("Drums go badum tss");
            }

        

       anim.SetInteger("AttackState", (int)attackState);

    }
}

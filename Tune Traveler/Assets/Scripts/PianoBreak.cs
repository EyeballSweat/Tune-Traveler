using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PianoBreak : MonoBehaviour
{
    public GameObject pianoObject;
    public ProjectileLaunch projectileLaunch;
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            anim.SetTrigger("PianoHitGround");
        }
        else if (projectileLaunch.pianoSummoned == false)
        {
            anim.ResetTrigger("PianoHitGround");
        }
    }
}

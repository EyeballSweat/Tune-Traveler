using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PianoBreak : MonoBehaviour
{
    public GameObject pianoObject;
    public ProjectileLaunch projectileLaunch;
    private Animator anim;
    [SerializeField] private AudioSource pianoBreakAudio;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            pianoBreakAudio.Play();
            anim.SetTrigger("PianoHitGround");
        }
        else if (projectileLaunch.pianoSummoned == false)
        {
            anim.ResetTrigger("PianoHitGround");
        }
    }
}

using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class itemCollector : MonoBehaviour
{
    private int note = 0;

    [SerializeField] private Image noteImage;

    [SerializeField] private Image saxImageUI;
    [SerializeField] private Image pianoImageUI;
    [SerializeField] private Image banjoImageUI;
    [SerializeField] private Image drumsImageUI;
    public bool hasSax;
    public bool hasPiano;
    public bool hasBanjo;
    public bool hasDrums;

    [SerializeField] private AudioSource collectionSoundEffect;

    private void Start()
    {
        hasSax = false;
        hasPiano = false;
        hasBanjo = false;
        hasDrums = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Note"))
        {
            collectionSoundEffect.Play();
            Destroy(collision.gameObject);
            note++;
            noteImage.enabled = true;
        }
        if (collision.gameObject.CompareTag("Sax"))
        {
            collectionSoundEffect.Play();
            Destroy(collision.gameObject);
            hasSax = true;
            saxImageUI.enabled = true;
        }
        if (collision.gameObject.CompareTag("Piano"))
        {
            collectionSoundEffect.Play();
            Destroy(collision.gameObject);
            hasPiano = true;
            pianoImageUI.enabled = true;
        }
        if (collision.gameObject.CompareTag("Banjo"))
        {
            collectionSoundEffect.Play();
            Destroy(collision.gameObject);
            hasBanjo = true;
            banjoImageUI.enabled = true;
        }
        if (collision.gameObject.CompareTag("Drums"))
        {
            collectionSoundEffect.Play();
            Destroy(collision.gameObject);
            hasDrums = true;
            drumsImageUI.enabled = true;
        }
    }

   // Theoretically this isn't needed but is useful for bug checks so I'll keep it here for that reason. Just un-comment it when needed.
   // private void OnTriggerExit2D(Collider2D collision)
   // {
   //     if (collision.gameObject.CompareTag("Note"))
   //     {
   //         Destroy(collision.gameObject);
   //         note++;
   //         noteImage.enabled = false;
   //     }
   // }
}

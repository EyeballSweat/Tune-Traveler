using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class itemCollector : MonoBehaviour
{
    private int note = 0;

    [SerializeField] private Image noteImage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Note"))
        {
            Destroy(collision.gameObject);
            note++;
            noteImage.enabled = true;
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

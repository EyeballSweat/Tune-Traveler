using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchDoor : MonoBehaviour
{
    public BanjoEffectedSwitch banjoEffectedSwitch;
    [SerializeField] private GameObject doorOpen;
    [SerializeField] private GameObject doorClosed;
    private bool isOpen;

    private void Start()
    {
        isOpen = false;
        gameObject.GetComponent<SpriteRenderer>().sprite = doorClosed.GetComponent<SpriteRenderer>().sprite;
    }

    private void Update()
    {
        if (banjoEffectedSwitch.isOn == true)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = doorOpen.GetComponent<SpriteRenderer>().sprite;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            isOpen = true;
        }
        else if (banjoEffectedSwitch.isOn == false)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = doorClosed.GetComponent<SpriteRenderer>().sprite;
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
            isOpen = false;
        }
    }
}

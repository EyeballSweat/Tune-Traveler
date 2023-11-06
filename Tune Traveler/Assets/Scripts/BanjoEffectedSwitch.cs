using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanjoEffectedSwitch : MonoBehaviour
{
    [SerializeField] private GameObject switchOn;
    [SerializeField] private GameObject switchOff;
    public bool isOn = false;

    private void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = switchOff.GetComponent<SpriteRenderer>().sprite;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Banjo Waves" && !isOn)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = switchOn.GetComponent<SpriteRenderer>().sprite;
            isOn = true;
        }
        else if (collision.gameObject.tag == "Banjo Waves" && isOn)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = switchOff.GetComponent<SpriteRenderer>().sprite;
            isOn = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorGate : MonoBehaviour
{
    public Sensor sensor;
    [SerializeField] private GameObject gateOpen;
    [SerializeField] private GameObject gateClosed;
    private bool isOpen;

    void Start()
    {
        isOpen = false;
        gameObject.GetComponent<SpriteRenderer>().sprite = gateClosed.GetComponent<SpriteRenderer>().sprite;
    }

    void Update()
    {
        if (sensor.isAware == true)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = gateClosed.GetComponent<SpriteRenderer>().sprite;
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
            isOpen = false;
        }
        else if (sensor.isAware == false)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = gateOpen.GetComponent<SpriteRenderer>().sprite;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            isOpen = true;
        }
    }
}

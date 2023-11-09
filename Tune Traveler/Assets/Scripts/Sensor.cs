using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor : MonoBehaviour
{
    public PlayerMovement playerMovement;
    
    public GameObject sensorAware;
    public GameObject sensorUnaware;
    public bool isAware;

    private void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = sensorUnaware.GetComponent<SpriteRenderer>().sprite;
    }

    private void Update()
    {
        if (playerMovement.isInvisible == true)
        {
            isAware = false;
        }
        else
        {
            isAware = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = sensorUnaware.GetComponent<SpriteRenderer>().sprite;
    }
}

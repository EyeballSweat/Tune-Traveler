using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSmooth : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 offset;
    [SerializeField] [Range(1, 10)]
    private float smoothFactor;
    [SerializeField] private float verticalOffset;

    private void FixedUpdate()
    {
        Follow();
    }

    private void Follow()
    {
        Vector3 playerPosition = player.position + offset;
        Vector3 smoothPosition = Vector3.Lerp(transform.position, playerPosition, smoothFactor * Time.fixedDeltaTime);
        transform.position = new Vector3(smoothPosition.x, smoothPosition.y + verticalOffset, transform.position.z);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSmooth : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 offset;
    [SerializeField] [Range(1, 10)]
    private float smoothFactor;
    [SerializeField] private Vector3 minValues, maxValue;

    private void FixedUpdate()
    {
        Follow();
    }

    private void Follow()
    {
        Vector3 playerPosition = player.position + offset;

        Vector3 boundPosition = new Vector3(
            Mathf.Clamp(playerPosition.x, minValues.x, maxValue.x),
            Mathf.Clamp(playerPosition.y, minValues.y, maxValue.y),
            Mathf.Clamp(playerPosition.z, minValues.z, maxValue.z));

        Vector3 smoothPosition = Vector3.Lerp(transform.position, boundPosition, smoothFactor * Time.fixedDeltaTime);
        transform.position = smoothPosition;
    }
}

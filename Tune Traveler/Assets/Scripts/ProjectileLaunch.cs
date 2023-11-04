using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLaunch : MonoBehaviour
{
    public GameObject projectilePrefab;
    [SerializeField] private Transform launchPoint;

    [SerializeField] private float shootTime;
    [SerializeField] private float shootCounter;

    public itemCollector itemCollector;

    void Start()
    {
        shootCounter = shootTime;
    }

    void Update()
    {
        if (itemCollector.hasBanjo)
        {
            ShootBanjo();
        }
    }

    public void ShootBanjo()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && shootCounter <= 0)
        {
            Instantiate(projectilePrefab, launchPoint.position, Quaternion.identity);
            shootCounter = shootTime;
        }
        shootCounter -= Time.deltaTime;
    }
}

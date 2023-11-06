using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;

public class ProjectileLaunch : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public itemCollector itemCollector;
    public GameObject banjoWavePrefab;
    public GameObject pianoPrefab;
    [SerializeField] private Transform launchPoint;

    private Animator anim;
    [SerializeField] private LayerMask ground;

    [SerializeField] private float shootTime;
    [SerializeField] private float shootCounter;

    public bool pianoSummoned;
    [SerializeField] private float pianoSpawnDistance;
    private Vector3 pianoSpawnPosition;
    private GameObject clonedPiano = null;

    void Start()
    {
        anim = GetComponent<Animator>();
        anim.ResetTrigger("PianoHitGround");
        shootCounter = shootTime;
        pianoSpawnPosition = playerMovement.playerPosition + playerMovement.playerDirection * pianoSpawnDistance;
        pianoSummoned = false;
    }

    void Update()
    {
        if (itemCollector.hasBanjo)
        {
            ShootBanjo();
        }
        
        if (itemCollector.hasPiano)
        {
            SummonPiano();
        }

        if (itemCollector.hasDrums)
        {
            ActivateDrums();
        }
    }

    public void ShootBanjo()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && shootCounter <= 0)
        {
            playerMovement.isInvisible = false;

            Instantiate(banjoWavePrefab, launchPoint.position, Quaternion.identity);
            shootCounter = shootTime;
        }
        shootCounter -= Time.deltaTime;
    }

    public void SummonPiano()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow) && playerMovement.IsGrounded() && !pianoSummoned)
        {
            playerMovement.isInvisible = false;

            clonedPiano = Instantiate(pianoPrefab, launchPoint.position, Quaternion.identity);
            pianoSummoned = true;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && playerMovement.IsGrounded() && pianoSummoned)
        {
            playerMovement.isInvisible = false;

            Destroy(clonedPiano);
            anim.ResetTrigger("PianoHitGround");
            pianoSummoned = false;
        }
    }

    public void ActivateDrums()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && playerMovement.isInvisible == false)
        {
            playerMovement.isInvisible = true;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) && playerMovement.isInvisible == true)
        {
            playerMovement.isInvisible = false;
        }
    }
}

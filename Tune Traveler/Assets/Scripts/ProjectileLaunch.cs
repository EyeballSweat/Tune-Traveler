using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.Image;

public class ProjectileLaunch : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public itemCollector itemCollector;
    public GameObject banjoWavePrefab;
    public GameObject pianoPrefab;
    public GameObject saxProjectilePrefab;
    [SerializeField] private Transform launchPoint;

    private Animator anim;
    [SerializeField] private LayerMask ground;

    [SerializeField] private float shootTime;
    [SerializeField] private float shootCounter;

    public bool pianoSummoned;
    [SerializeField] private float pianoSpawnDistance;
    private Vector3 pianoSpawnPosition;
    private GameObject clonedPiano = null;
    [SerializeField] private Vector2 pianoOffset;


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

        if (itemCollector.hasSax)
        {
            SaxToot();
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
            if (playerMovement.flippedLeft)
            {
                clonedPiano = Instantiate(pianoPrefab, new Vector2(launchPoint.transform.position.x + -pianoOffset.x, launchPoint.transform.position.y + pianoOffset.y), Quaternion.identity);
            }  
            else
            {
                clonedPiano = Instantiate(pianoPrefab, new Vector2(launchPoint.transform.position.x + pianoOffset.x, launchPoint.transform.position.y + pianoOffset.y), Quaternion.identity);
            }
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
            playerMovement.activatingDrums = true;
            StartCoroutine(ActivatingDrums());
            playerMovement.isInvisible = true;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) && playerMovement.isInvisible == true)
        {
            playerMovement.isInvisible = false;
        }
    }

    public void SaxToot()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) && playerMovement.canDash == true)
        {
            Destroy(GameObject.Instantiate(saxProjectilePrefab, launchPoint.position, Quaternion.identity), playerMovement.dashingTime);
            StartCoroutine(playerMovement.SaxDash());
        }
    }

    IEnumerator ActivatingDrums()
    {
        yield return new WaitForSeconds(1);
        playerMovement.activatingDrums = false;
    }
}

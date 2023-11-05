using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;

public class ProjectileLaunch : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public GameObject banjoWavePrefab;
    public GameObject pianoPrefab;
    [SerializeField] private Transform launchPoint;

    private Animator anim;
    private BoxCollider2D coll;
    [SerializeField] private LayerMask ground;

    [SerializeField] private float shootTime;
    [SerializeField] private float shootCounter;

    public bool pianoSummoned;
    [SerializeField] private float pianoSpawnDistance;
    private Vector3 pianoSpawnPosition;
    private GameObject clonedPiano = null;

    public itemCollector itemCollector;

    void Start()
    {
        anim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
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
    }

    public void ShootBanjo()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && shootCounter <= 0)
        {
            Instantiate(banjoWavePrefab, launchPoint.position, Quaternion.identity);
            shootCounter = shootTime;
        }
        shootCounter -= Time.deltaTime;
    }

    public void SummonPiano()
    {

        

        if (Input.GetKeyDown(KeyCode.DownArrow) && playerMovement.IsGrounded() && !pianoSummoned)
        {
            clonedPiano = Instantiate(pianoPrefab, launchPoint.position, Quaternion.identity);
            pianoSummoned = true;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && playerMovement.IsGrounded() && pianoSummoned)
        {
            Destroy(clonedPiano);
            anim.ResetTrigger("PianoHitGround");
            pianoSummoned = false;
        }
    }
}

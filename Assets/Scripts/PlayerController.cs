using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Camera CellCamera;
    public Camera HallwayCamera;
    public Camera GuardCamera;

    Animator animator;

    public float playerSpeed;
    public float rotationSpeed;
    public float defaultHealth;
    float currentHealth;

    [HideInInspector] public bool inCell;
    [HideInInspector] public bool inHallway;
    [HideInInspector] public bool inGuardRoom;
    [HideInInspector] public float shakeTimer;

    [HideInInspector] public bool isInfected;
    [HideInInspector] public bool shaking = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        isInfected = false;
        currentHealth = defaultHealth;
        shakeTimer = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {

        Move();

        SetShakeParasites();

        SetInfected();

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Cell") && isInfected)
        {
            CellCamera.enabled = true;
            HallwayCamera.enabled = false;

        }

        if (other.CompareTag("Hallway"))
        {
            HallwayCamera.enabled = true;
            CellCamera.enabled = false;
            GuardCamera.enabled = false;

        }

        if (other.CompareTag("Guard"))
        {
            GuardCamera.enabled = true;
            HallwayCamera.enabled = false;

        }

    }

    private void Move()
    {
        float verticalInput = Input.GetAxisRaw("Vertical");
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        Vector3 movementDirection = new Vector3(0, 0, verticalInput);
        movementDirection.Normalize();

        // Forward Movement
        if (Input.GetKey(KeyCode.W) && !shaking)
        {
            transform.Translate(transform.forward * verticalInput * playerSpeed * Time.deltaTime, Space.World);

        }

        //Rotation
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.up, horizontalInput * rotationSpeed * Time.deltaTime);

        }

        //Set walking animations
        if (movementDirection.z > 0.0f && !shaking)
        {
            animator.SetBool("isWalking", true);

        }
        else
        {
            animator.SetBool("isWalking", false);

        }
    }

    private void SetShakeParasites()
    {
        //start shaking
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool("Shake", true);

            playerSpeed = 0.0f;

            shaking = true;
        }

        //start shake timer
        if (Input.GetKey(KeyCode.Space))
        {
            if(shakeTimer > 0.0f)
            {
                shakeTimer -= Time.deltaTime;
            }
        }

        //stop shaking
        if (Input.GetKeyUp(KeyCode.Space))
        {
            animator.SetBool("Shake", false);
            shakeTimer = 1.0f;

            playerSpeed = 5.0f;

            shaking = false;

        }


    }

    private void SetInfected()
    {
        //set infected and manage health
        if (isInfected)
        {
            playerSpeed = 0.75f;
            animator.SetBool("isInfected", true);
            currentHealth -= 0.5f * Time.deltaTime;
        }
        else
        {
            playerSpeed = 5.0f;
            animator.SetBool("isInfected", false);
        }
    }
}

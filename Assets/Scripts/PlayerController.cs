using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Camera CellCamera;
    public Camera HallwayCamera;
    public Camera GuardCamera;

    public event EventHandler OnDeath;

    Animator animator;
    Collider[] ragdollColliders;

    public float playerSpeed;
    public float rotationSpeed;

    [HideInInspector] public float currentHealth;
    [HideInInspector] public bool inCell;
    [HideInInspector] public bool inHallway;
    [HideInInspector] public bool inGuardRoom;
    [HideInInspector] public float shakeTimer;
    [HideInInspector] public bool isDead;

    [HideInInspector] public bool isInfected;
    [HideInInspector] public bool shaking = false;
    [HideInInspector] public bool attacking = false;

    bool ragDoll = true;
    bool isDamaged = false;
    float timer_damagedAnim = 1.1f;
    float attackCoolDown = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        isInfected = false;
        currentHealth = 1.0f;
        shakeTimer = 1.0f;
        ragdollColliders = this.gameObject.GetComponentsInChildren<Collider>();

        DisableRagDoll();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            Move();
            SetShakeParasites();
            SetInfected();
        }
        else if (ragDoll)
        {
            EnableRagDoll();
            ragDoll = false;
        }

        SetIsDamaged();

        SetAttacking();

    }


    private void SetAttacking()
    {
        if (Input.GetKeyDown(KeyCode.E) && !attacking)
        {
            Attack();
        }

        if (attacking && attackCoolDown > 0.0f)
        {
            attackCoolDown -= Time.deltaTime;
            playerSpeed = 0.0f;
        }
        else
        {
            attackCoolDown = 0.5f;

            if (!isInfected)
            {
                playerSpeed = 5.0f;

            }

            attacking = false;
        }
    }

    private void SetIsDamaged()
    {
        if (isDamaged && timer_damagedAnim > 0.0f)
        {
            timer_damagedAnim -= Time.deltaTime;
            playerSpeed = 0.0f;
        }

        if (timer_damagedAnim < 0.0f)
        {
            isDamaged = false;
            timer_damagedAnim = 1.1f;
            animator.ResetTrigger("Damaged");
        }
    }

    private void Attack()
    {
        animator.SetTrigger("attack");
        attacking = true;
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

        //if enemy hits player
        if(other.CompareTag("HitBox") && !isDamaged && !attacking)
        {
            isDamaged = true;
            animator.SetTrigger("Damaged");

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
            Debug.Log("Infected");
            playerSpeed = 0.75f;
            animator.SetBool("isInfected", true);

            //take damage
            if(currentHealth > 0)
            {
                currentHealth -= 0.05f * Time.deltaTime;
            }
            else
            {
                currentHealth = 0.0f;
                isDead = true;
                ragDoll = true;
            }
        }
        else
        {
            //un-infect
            playerSpeed = 5.0f;
            animator.SetBool("isInfected", false);
        }
    }

    private void DisableRagDoll()
    {
        foreach (Collider collider in ragdollColliders)
        {
            if(collider.gameObject != this.gameObject)
            {
                collider.isTrigger = true;
            }
            
        }

    }

    private void EnableRagDoll()
    {
        animator.enabled = false;
        animator.avatar = null;
        GetComponent<CapsuleCollider>().isTrigger = true;
        GetComponent<Rigidbody>().useGravity = false;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        playerSpeed = 0.0f;

        foreach (Collider collider in ragdollColliders)
        {
            if (collider.gameObject != this.gameObject)
            {
                collider.isTrigger = false;
                collider.attachedRigidbody.velocity = Vector3.zero;
            }

        }
    }
}

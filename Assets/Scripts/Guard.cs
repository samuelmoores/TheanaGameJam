using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class Guard : MonoBehaviour
{
    NavMeshAgent agent;
    GameObject player;
    PlayerController playerController;

    float distanceFromPlayer;
    bool playerAlive;
    float damagedCoolDown;

    [HideInInspector] public Animator animator;
    [HideInInspector] public float health = 0.5f;
    [HideInInspector] public bool isDead = false;
    [HideInInspector] public bool damaged = false;
    [HideInInspector] public bool attacking = false;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Prisoner");
        animator = GetComponent<Animator>();
        playerController = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        distanceFromPlayer = Vector3.Distance(transform.position, player.transform.position);
        playerAlive = !playerController.isDead;

        if(damaged && damagedCoolDown > 0.0f)
        {
            damagedCoolDown -= Time.deltaTime;
        }
        else if(!isDead)
        {
            damagedCoolDown = 0.5f;
            damaged = false;
            animator.SetBool("isDamaged", false);

        }

        //start interacting with player
        if (distanceFromPlayer < 10 && !isDead)
        {
            //start attacking
            if(distanceFromPlayer < 1.0f && playerAlive && !damaged && !playerController.attacking)
            {
                animator.SetBool("isWalking", false);
                animator.SetBool("isAttacking", true);
                attacking = true;
                agent.speed = 0.0f;
                agent.destination = transform.position;
            }
            //or chase
            else if(playerAlive && !damaged)
            {
                agent.destination = player.transform.position;
                animator.SetBool("isWalking", true);
                animator.SetBool("isAttacking", false);
                attacking = false;
                agent.speed = 1.0f;

            }
            //or stop becuase player is dead or guard is damaged
            else
            {
                animator.SetBool("isWalking", false);
                animator.SetBool("isAttacking", false);
                agent.speed = 0.0f;

            }
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player_DamageBox") && playerController.attacking)
        {
            Debug.Log("Guard hit by player");

            damaged = true;
            health -= 0.1f;
            animator.SetBool("isDamaged", true);

            if (health < 0.0f && !isDead)
            {
                animator.SetBool("isDead", true);
                isDead = true;

            }
        }
    }
}



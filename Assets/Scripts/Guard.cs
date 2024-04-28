using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Guard : MonoBehaviour
{
    NavMeshAgent agent;
    GameObject player;
    PlayerController playerController;
    Animator animator;
    float distanceFromPlayer;
    bool playerAlive;

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

        //start chasing
        if(distanceFromPlayer < 10)
        {
            //start attacking
            if(distanceFromPlayer < 0.5f && playerAlive)
            {
                animator.SetBool("isWalking", false);

                animator.SetBool("isAttacking", true);
                agent.speed = 0.0f;
                agent.destination = transform.position;
            }
            //or go back to chasing
            else if(playerAlive)
            {
                agent.destination = player.transform.position;
                animator.SetBool("isWalking", true);
                animator.SetBool("isAttacking", false);
                agent.speed = 1.0f;

            }
            //or stop becuase player is dead now
            else
            {
                animator.SetBool("isWalking", false);
                animator.SetBool("isAttacking", false);
            }
        }
        
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor.Networking.PlayerConnection;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Beetle : MonoBehaviour
{
    [HideInInspector] public bool playerFound = true;
    public Transform attachmentPoint;
    public GameObject DeadBeetle;
    public GameObject RespawnBeetle;

    GameObject Player;
    PlayerController playerController;
    NavMeshAgent agent;
    GameObject DeadBeetleInstance;
    GameObject RespawnBeetleInstance;
    bool attachedToPlayer = false;
    Vector3 spawnPosition;
    Quaternion startRot;
    float playerShakeTimer;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Prisoner");
        playerController = Player.GetComponent<PlayerController>();
        startRot = transform.rotation;
        spawnPosition = transform.position;
       

        //init AI
        agent = GetComponent<NavMeshAgent>();
        agent.speed = Random.Range(0.5f, 4.0f);
        agent.angularSpeed = Random.Range(110, 130);
        agent.acceleration = Random.Range(1, 5);

    }

    // Update is called once per frame
    void Update()
    {
        float distanceFromPlayer = Vector3.Distance(transform.position, Player.transform.position);

        float attachDistance = 1.0f;
        float chaseDistance_begin = 5.0f;
        playerShakeTimer = playerController.shakeTimer;

        Debug.Log(playerShakeTimer);

        //attach to player
        if (distanceFromPlayer < attachDistance && playerShakeTimer > 0.0f)
        {
            //Debug.Log("Attach");

            AttachToPlayer();
        }
        else if ((distanceFromPlayer < chaseDistance_begin || playerFound) && !attachedToPlayer)
        {
            //Debug.Log("Chase");
            ChasePlayer();
        }

        //detach from player
        if(attachedToPlayer && playerShakeTimer <= 0.0f)
        {
            Debug.Log("Detach");

            DetachFromPlayer();
        }


    }

    private void ChasePlayer()
    {
        playerFound = true;
        agent.destination = Player.transform.position;
    }

    private void AttachToPlayer()
    {
        attachedToPlayer = true;
        transform.position = attachmentPoint.position;
        transform.rotation = attachmentPoint.transform.rotation;
        transform.Rotate(0, -90.0f, 180.0f);

        GetComponent<BoxCollider>().enabled = false;
        playerController.isInfected = true;
    }

    private void DetachFromPlayer()
    {
        //spawn dead beetle
        SpawnDeadBeetle();

        //disinfect player
        playerController.isInfected = false;

        //respawn new beetle that automatically chases player
        RespawnBeetleInstance = GameObject.Instantiate(RespawnBeetle, spawnPosition, startRot);

        Destroy(gameObject);
    }

    private void SpawnDeadBeetle()
    {
        attachedToPlayer = false;
        DeadBeetleInstance = GameObject.Instantiate(DeadBeetle, attachmentPoint.position, attachmentPoint.rotation);
        DeadBeetleInstance.GetComponent<Rigidbody>().AddForce(transform.right * 5.0f, ForceMode.Impulse);
        DeadBeetleInstance.GetComponent<BoxCollider>().enabled = true;
        DeadBeetleInstance.GetComponent<CapsuleCollider>().enabled = true;

        Destroy(DeadBeetleInstance, 3);
    }
}

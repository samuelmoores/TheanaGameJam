using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor.Networking.PlayerConnection;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Beetle : MonoBehaviour
{
    private GameObject Player;
    private PlayerController playerController;
    private NavMeshAgent agent;
    public Transform attachmentPoint;
    bool attachedToPlayer = false;
    public GameObject DeadBeetle;
    public GameObject RespawnBeetle;
    GameObject RespawnBeetleInstance;
    GameObject DeadBeetleInstance;
    bool playerFound = false;
    public Vector3 respawnPosition;

    Quaternion startRot;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Prisoner");
        playerController = Player.GetComponent<PlayerController>();

        agent = GetComponent<NavMeshAgent>();
        agent.speed = Random.Range(0.5f, 4.0f);
        agent.angularSpeed = Random.Range(110, 130);
        agent.acceleration = Random.Range(1, 5);

        startRot = transform.rotation;

        Debug.Log("\n");

        //Debug.Log("Start position: " + respawnLocation.position);

    }

    // Update is called once per frame
    void Update()
    {

        float distanceFromPlayer = Vector3.Distance(transform.position, Player.transform.position);

        float attachDistance = 1.0f;
        float chaseDistance = 5.0f;

        //attach to player
        if (distanceFromPlayer < attachDistance && !playerController.shaking)
        {
            attachedToPlayer = true;
            transform.position = attachmentPoint.position;
            transform.rotation = attachmentPoint.transform.rotation;
            transform.Rotate(0, -90.0f, 180.0f);

            GetComponent<BoxCollider>().enabled = false;
        }
        else if (distanceFromPlayer < chaseDistance || playerFound)
        {
            playerFound = true;
            agent.destination = Player.transform.position;
        }

        if(Input.GetKeyDown(KeyCode.Space) && attachedToPlayer)
        {
            attachedToPlayer = false;
            GetComponent<BoxCollider>().enabled = false;
            GetComponent<NavMeshAgent>().enabled = false;

            //Debug.Log("Spawn position (respawn): " + respawnLocation.position);


            Debug.Log("Spawn position (DeadBeetle): " + DeadBeetle.transform.position);
            Debug.Log("\n");

            DeadBeetleInstance = GameObject.Instantiate(DeadBeetle, attachmentPoint.position, attachmentPoint.rotation);

            RespawnBeetleInstance = GameObject.Instantiate(RespawnBeetle, respawnPosition, startRot);

            RespawnBeetleInstance.GetComponent<BoxCollider>().enabled = true;
            RespawnBeetleInstance.GetComponent<NavMeshAgent>().enabled = true;

            Destroy(DeadBeetleInstance, 3);


            Destroy(gameObject);
        }

    }
}

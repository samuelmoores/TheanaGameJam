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
    GameObject DeadBeetleInstance;
    public Transform spawnTransform;
    bool playerFound = false;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Prisoner");
        playerController = Player.GetComponent<PlayerController>();

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
            Debug.Log("Spawn");

            GetComponent<BoxCollider>().enabled = false;
            GetComponent<NavMeshAgent>().enabled = false;

            DeadBeetleInstance = GameObject.Instantiate(DeadBeetle, transform.position, transform.rotation);
            DeadBeetleInstance.GetComponent<Rigidbody>().AddForce(transform.up * 100);
            Destroy(DeadBeetleInstance, 5);

            Destroy(gameObject);
        }

    }
}

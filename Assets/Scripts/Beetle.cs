using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Beetle : MonoBehaviour
{
    private GameObject Player;
    private NavMeshAgent agent;
    public Transform attachmentPoint;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        Player = GameObject.Find("Prisoner");
        agent.speed = Random.Range(0.5f, 4.0f);
        agent.angularSpeed = Random.Range(110, 130);
        agent.acceleration = Random.Range(1, 5);

    }

    // Update is called once per frame
    void Update()
    {
        float distanceFromPlayer = Vector3.Distance(agent.transform.position, Player.transform.position);

        if(distanceFromPlayer < 1.0f)
        {
            transform.position = attachmentPoint.position;
            transform.rotation = attachmentPoint.transform.rotation;
            transform.Rotate(0, -90.0f, 180.0f);

            GetComponent<BoxCollider>().enabled = false;
        }
        else
        {
            agent.destination = Player.transform.position;
        }

    }
}

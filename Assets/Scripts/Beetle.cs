using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.AI;

public class Beetle : MonoBehaviour
{
    private GameObject Player;
    private NavMeshAgent agent;
    bool playerFound = false;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        Player = GameObject.Find("Prisoner");
        agent.speed = Random.Range(0.5f, 4.0f);
        agent.angularSpeed = Random.Range(100, 200);
        agent.acceleration = Random.Range(1, 5);
    }

    // Update is called once per frame
    void Update()
    {
        float distanceFromPlayer = Vector3.Distance(agent.transform.position, Player.transform.position);

        if(distanceFromPlayer < 5.0f || playerFound)
        {
            playerFound = true;
            agent.destination = Player.transform.position;
        }

    }
}

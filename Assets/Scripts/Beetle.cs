using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Beetle : MonoBehaviour
{
    private GameObject Player;
    private NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        Player = GameObject.Find("Prisoner");
    }

    // Update is called once per frame
    void Update()
    {
        agent.destination = Player.transform.position;
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour
{
    PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Prisoner").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            playerController.currentHealth += 0.25f;

            if(playerController.currentHealth < 1.0f)
            {
                playerController.currentHealth = 1.0f;
            }

            Destroy(gameObject);
        }
    }
}

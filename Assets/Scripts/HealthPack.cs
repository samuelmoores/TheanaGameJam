using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour
{
    public GameObject currentHealthPack;
    Vector3 spawnPosition;

    GameObject newHealthPack;
    PlayerController playerController;
    bool pickedUp = false;
    float timer = 3.0f;
    float rotationSpeed = 20.0f;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Prisoner").GetComponent<PlayerController>();
        spawnPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(pickedUp)
        {
            if(timer > 0.0f)
            {
                timer -= Time.deltaTime;
                rotationSpeed += 1.0f;
                transform.Translate(Vector3.up * Time.deltaTime);
                Debug.Log(timer);

                if(timer < 1.0f)
                {
                    GetComponent<BoxCollider>().enabled = false;
                    GetComponent<MeshRenderer>().enabled = false;
                    transform.position = spawnPosition;
                }

            }
            else
            {
                Debug.Log("spawn new health pack");
                newHealthPack = GameObject.Instantiate(currentHealthPack, transform.position, transform.rotation);
                newHealthPack.GetComponent<BoxCollider>().enabled = true;
                newHealthPack.GetComponent<MeshRenderer>().enabled = true;

                Destroy(gameObject);
            }
        }

        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && playerController.currentHealth < 1.0f)
        {
            playerController.currentHealth += 0.25f;

            if(playerController.currentHealth < 1.0f)
            {
                playerController.currentHealth = 1.0f;
            }


            pickedUp = true;

        }
    }
}

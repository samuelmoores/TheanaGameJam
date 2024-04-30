using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    float attackCooldown = 0.0f;
    bool playerHit = false;
    PlayerController Player;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Prisoner").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(attackCooldown < 2.0f)
        {
            attackCooldown += Time.deltaTime;
        }
        else
        {
            attackCooldown = 2.0f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && attackCooldown > 0.75f)
        {
            attackCooldown = 0.0f;

            if(Player.currentHealth > 0.0f)
            {
                //damage player
                Player.currentHealth -= 0.1f;

                //is player dead?
                if (Player.currentHealth <= 0.0f)
                {
                    Player.isDead = true;
                }
            }

            
        }
    }
}

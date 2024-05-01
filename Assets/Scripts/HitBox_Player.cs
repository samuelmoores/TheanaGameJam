using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox_Player : MonoBehaviour
{
    PlayerController player;
    Guard guard;
    int attackTracker = 0;

    // Start is called before the first frame update
    void Start()
    {
        guard = GameObject.Find("Guard").GetComponent<Guard>();
        player = GameObject.Find("Prisoner").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy") && player.attacking)
        {
            guard.health -= 0.1f;

            if(guard.health < 0.0f && !guard.isDead)
            {
                guard.animator.SetBool("isDead", true);
                guard.isDead = true;
            }
        }
    }
}

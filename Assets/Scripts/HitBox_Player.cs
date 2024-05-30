using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox_Player : MonoBehaviour
{
    PlayerController player;
    Guard guard;
    Animator animator_guard;

    // Start is called before the first frame update
    void Start()
    {
        guard = GameObject.Find("Guard").GetComponent<Guard>();
        animator_guard = guard.GetComponent<Animator>();
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
            guard.damaged = true;
            guard.health -= 0.1f;
            animator_guard.SetTrigger("Damaged");
            Debug.Log("Damage Guard");

            if(guard.health < 0.0f && !guard.isDead)
            {
                guard.isDead = true;
            }
        }
    }
}

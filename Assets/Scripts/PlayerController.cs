using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator animator;

    public float playerSpeed;
    public float rotationSpeed;

    [HideInInspector] public bool isInfected;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        isInfected = false;
    }

    // Update is called once per frame
    void Update()
    {
        float verticalInput = Input.GetAxisRaw("Vertical");
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        Vector3 movementDirection = new Vector3(0, 0, verticalInput);
        movementDirection.Normalize();

        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(transform.forward * verticalInput * playerSpeed * Time.deltaTime, Space.World);


        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.up, horizontalInput * rotationSpeed * Time.deltaTime);

        }

        if (movementDirection.z > 0.0f)
        {
            animator.SetBool("isWalking", true);

        }
        else
        {
            animator.SetBool("isWalking", false);

        }

        if (isInfected)
        {
            playerSpeed = 0.75f;
            animator.SetBool("isInfected", true);
        }

    }
}

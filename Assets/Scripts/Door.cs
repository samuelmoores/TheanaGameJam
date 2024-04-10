using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    public Potion startPotion;
    GameObject DoorHinge;

    // Start is called before the first frame update
    void Start()
    {

        DoorHinge = GameObject.Find("DoorHinge");

    }

    // Update is called once per frame
    void Update()
    {

        if (startPotion.consumed)
        {
            if(DoorHinge.transform.rotation.y < 0.95f)
            {
                DoorHinge.transform.Rotate(Vector3.up, 5 * Time.deltaTime);

            }
        }

    }

}

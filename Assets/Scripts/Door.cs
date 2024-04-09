using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    public Potion startPotion;
    GameObject DoorHinge;
    int newRot;

    // Start is called before the first frame update
    void Start()
    {
        newRot = 0;

        DoorHinge = GameObject.Find("DoorHinge");

    }

    // Update is called once per frame
    void Update()
    {

        if (startPotion.consumed)
        {
            newRot++;

            DoorHinge.transform.Rotate(Vector3.up, 5 * Time.deltaTime);
        }

    }

}

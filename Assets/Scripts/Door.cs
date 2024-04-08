using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    public Potion startPotion;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<MeshRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {


        if (startPotion.consumed)
        {
            GetComponent<MeshRenderer>().enabled = true;
            Debug.Log("Consumed");

        }
    }
}

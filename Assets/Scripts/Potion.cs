using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    public bool consumed = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {

        if(other.gameObject.CompareTag("Player"))
        {
            //GameObject.Find("Prisoner").GetComponent<PlayerController>().isInfected = true;

            GameObject.Find("Camera_Player").GetComponent<Camera>().enabled = false;
            GameObject.Find("Camera_Cell").GetComponent<Camera>().enabled = true;
            GameObject.Find("OrbOverlay").GetComponent<Canvas>().enabled = true;

            GameObject.Find("Dungeon_Wall").GetComponent<MeshRenderer>().enabled = false;
            GameObject.Find("Dungeon_Wall").GetComponent<BoxCollider>().enabled = false;


            GameObject.Find("Door").GetComponent<MeshRenderer>().enabled = true;
            GameObject.Find("Dungeon_Wall_Door").GetComponent<MeshRenderer>().enabled = true;


            consumed = true;
        }



    }


}

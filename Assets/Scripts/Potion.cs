using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("StartGame");
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {

        if(other.gameObject.CompareTag("Player"))
        {
            GameObject.Find("Camera_Player").GetComponent<Camera>().enabled = false;
            GameObject.Find("Camera_Cell").GetComponent<Camera>().enabled = true;
            GameObject.Find("OrbOverlay").GetComponent<Canvas>().enabled = true;
        }



    }


}

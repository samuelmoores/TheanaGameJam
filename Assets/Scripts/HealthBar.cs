using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    PlayerController Player;
    Slider slider;
    

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Prisoner").GetComponent<PlayerController>();
        slider = GetComponent<Slider>();
        slider.value = Player.currentHealth;
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = Player.currentHealth;

    }
}

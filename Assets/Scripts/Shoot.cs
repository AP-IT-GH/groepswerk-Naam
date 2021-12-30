using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Shoot : MonoBehaviour
{
    private ShooterPlayer player;
    private bool IsPressed = false;

    void Start()
    {   
        player = GetComponentInParent<ShooterPlayer>();
    }


    void Update()
    {
        if (IsPressed)
        {
            player.Shoot();
        }
    }
}

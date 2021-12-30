using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Shoot : MonoBehaviour
{
    private ShooterPlayer player;
    [SerializeField]
    private GameObject controllerObject;
    private ActionBasedController controller;
    private bool IsPressed = false;
    // Start is called before the first frame update
    void Start()
    {
        controller = controllerObject.GetComponent<ActionBasedController>();      
        player = GetComponentInParent<ShooterPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        //InputHelpers.IsPressed(controller.inputDevice, InputHelpers.Button.PrimaryButton, out IsPressed);
        if (IsPressed)
        {
            player.Shoot();
        }
    }
}

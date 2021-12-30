using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;

[System.Serializable]
public class PrimaryButtonEvent : UnityEvent<bool> { }

public class PrimaryButtonWatcher : MonoBehaviour
{
    public PrimaryButtonEvent primaryButtonDown;
    private bool previousButtonState = false;
    private List<InputDevice> devicesPrimaryButton;
    //private List<InputDevice> leftHandedDevices;
    //private List<InputDevice> rightHandedDevices;

    private void Awake()
    {
        if (primaryButtonDown == null)
        {
            primaryButtonDown = new PrimaryButtonEvent();
        }


        devicesPrimaryButton = new List<InputDevice>();
        //leftHandedDevices = new List<InputDevice>();
        //rightHandedDevices = new List<InputDevice>();
    }
    private void OnEnable()
    {
        //InputDevices.GetDevicesAtXRNode(XRNode.LeftHand, leftHandedDevices);
        //InputDevices.GetDevicesAtXRNode(XRNode.RightHand, rightHandedDevices);

        List<InputDevice> allDevices = new List<InputDevice>();
        InputDevices.GetDevices(allDevices);
        foreach (InputDevice device in allDevices)
        {
            InputDevices_Connected(device);
        }

        InputDevices.deviceConnected += InputDevices_Connected;
        InputDevices.deviceDisconnected += InputDevices_Disconnected;

        //if (leftHandedDevices.Count > 1 || rightHandedDevices.Count > 1)
        //{
        //    Debug.Log("More than 1 left/right handed device found");
        //}
        //else if(leftHandedDevices.Count == 0 || rightHandedDevices.Count == 0)
        //{
        //    Debug.Log("none found");
        //    Debug.Log(rightHandedDevices.Count);
        //}
        //else
        //{
        //    Debug.Log("error");
        //}
    }

    void InputDevices_Connected(InputDevice device)
    {
        bool discardedValue;
        if (device.TryGetFeatureValue(CommonUsages.primaryButton, out discardedValue))
        {
            devicesPrimaryButton.Add(device);
        }
    }

    void InputDevices_Disconnected(InputDevice device)
    {
        if (devicesPrimaryButton.Contains(device))
        {
            devicesPrimaryButton.Remove(device);
        }
    }

    private void OnDisable()
    {
        InputDevices.deviceConnected -= InputDevices_Connected;
        InputDevices.deviceDisconnected -= InputDevices_Disconnected;
        devicesPrimaryButton.Clear();
        //rightHandedDevices.Clear();
        //leftHandedDevices.Clear();
    }

    private void Update()
    {

        bool tempstate = false;
        foreach (var device in devicesPrimaryButton)
        {
            bool primaryButtonState = false;
            // If we got a value and this is true then primaryButton is pressed, tempstate has to be false or else it's being held down
            tempstate = device.TryGetFeatureValue(CommonUsages.primaryButton, out primaryButtonState) && primaryButtonState || tempstate;
        }

        if (tempstate != previousButtonState)
        {
            primaryButtonDown.Invoke(tempstate);
            previousButtonState = tempstate;
        }
    }
}

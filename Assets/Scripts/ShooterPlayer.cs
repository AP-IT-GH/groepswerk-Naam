using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class PrimaryButtonEvent : UnityEvent<bool> { }

public class PrimaryButtonWatcher : MonoBehaviour
{
    public PrimaryButtonEvent primaryButtonDown;
    private bool previousButtonState = false;
    private List<InputDevice> leftHandedDevices;
    private List<InputDevice> rightHandedDevices;

    private void Awake()
    {
        if (primaryButtonDown == null)
        {
            primaryButtonDown = new PrimaryButtonEvent();
        }

        leftHandedDevices = new List<InputDevice>();
        rightHandedDevices = new List<InputDevice>();
    }
    private void OnEnable()
    {
        InputDevices.GetDevicesAtXRNode(XRNode.LeftHand, leftHandedDevices);
        InputDevices.GetDevicesAtXRNode(XRNode.RightHand, rightHandedDevices);

        if (leftHandedDevices.Count > 1 || rightHandedDevices.Count > 1)
        {
            Debug.Log("More than 1 left/right handed device found");
        }
    }

    private void OnDisable()
    {
        rightHandedDevices.Clear();
        leftHandedDevices.Clear();
    }

    private void Update()
    {

        bool tempstate = false;
        foreach (var rightHand in rightHandedDevices)
        {
            bool primaryButtonState = false;
            // If we got a value and this is true then primaryButton is pressed, tempstate has to be false or else it's being held down
            tempstate = rightHand.TryGetFeatureValue(CommonUsages.primaryButton, out primaryButtonState) && primaryButtonState || tempstate;
        }

        if (tempstate != previousButtonState)
        {
            primaryButtonDown.Invoke(tempstate);
            previousButtonState = tempstate;
        }
    }
}

public class ShooterPlayer : MonoBehaviour
{
    [SerializeField]
    private TextMeshPro scoreBoard;
    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private float bulletSpeed = 2500f;
    [SerializeField]
    private float fireRate = 0.5f;
    private float timer = 5f;
    private bool shoot = false;
    private float score = 0;
    private EnvironmentSpawner environment;

    private PrimaryButtonWatcher watcher;
    public bool isPressed = false;

    void Start()
    {
        environment = GetComponentInParent<EnvironmentSpawner>();
        environment.ClearEnvironment();
        transform.localPosition = new Vector3(0, 0, 0);

        watcher.primaryButtonDown.AddListener(onPrimaryButtonEvent);
    }

    void onPrimaryButtonEvent(bool pressed)
    {
        isPressed = pressed;
        if (pressed)
        {
            Shoot();
        }
    }

    void Update()
    {
        scoreBoard.text = score.ToString("f4");
    }


    public void Shoot()
    {
        while (isPressed) 
        {
            Debug.Log("Shoot");
        }
        if (timer > fireRate)
        {
            GameObject newBullet = Instantiate(bullet, new Vector3(0, 1, 0) + transform.forward, transform.rotation);
            newBullet.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed, ForceMode.VelocityChange);
            shoot = true;
            timer = 0f;
        }
        if (shoot)
        {
            if (timer < fireRate)
            {
                timer += Time.deltaTime;
            }
            else
            {
                timer = fireRate;

            }
        }
    }
    public void AddReward(float reward)
    {
        score += reward;
    }
}

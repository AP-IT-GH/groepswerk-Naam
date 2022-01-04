using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

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
    [SerializeField]
    private PrimaryButtonWatcher watcher;
    public bool isPressed = false;
    private Coroutine shootRoutine;

    void Start()
    {
        transform.localPosition = new Vector3(0, 0, 0);

        watcher.primaryButtonDown.AddListener(onPrimaryButtonEvent);
    }

    void onPrimaryButtonEvent(bool pressed)
    {
        isPressed = pressed;
        //Shoot();
        if (shootRoutine != null)
        {
            StopCoroutine(Shoot());
        }
        shootRoutine = StartCoroutine(Shoot());
    }

    void Update()
    {
        scoreBoard.text = "Score: " + score.ToString("f0");
    }


    public IEnumerator Shoot()
    {
        if (timer > fireRate)
        {
            GameObject newBullet = Instantiate(bullet, gameObject.transform.position + transform.forward, transform.rotation);
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
        yield return null;
    }
    public void AddReward(float reward)
    {
        score += reward;
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
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
    private EnvironmentSpawner environment;
    



    void Start()
    {
        
        environment = GetComponentInParent<EnvironmentSpawner>();
        environment.ClearEnvironment();
        transform.localPosition = new Vector3(0, 0, 0);
    }

    void Update()
    {
        scoreBoard.text = score.ToString("f4");
    }


    public void Shoot()
    {
        Debug.Log("Shoot");
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

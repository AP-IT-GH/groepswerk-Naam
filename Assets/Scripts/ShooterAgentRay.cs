using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class ShooterAgentRay : Agent
{
    [SerializeField]
    private TextMeshPro score;
    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private float bulletSpeed = 2500f;
    [SerializeField]
    private float fireRate = 0.5f;
    private float timer = 5f;
    private bool shoot = false;
    public int agentScore;
    private EnvironmentSpawnerAgent environment;

    private RayPerceptionSensorComponent3D sensorComponent;
    private RayPerceptionSensor sensor;
    private RayPerceptionOutput outputs;
    private Vector3 location;
    public Vector3 Location
    {
        get
        {
            return location;
        }
    }


    void Start()
    {
        environment = GetComponentInParent<EnvironmentSpawnerAgent>();
        sensorComponent = GetComponent<RayPerceptionSensorComponent3D>();
        sensor = sensorComponent.RaySensor;
        outputs = sensor.RayPerceptionOutput;
    }

    void Update()
    {
        //score.text = GetCumulativeReward().ToString("f6");
        score.text = agentScore.ToString();
    }

    public override void OnEpisodeBegin()
    {
        environment.ClearEnvironment();
        environment.StartEnvironment();
        transform.localPosition = new Vector3(0, 0, 0);
    }

    public void FindTarget()
    {
        if (location == Vector3.zero)
        {
            foreach (var rayOutput in outputs.RayOutputs)
            {
                if (rayOutput.HitTaggedObject && rayOutput.HitGameObject != null)
                {
                    Debug.Log("Target found");
                    location = rayOutput.HitGameObject.transform.position;
                    transform.LookAt(location);
                }
            }
        }
    }

    public void ClearTarget()
    {
        if (location != Vector3.zero)
        {
            location = Vector3.zero;
        }
    }

    public override void OnActionReceived(ActionBuffers actions)
    {

        var action = actions.DiscreteActions;
        if (action[0] == 1)
        {
            AddReward(-0.001f);
            FindTarget();
        }
        else if (action[0] == 2)
        {
            AddReward(-0.0001f);
            ClearTarget();
        }
        else if (action[0] == 3)
        {
            AddReward(-0.05f);
            Shoot();
        }
    }

    private void Shoot()
    {
        if (timer > fireRate)
        {
            GameObject newBullet = Instantiate(bullet, new Vector3(-180, 1, 0) + transform.forward, transform.rotation);
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

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var action = 0;
        if (Input.GetKey(KeyCode.Tab))
        {
            action = 1;
        }
        if (Input.GetKey(KeyCode.Mouse1))
        {
            action = 2;
        }
        if (Input.GetKey(KeyCode.Mouse0))
        {
            action = 3;
        }
        var discreteActionOut = actionsOut.DiscreteActions;
        discreteActionOut[0] = action;
    }
}

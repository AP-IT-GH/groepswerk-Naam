using TMPro;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using UnityEngine;

public class ShooterAgent : Agent
{
    [SerializeField]
    private TextMeshPro score;
    [SerializeField]
    private float turnSpeed = 1f;
    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private float bulletSpeed = 2500f;
    [SerializeField]
    private float fireRate = 0.5f;
    private float timer = 5f;
    private bool shoot = false;
    private EnvironmentSpawner environment;

    void Start()
    {
        environment = GetComponentInParent<EnvironmentSpawner>();  
    }

    void Update()
    {
        score.text = GetCumulativeReward().ToString("f4");
    }

    public override void OnEpisodeBegin()
    {
        environment.ClearEnvironment();
        transform.localPosition = new Vector3(0, 0, 0);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        var action = actions.DiscreteActions;
        if (action[0] == 1)
        {
            AddReward(0.00001f);
            TurnLeft();
        }
        else if (action[0] == 2)
        {
            AddReward(0.00001f);
            TurnRight();
        } 
        else if (action[0] == 3)
        {
            AddReward(-0.01f);
            Shoot();
        }
    }

    private void TurnLeft()
    {
        var turnSpeed = Time.deltaTime * this.turnSpeed;
        transform.eulerAngles -= new Vector3(0, turnSpeed, 0);
    }

    private void TurnRight()
    {
        var turnSpeed = Time.deltaTime * this.turnSpeed;
        transform.eulerAngles += new Vector3(0, turnSpeed, 0);
    }

    private void Shoot()
    {
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

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var action = 0;
        if (Input.GetKey(KeyCode.A))
        {
            action = 1;
        }
        if (Input.GetKey(KeyCode.D))
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

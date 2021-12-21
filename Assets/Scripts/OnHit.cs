using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHit : MonoBehaviour
{
    private GameObject agentObject;
    private ShooterAgent agent;

    private void Start()
    {
        agentObject = GameObject.FindGameObjectWithTag("Agent");
        agent = agentObject.GetComponent<ShooterAgent>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision detected");
        if (collision.gameObject.tag == "Bullet" && gameObject.tag == "Target")
        {
            Debug.Log("Target Deleted");
            agent.AddReward(1f);
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Bullet" && gameObject.tag == "Priority")
        {
            agent.AddReward(3f);
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Bullet" && gameObject.tag == "Ally")
        {
            agent.AddReward(-2f);
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }
}

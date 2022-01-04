using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestroyer : MonoBehaviour
{
    [Tooltip("Time before destroying in seconds")]
    public float lifeTime = 5.0f;
    private GameObject agentObject;
    private ShooterAgent agent;
    private ShooterAgentRay agentRay;
    private bool despawned = false;

    private void OnEnable()
    {
        agentObject = GameObject.FindGameObjectWithTag("Agent");
        agent = agentObject.GetComponent<ShooterAgent>();
        agentRay = agentObject.GetComponent<ShooterAgentRay>();
        StartCoroutine(GameObjectDestroy());
    }

    private IEnumerator GameObjectDestroy()
    {
        yield return new WaitForSeconds(lifeTime);
        despawned = true;
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        if (agentRay != null)
        {
            if (gameObject.transform.position.Equals(agentRay.Location))
            {
                agentRay.ClearTarget();
            }
        }
        if (despawned && agent != null)
        {
            switch (gameObject.tag)
            {
                case "Ally":
                    agent.AddReward(1f);
                    break;
                case "Priorty":
                    agent.AddReward(-1f);
                    //agent.AddReward(-0.1f);
                    break;
                case "Target":
                    agent.AddReward(-1f);
                    //agent.AddReward(-0.1f);
                    break;
                default:
                    break;
            }
        }
        else if (despawned && agentRay != null)
        {
            switch (gameObject.tag)
            {
                case "Ally":
                    agentRay.AddReward(1f);
                    break;
                case "Priorty":
                    agentRay.AddReward(-1f);
                    //agent.AddReward(-0.1f);
                    break;
                case "Target":
                    agentRay.AddReward(-1f);
                    //agent.AddReward(-0.1f);
                    break;
                default:
                    break;
            }
        }
        despawned = false;
    }
}

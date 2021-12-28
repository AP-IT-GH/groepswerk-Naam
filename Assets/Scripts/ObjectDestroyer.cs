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
    private bool despawned = false;

    private void OnEnable()
    {
        agentObject = GameObject.FindGameObjectWithTag("Agent");
        agent = agentObject.GetComponent<ShooterAgent>();
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
        if (despawned)
        {
            switch (gameObject.tag)
            {
                case "Ally":
                    agent.AddReward(0.5f);
                    break;
                case "Priorty":
                    agent.AddReward(-0.4f);
                    break;
                case "Target":
                    agent.AddReward(-0.4f);
                    break;
                default:
                    break;
            }
        }
        despawned = false;
    }
}

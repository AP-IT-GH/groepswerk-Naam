using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnHit : MonoBehaviour
{
    private GameObject agentObject;
    private ShooterAgent agent;
    private ShooterPlayer player;
    private ShooterAgentRay agentRay;

    private void Start()
    {
        agentObject = GameObject.FindGameObjectWithTag("Agent");
        agent = agentObject.GetComponent<ShooterAgent>();
        agentRay = agentObject.GetComponent<ShooterAgentRay>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet" && gameObject.tag == "Target")
        {
            Debug.Log("Hit Enemy");
            AddScore(5f, 1);
            DestroyObject(collision, gameObject);
        }
        else if (collision.gameObject.tag == "Bullet" && gameObject.tag == "Priority")
        {
            Debug.Log("Hit Priority");
            AddScore(10f, 3);
            DestroyObject(collision, gameObject);
        }
        else if (collision.gameObject.tag == "Bullet" && gameObject.tag == "Ally")
        {
            Debug.Log("Hit Ally");
            AddScore(-50f, -2);
            DestroyObject(collision, gameObject);
        }
        else if (collision.gameObject.tag == "BulletPlayer" && gameObject.tag == "Target")
        {
            player.AddReward(1f);
            DestroyObject(collision, gameObject);
        }
        else if (collision.gameObject.tag == "BulletPlayer" && gameObject.tag == "Priority")
        {
            player.AddReward(3f);
            DestroyObject(collision, gameObject);
        }
        else if (collision.gameObject.tag == "BulletPlayer" && gameObject.tag == "Ally")
        {
            player.AddReward(-2f);
            DestroyObject(collision, gameObject);
        }
        else if (collision.gameObject.tag == "BulletPlayer" && gameObject.tag == "GameOver")
        {
            Destroy(collision.gameObject);
            SceneManager.LoadScene("backup");
        }
    }

    private void DestroyObject(Collision collision, GameObject gameObject)
    {
        Destroy(gameObject);
        Destroy(collision.gameObject);
    }

    private void AddScore(float reward, int score)
    {
        if (agent != null)
        {
            agent.AddReward(reward);
            agent.agentScore += score;
        } else if (agentRay != null)
        {
            agentRay.AddReward(reward);
            agentRay.agentScore += score;
            agentRay.ClearTarget();
        }
    }
}

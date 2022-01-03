using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnHit : MonoBehaviour
{
    private GameObject agentObject;
    private GameObject playerObject;
    private ShooterAgent agent;
    private ShooterPlayer player;

    private void Start()
    {
        agentObject = GameObject.FindGameObjectWithTag("Agent");
        playerObject = GameObject.FindGameObjectWithTag("Player");
        agent = agentObject.GetComponent<ShooterAgent>();
        player = playerObject.GetComponent<ShooterPlayer>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet" && gameObject.tag == "Target")
        {
            agent.AddReward(1f);
            agent.agentScore += 1;
            DestroyObject(collision, gameObject);
        }
        else if (collision.gameObject.tag == "Bullet" && gameObject.tag == "Priority")
        {
            agent.AddReward(3f);
            agent.agentScore += 3;
            DestroyObject(collision, gameObject);
        }
        else if (collision.gameObject.tag == "Bullet" && gameObject.tag == "Ally")
        {
            agent.AddReward(-2f);
            agent.agentScore -= 2;
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
        else if (collision.gameObject.tag == "BulletPlayer" && gameObject.tag == "Wall")
        {
            Destroy(collision.gameObject);
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
}

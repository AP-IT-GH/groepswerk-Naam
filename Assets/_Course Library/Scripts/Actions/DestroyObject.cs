using UnityEngine;

/// <summary>
/// Destroys object after a few seconds
/// </summary>
public class DestroyObject : MonoBehaviour
{
    [Tooltip("Time before destroying in seconds")]
    public float lifeTime = 5.0f;
    private ShooterAgent agent;

    private void Start()
    {
        GameObjectDestroy();
    }

    private void GameObjectDestroy()
    {
        Destroy(gameObject, lifeTime);
        if (gameObject.tag == "Ally")
        {
            agent.AddReward(.5f);
        }
    }
}

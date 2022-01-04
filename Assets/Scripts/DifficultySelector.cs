using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultySelector : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "BulletPlayer" && gameObject.tag == "Easy")
        {
            gameManager.SetDifficulty(1);
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "BulletPlayer" && gameObject.tag == "Normal")
        {
            gameManager.SetDifficulty(2);
            Destroy(collision.gameObject);

        }
        else if (collision.gameObject.tag == "BulletPlayer" && gameObject.tag == "Hard")
        {
            gameManager.SetDifficulty(3);
            Destroy(collision.gameObject);
        }
    }
}

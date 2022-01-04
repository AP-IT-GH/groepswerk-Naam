using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WallCollider : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "BulletPlayer" && gameObject.tag == "GameOver")
        {
            Destroy(collision.gameObject);
            SceneManager.LoadScene("backup");
        }
    }
}

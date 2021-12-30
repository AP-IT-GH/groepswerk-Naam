using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshPro timer;
    public EnvironmentSpawner Environment;
    [SerializeField]
    private TextMeshPro scoreBoard;
    [SerializeField]
    private TextMeshPro gameOverCanvas;
    private int awaitTime;
    public enum GameStates
    {
        Playing,
        GameOver
    }
    public GameStates gameState = GameStates.Playing;
    // Start is called before the first frame update
    void Start()
    {
        awaitTime = 60;
        StartCoroutine(GameOver());
        scoreBoard.gameObject.SetActive(true);
        timer.gameObject.SetActive(true);
        gameOverCanvas.gameObject.SetActive(false);
    }

    private IEnumerator GameOver()
    {
        switch (gameState)
        {
            case GameStates.Playing:
                for (int i = awaitTime; i > -1; i--)
                {
                    yield return new WaitForSeconds(1);
                    timer.text = i.ToString();
                }
                Debug.Log("einde spel");
                scoreBoard.gameObject.SetActive(false);
                timer.gameObject.SetActive(false);
                gameOverCanvas.gameObject.SetActive(true);
                gameOverCanvas.text = "End " + scoreBoard.text;
                Environment.ClearEnvironment();
                gameState = GameStates.GameOver;
                break;
        }
    }
}

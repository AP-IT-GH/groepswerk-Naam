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
    public EnvironmentSpawnerAgent AgentEnvironment;
    [SerializeField]
    private TextMeshPro scoreBoard;
    [SerializeField]
    private TextMeshPro gameOverCanvas;
    [SerializeField]
    private GameObject gameOverButton;
    [SerializeField]
    private int awaitTime = 60;
    [SerializeField]
    private GameObject easyAgent;
    [SerializeField]
    private GameObject normalAgent;
    [SerializeField]
    private GameObject hardAgent;
    [SerializeField]
    private EnvironmentSpawner spawner;
    [SerializeField]
    private EnvironmentSpawnerAgent spawnerAgent;

    public enum GameStates
    {
        Playing,
        GameOver
    }
    private GameStates gameState = GameStates.Playing;

    void Start()
    {
        scoreBoard.gameObject.SetActive(true);
        timer.gameObject.SetActive(true);
        gameOverCanvas.gameObject.SetActive(false);
        gameOverButton.gameObject.SetActive(false);

        easyAgent.gameObject.SetActive(false);
        normalAgent.gameObject.SetActive(false);
        hardAgent.gameObject.SetActive(false);
    }

    public void SetDifficulty(int difficulty)
    {
        switch (difficulty)
        {
            case 1:
                easyAgent.gameObject.SetActive(true);
                break;
            case 2:
                normalAgent.gameObject.SetActive(true);
                break;
            case 3:
                hardAgent.gameObject.SetActive(true);
                break;
        }
        StartCoroutine(GameOver());
    }

    private IEnumerator GameOver()
    {
        switch (gameState)
        {
            case GameStates.Playing:
                spawner.enabled = true;
                spawnerAgent.enabled = true;
                spawner.ClearEnvironment();
                spawner.StartEnvironment();

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
                AgentEnvironment.ClearEnvironment();
                gameOverButton.gameObject.SetActive(true);
                gameState = GameStates.GameOver;
                break;
        }
    }
}

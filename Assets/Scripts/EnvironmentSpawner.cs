using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentSpawner : MonoBehaviour
{
    public GameObject ally;
    public GameObject target;
    public GameObject priority;
    public GameObject allyPrefab;
    public GameObject priorityPrefab;
    public GameObject targetPrefab;

    [SerializeField]
    private bool spawnTargets = false;
    [SerializeField]
    private bool spawnAllies = false;
    [SerializeField]
    private bool spawnPriorities = false;

    private void OnEnable()
    {
        target = transform.Find("Target").gameObject;
        ally = transform.Find("Ally").gameObject;
        priority = transform.Find("Priority").gameObject;
        StartCoroutine(Spawner());
        StartCoroutine(AllySpawner());
        StartCoroutine(PrioritySpawner());
    }

    private IEnumerator Spawner()
    {
        while (spawnTargets)
        {
            var waitTime = Random.Range(0.5f, 2f);
            yield return new WaitForSeconds(waitTime);
            SpawnTarget(target, targetPrefab);
        }
    }

    private IEnumerator AllySpawner()
    {
        while (spawnAllies)
        {
            var waitTime = Random.Range(10f, 20f);
            yield return new WaitForSeconds(waitTime);
            SpawnTarget(ally, allyPrefab);
        }
    }
    private IEnumerator PrioritySpawner()
    {
        while (spawnPriorities)
        {
            var waitTime = Random.Range(15f, 25f);
            yield return new WaitForSeconds(waitTime);
            SpawnTarget(priority, priorityPrefab);
        }
    }

    private void SpawnTarget(GameObject gameObject, GameObject gamePrefab)
    {
        var random = Random.Range(1, 5);
        float zRandom;
        float xRandom;
        switch (random)
        {
            case 1:
                zRandom = Random.Range(10f, 40f);
                xRandom = Random.Range(10f, 40f);
                break;
            case 2:
                zRandom = Random.Range(10f, 40f);
                xRandom = Random.Range(-10f, -40f);
                break;
            case 3:
                zRandom = Random.Range(-10f, -40f);
                xRandom = Random.Range(-10f, -40f);
                break;
            case 4:
                zRandom = Random.Range(-10f, -40f);
                xRandom = Random.Range(10f, 40f);
                break;
            default:
                zRandom = 0;
                xRandom = 0;
                break;
        }
        GameObject newTarget = Instantiate(gamePrefab);
        newTarget.transform.SetParent(gameObject.transform);
        newTarget.transform.localPosition = new Vector3(xRandom, 0, zRandom);
        newTarget.transform.LookAt(Vector3.zero);
    }

    public void ClearEnvironment()
    {
        StopAllCoroutines();

        foreach (Transform target in target.transform)
        {
            GameObject.Destroy(target.gameObject);
        }
        foreach (Transform ally in ally.transform)
        {
            GameObject.Destroy(ally.gameObject);
        }
        foreach (Transform priority in priority.transform)
        {
            GameObject.Destroy(priority.gameObject);
        }

        StartCoroutine(Spawner());
        StartCoroutine(AllySpawner());
        StartCoroutine(PrioritySpawner());
    }
}

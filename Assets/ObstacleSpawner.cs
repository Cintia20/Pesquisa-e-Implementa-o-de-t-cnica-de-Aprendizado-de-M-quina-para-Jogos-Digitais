using UnityEngine;
using System.Collections.Generic;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public float spawnRate = 2f;
    public float obstacleGap = 3f;
    public float spawnDistance = 10f;

    private float nextSpawnTime = 0f;
    private Transform birdTransform;
    private List<GameObject> obstacles = new List<GameObject>();
    private Vector3 nextObstaclePos;
    private float nextGapY;

    void Start()
    {
        birdTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (Time.time > nextSpawnTime)
        {
            SpawnObstacle();
            nextSpawnTime = Time.time + spawnRate;
        }
    }

    void SpawnObstacle()
    {
        // Posição X à frente do pássaro
        float spawnX = birdTransform.position.x + spawnDistance;

        // Posição Y aleatória para o gap
        nextGapY = Random.Range(-2f, 2f);
        nextObstaclePos = new Vector3(spawnX, nextGapY, 0);

        // Criar obstáculo superior
        GameObject topObstacle = Instantiate(obstaclePrefab,
            new Vector3(spawnX, nextGapY + (obstacleGap / 2) + 5f, 0), Quaternion.identity);
        topObstacle.transform.localScale = new Vector3(4f, 10f, 1f);
        topObstacle.tag = "Obstacle";
        obstacles.Add(topObstacle);

        // Criar obstáculo inferior
        GameObject bottomObstacle = Instantiate(obstaclePrefab,
            new Vector3(spawnX, nextGapY - (obstacleGap / 2) - 5f, 0), Quaternion.identity);
        bottomObstacle.transform.localScale = new Vector3(4f, 10f, 1f);
        bottomObstacle.tag = "Obstacle";
        obstacles.Add(bottomObstacle);
    }

    public bool GetNextObstaclePosition(out Vector3 position, out float gapY)
    {
        if (obstacles.Count > 0)
        {
            position = nextObstaclePos;
            gapY = nextGapY;
            return true;
        }
        position = Vector3.zero;
        gapY = 0f;
        return false;
    }

    public void ClearObstacles()
    {
        foreach (GameObject obstacle in obstacles)
        {
            if (obstacle != null)
                Destroy(obstacle);
        }
        obstacles.Clear();
    }
}
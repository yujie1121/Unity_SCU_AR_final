using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField, Header("要生成的預製物")]
    private GameObject prefabToSpawn;
    [SerializeField, Header("生成點陣列")]
    private Transform[] spawnPoints;
    [SerializeField, Header("生成間隔"), Range(0, 5)]
    private float spawnInterval = 3;

    private void Awake()
    {
        InvokeRepeating("SpawnEnemy", 0, spawnInterval);
    }

    private void SpawnEnemy()
    {
        int random = Random.Range(0, spawnPoints.Length);
        Instantiate(prefabToSpawn,
            spawnPoints[random].position, spawnPoints[random].rotation);
    }
}

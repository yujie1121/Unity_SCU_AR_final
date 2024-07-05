using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField, Header("�n�ͦ����w�s��")]
    private GameObject prefabToSpawn;
    [SerializeField, Header("�ͦ��I�}�C")]
    private Transform[] spawnPoints;
    [SerializeField, Header("�ͦ����j"), Range(0, 5)]
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

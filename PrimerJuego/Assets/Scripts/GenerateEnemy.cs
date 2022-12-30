using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateEnemy : MonoBehaviour
{
    public GameObject EnemyPrefab;
    public float spawnTime = 15;
    public float SpawnRadius = 1;
    public float lastEnemyTime = 10f;

    void EnemyGenerate()
    {
        Vector3 randomPosition = transform.position + Random.insideUnitSphere * SpawnRadius;
        randomPosition.y = 0.5f;
        GameObject.Instantiate(EnemyPrefab, randomPosition, Quaternion.identity);
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    //segundos desde que se inicia el juego
    {
        if (Time.time - lastEnemyTime > spawnTime)
        {
            EnemyGenerate();
            lastEnemyTime = Time.time;
        }
    }
}

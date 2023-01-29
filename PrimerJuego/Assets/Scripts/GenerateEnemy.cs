using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateEnemy : MonoBehaviour
{
    public GameObject EnemyPrefab;
    public float spawnTime = 5;
    public float SpawnRadius = 3;
    public float lastEnemyTime = 10f;
    private float numberOfEnemys = 0;
    public float maxNumberOfEnemys = 5;

    void EnemyGenerate()
    {
        Vector3 randomPosition = transform.position + Random.insideUnitSphere * SpawnRadius;
        randomPosition.y = 1f;
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
        if ((Time.time - lastEnemyTime > spawnTime) && numberOfEnemys <= maxNumberOfEnemys)
        {
            EnemyGenerate();
            ++numberOfEnemys;
            lastEnemyTime = Time.time;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public GameObject enemy;
    public float spawnTime = 2.0f;
    public float spawnRadius = 2.0f;
    // public bool canSpawn = true;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("GenerateEnemy", spawnTime, spawnRadius);
    }

    // Update is called once per frame
    void Update()
    {
        // canSpawn = false;
        // StartCoroutine(WaitForMove(spawnTime));
        // if (canSpawn == true) {
        //     GenerateEnemy();
        //     canSpawn = false;
        // }
    }

    public void GenerateEnemy()
    {
        Instantiate(enemy, new Vector3(Random.Range(-10.0f, 10.0f), 0, Random.Range(-10.0f, 10.0f)), Quaternion.identity);
    }

    // IEnumerator WaitForMove(float time) {
    //     yield return new WaitForSeconds(time);
    //     canSpawn = true;
    // }
}

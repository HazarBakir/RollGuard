using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    private float spawnRange = 9f;
    public int enemyCount;
    public int waveNumber = 1;
    public GameObject powerupPrefab;
    void Start()
    {
        SpawnEnemyWave(waveNumber);
    }

    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;
        if (enemyCount == 0)
        {
            waveNumber++;
            SpawnEnemyWave(waveNumber);
            SpawnPowerups();
        }
    }
    void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
        Instantiate(enemyPrefab, GenerateSpawnOption(), enemyPrefab.transform.rotation);
        }
    }
    void SpawnPowerups()
    {
        Instantiate(powerupPrefab, GenerateSpawnOption(), powerupPrefab.transform.rotation);
    }
    private Vector3 GenerateSpawnOption()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPosition = new Vector3(spawnPosX, 0, spawnPosZ);
        return randomPosition;
    }
}

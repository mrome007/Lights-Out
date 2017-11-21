using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveController : MonoBehaviour 
{
    [SerializeField]
    private LightsOutController lightsOutController;

    [SerializeField]
    private Transform playerTransform;

    [SerializeField]
    private List<LightsOutEnemy> lightsOutEnemies;

    [SerializeField]
    private Transform leftSpawnPoint;
    [SerializeField]
    private Transform rightSpawnPoint;
    [SerializeField]
    private Transform topSpawnPoint;
    [SerializeField]
    private Transform bottomSpawnPoint;

    [SerializeField]
    private int baseNumberOfEnemies;
    [SerializeField]
    private int incrRateOfEnemies;

    private int waveNumber = 1;

    private float timeToSpawnEnemy = 3.0f;
    private float timeBetweenWaves = 5.0f;

    private void Awake()
    {
        lightsOutController.LightsOut += LightsAreOut;
    }

    private void OnDestroy()
    {
        lightsOutController.LightsOut -= LightsAreOut;
    }

    private void LightsAreOut(object sender, EventArgs e)
    {
        lightsOutController.LightsOut -= LightsAreOut;

        StartCoroutine(EnemyWave());
    }

    private IEnumerator EnemyWave()
    {
        while(true)
        {
            Debug.Log("Wave Number: " + waveNumber);
            for(int index = 0; index < baseNumberOfEnemies; index++)
            {
                StartCoroutine(SpawnEnemy());
            }
            yield return new WaitForSeconds(timeBetweenWaves);
            baseNumberOfEnemies += incrRateOfEnemies;
            waveNumber++;
        }
    }

    private IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(0f, timeToSpawnEnemy));
        var enemyType = PickEnemy();
        var enemyPosition = PickSpawnPoint();
        var direction = playerTransform.position - enemyPosition;

        var enemy = Instantiate(enemyType, enemyPosition, Quaternion.identity);
        enemy.Initialize(direction, 4f);
    }

    private Vector3 PickSpawnPoint()
    {
        var spawnPoint = Vector3.zero;
        var random = UnityEngine.Random.Range(0, 4);
        switch(random)
        {
            case 0:
                spawnPoint = leftSpawnPoint.position;
                spawnPoint.y = UnityEngine.Random.Range(-8f, 8f);
                break;
            case 1:
                spawnPoint = rightSpawnPoint.position;
                spawnPoint.y = UnityEngine.Random.Range(-8f, 8f);
                break;
            case 2:
                spawnPoint = topSpawnPoint.position;
                spawnPoint.x = UnityEngine.Random.Range(-15f, 15f);
                break;
            case 3:
                spawnPoint = bottomSpawnPoint.position;
                spawnPoint.x = UnityEngine.Random.Range(-15f, 15f);
                break;
            default:
                spawnPoint = topSpawnPoint.position;
                break;
        }

        return spawnPoint;
    }

    private LightsOutEnemy PickEnemy()
    {
        return lightsOutEnemies[UnityEngine.Random.Range(0, lightsOutEnemies.Count)];
    }
}

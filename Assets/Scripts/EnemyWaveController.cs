using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    private float enemySpeed;
    [SerializeField]
    private int baseNumberOfEnemies;
    [SerializeField]
    private int incrRateOfEnemies;

    [SerializeField]
    private GameObject waveTextContainer;
    [SerializeField]
    private Text waveNumberText;

    public static int WaveNumber = 1;

    private float timeToSpawnEnemy = 1.5f;
    private float timeBetweenWaves = 6.0f;

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
        UpdateWaveText();
    }

    private IEnumerator EnemyWave()
    {
        while(true)
        {
            for(int index = 0; index < baseNumberOfEnemies; index++)
            {
                StartCoroutine(SpawnEnemy());
            }
            yield return new WaitForSeconds(timeBetweenWaves);
            var increaseEnemies = UnityEngine.Random.Range(0, 100);
            if(increaseEnemies >= 55)
            {
                baseNumberOfEnemies += incrRateOfEnemies;
            }
            WaveNumber++;
            UpdateWaveText();
        }
    }

    private IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(0f, timeToSpawnEnemy));
        var enemyType = PickEnemy();
        var enemyPosition = PickSpawnPoint();
        var direction = playerTransform.position - enemyPosition;

        var enemy = Instantiate(enemyType, enemyPosition, Quaternion.identity);
        enemy.Initialize(direction, enemySpeed);
    }

    private Vector3 PickSpawnPoint()
    {
        var spawnPoint = Vector3.zero;
        var random = UnityEngine.Random.Range(0, 4);
        switch(random)
        {
            case 0:
                spawnPoint = leftSpawnPoint.position;
                spawnPoint.y = UnityEngine.Random.Range(-5f, 5f);
                break;
            case 1:
                spawnPoint = rightSpawnPoint.position;
                spawnPoint.y = UnityEngine.Random.Range(-5f, 5f);
                break;
            case 2:
                spawnPoint = topSpawnPoint.position;
                spawnPoint.x = UnityEngine.Random.Range(-10f, 10f);
                break;
            case 3:
                spawnPoint = bottomSpawnPoint.position;
                spawnPoint.x = UnityEngine.Random.Range(-10f, 10f);
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

    private void UpdateWaveText()
    {
        waveTextContainer.SetActive(true);
        waveNumberText.text = WaveNumber.ToString();
    }
}

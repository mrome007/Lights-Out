using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Enemy wave controller.
/// </summary>
public class EnemyWaveController : MonoBehaviour 
{
    #region Inspector elements

    /// <summary>
    /// The lights out controller.
    /// </summary>
    [SerializeField]
    private LightsOutController lightsOutController;

    /// <summary>
    /// The player transform.
    /// </summary>
    [SerializeField]
    private Transform playerTransform;

    /// <summary>
    /// List of enemy types.
    /// </summary>
    [SerializeField]
    private List<LightsOutEnemy> lightsOutEnemies;

    /// <summary>
    /// The left spawn point.
    /// </summary>
    [SerializeField]
    private Transform leftSpawnPoint;

    /// <summary>
    /// The right spawn point.
    /// </summary>
    [SerializeField]
    private Transform rightSpawnPoint;

    /// <summary>
    /// The top spawn point.
    /// </summary>
    [SerializeField]
    private Transform topSpawnPoint;

    /// <summary>
    /// The bottom spawn point.
    /// </summary>
    [SerializeField]
    private Transform bottomSpawnPoint;

    /// <summary>
    /// The enemy speed.
    /// </summary>
    [SerializeField]
    private float enemySpeed;

    /// <summary>
    /// The base number of enemies.
    /// </summary>
    [SerializeField]
    private int baseNumberOfEnemies;

    /// <summary>
    /// The increment rate of enemies.
    /// </summary>
    [SerializeField]
    private int incrRateOfEnemies;

    #endregion

    #region UI elements

    /// <summary>
    /// The wave text container.
    /// </summary>
    [SerializeField]
    private GameObject waveTextContainer;

    /// <summary>
    /// The wave number text.
    /// </summary>
    [SerializeField]
    private Text waveNumberText;

    #endregion

    /// <summary>
    /// The wave number.
    /// </summary>
    public static int WaveNumber = 1;

    /// <summary>
    /// The time to spawn enemy.
    /// </summary>
    private float timeToSpawnEnemy = 1.5f;

    /// <summary>
    /// The time between waves.
    /// </summary>
    private float timeBetweenWaves = 6.0f;

    /// <summary>
    /// Unity Awake method.
    /// </summary>
    private void Awake()
    {
        lightsOutController.LightsOut += LightsAreOut;
    }

    /// <summary>
    /// Unity OnDestroy method.
    /// </summary>
    private void OnDestroy()
    {
        lightsOutController.LightsOut -= LightsAreOut;
    }

    /// <summary>
    /// Lights out handler
    /// </summary>
    /// <param name="sender">Sender.</param>
    /// <param name="e">E.</param>
    private void LightsAreOut(object sender, EventArgs e)
    {
        lightsOutController.LightsOut -= LightsAreOut;
        WaveNumber = 1;
        StartCoroutine(EnemyWave());
        UpdateWaveText();
    }

    /// <summary>
    /// Enemy wave coroutine.
    /// </summary>
    /// <returns>The wave.</returns>
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
            if(increaseEnemies >= 50)
            {
                baseNumberOfEnemies += incrRateOfEnemies;
            }
            WaveNumber++;
            UpdateWaveText();
        }
    }

    /// <summary>
    /// Spawns the enemy.
    /// </summary>
    /// <returns>The enemy.</returns>
    private IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(0f, timeToSpawnEnemy));
        var enemyType = PickEnemy();
        var enemyPosition = PickSpawnPoint();
        var direction = playerTransform.position - enemyPosition;

        var enemy = Instantiate(enemyType, enemyPosition, Quaternion.identity);
        enemy.Initialize(direction, enemySpeed);
    }

    /// <summary>
    /// Picks the spawn point.
    /// </summary>
    /// <returns>The spawn point.</returns>
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

    /// <summary>
    /// Picks the enemy.
    /// </summary>
    /// <returns>The enemy.</returns>
    private LightsOutEnemy PickEnemy()
    {
        return lightsOutEnemies[UnityEngine.Random.Range(0, lightsOutEnemies.Count)];
    }

    /// <summary>
    /// Updates the wave text.
    /// </summary>
    private void UpdateWaveText()
    {
        waveTextContainer.SetActive(true);
        waveNumberText.text = WaveNumber.ToString();
    }
}

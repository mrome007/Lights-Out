using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Light ball dissolve.
/// </summary>
public class LightBallDissolve : MonoBehaviour 
{
    /// <summary>
    /// The time to dissolve light ball.
    /// </summary>
    [SerializeField]
    private float timeToDissolveLightBall = 2f;

    /// <summary>
    /// Unity Start method.
    /// </summary>
    private void Start()
    {
        Destroy(gameObject, timeToDissolveLightBall);   
    }

    /// <summary>
    /// Unity OnDestroy method.
    /// </summary>
    private void OnDestroy()
    {
        YamiSpawnLightBall.IncrementLightBalls();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBallDissolve : MonoBehaviour 
{
    [SerializeField]
    private float timeToDissolveLightBall = 2f;
    
    private void Start()
    {
        Destroy(gameObject, timeToDissolveLightBall);   
    }

    private void OnDestroy()
    {
        YamiSpawnLightBall.NumberOfLightBalls++;
    }
}

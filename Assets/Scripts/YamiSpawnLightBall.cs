using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YamiSpawnLightBall : MonoBehaviour 
{
    private static int NumberOfLightBalls = 12;
    
    [SerializeField]
    private GameObject lightBallObject;

    [SerializeField]
    private float fireRate = 0.3f;

    private float fireTimer = 0f;

    private YamiPlayer yamiPlayer;

    private void Awake()
    {
        yamiPlayer = GetComponent<YamiPlayer>();
        if(yamiPlayer == null)
        {
            Debug.LogError("No Player");
        }
    }

    private void Update()
    {
        if(yamiPlayer.YamiPlayerMode == YamiPlayer.Mode.NOPLAY)
        {
            return;
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(fireTimer <= 0)
            {
                fireTimer = fireRate;

                if(NumberOfLightBalls > 0)
                {
                    NumberOfLightBalls--;
                    Instantiate(lightBallObject, transform.position, Quaternion.identity);
                }
            }
        }

        fireTimer -= Time.deltaTime;

        if(fireTimer <= 0)
        {
            fireTimer = 0f;
        }
    }

    public static void IncrementLightBalls()
    {
        NumberOfLightBalls++;
    }
}

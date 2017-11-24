using System;
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

    [SerializeField]
    private GameObject lightBallTextContainer;

    [SerializeField]
    private Text lightBallText;

    [SerializeField]
    private LightsOutController lightsOutController;

    private static Text staticLightBallText;

    private float fireTimer = 0f;

    private YamiPlayer yamiPlayer;

    private void Awake()
    {
        yamiPlayer = GetComponent<YamiPlayer>();
        if(yamiPlayer == null)
        {
            Debug.LogError("No Player");
        }
        staticLightBallText = lightBallText;
        lightsOutController.LightsOut += LightsOutHandler;
    }

    private void LightsOutHandler(object sender, EventArgs e)
    {
        lightsOutController.LightsOut -= LightsOutHandler;
        lightBallTextContainer.SetActive(true);
        UpdateNumberOfLightBallsText();
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
                    UpdateNumberOfLightBallsText();
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
        UpdateNumberOfLightBallsText();
    }

    private static void UpdateNumberOfLightBallsText()
    {
        if(staticLightBallText != null)
        {
            staticLightBallText.text = NumberOfLightBalls.ToString();
        }
    }
}

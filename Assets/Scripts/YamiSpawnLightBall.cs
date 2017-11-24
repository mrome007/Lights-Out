using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Yami spawn light ball.
/// </summary>
public class YamiSpawnLightBall : MonoBehaviour 
{
    /// <summary>
    /// The number of light balls.
    /// </summary>
    private static int NumberOfLightBalls = 12;

    /// <summary>
    /// The light ball object.
    /// </summary>
    [SerializeField]
    private GameObject lightBallObject;

    /// <summary>
    /// The fire rate.
    /// </summary>
    [SerializeField]
    private float fireRate = 0.3f;

    /// <summary>
    /// The light ball text container.
    /// </summary>
    [SerializeField]
    private GameObject lightBallTextContainer;

    /// <summary>
    /// The light ball text.
    /// </summary>
    [SerializeField]
    private Text lightBallText;

    /// <summary>
    /// The lights out controller.
    /// </summary>
    [SerializeField]
    private LightsOutController lightsOutController;

    /// <summary>
    /// The static light ball text.
    /// </summary>
    private static Text staticLightBallText;

    /// <summary>
    /// The fire timer.
    /// </summary>
    private float fireTimer = 0f;

    /// <summary>
    /// The yami player.
    /// </summary>
    private YamiPlayer yamiPlayer;

    /// <summary>
    /// Unity Awake method.
    /// </summary>
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

    /// <summary>
    /// Lightses the out handler.
    /// </summary>
    /// <param name="sender">Sender.</param>
    /// <param name="e">E.</param>
    private void LightsOutHandler(object sender, EventArgs e)
    {
        lightsOutController.LightsOut -= LightsOutHandler;
        NumberOfLightBalls = 12;
        lightBallTextContainer.SetActive(true);
        UpdateNumberOfLightBallsText();
    }

    /// <summary>
    /// Unity Update method.
    /// </summary>
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

    /// <summary>
    /// Increments the light balls.
    /// </summary>
    public static void IncrementLightBalls()
    {
        NumberOfLightBalls++;
        UpdateNumberOfLightBallsText();
    }

    /// <summary>
    /// Updates the number of light balls text.
    /// </summary>
    private static void UpdateNumberOfLightBallsText()
    {
        if(staticLightBallText != null)
        {
            staticLightBallText.text = NumberOfLightBalls.ToString();
        }
    }
}

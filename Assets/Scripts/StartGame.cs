using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Start game.
/// </summary>
public class StartGame : MonoBehaviour 
{
    /// <summary>
    /// The lights out controller.
    /// </summary>
    public LightsOutController LightsOutController;

    /// <summary>
    /// The audio source.
    /// </summary>
    public AudioSource AudioSource;

    /// <summary>
    /// Unity Update method.
    /// </summary>
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            LightsOutController.BeginLightsOut();
            AudioSource.pitch = 1;
            Destroy(gameObject);
        }
    }
}

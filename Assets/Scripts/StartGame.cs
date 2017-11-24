using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour 
{
    public LightsOutController LightsOutController;
    public AudioSource AudioSource;

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

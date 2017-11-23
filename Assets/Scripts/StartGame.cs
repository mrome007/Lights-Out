using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour 
{
    public LightsOutController LightsOutController;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            LightsOutController.BeginLightsOut();
            Destroy(gameObject);
        }
    }
}

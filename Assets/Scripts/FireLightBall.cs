using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireLightBall : MonoBehaviour 
{
    public Vector2 direction;
    private float lightBallSpeed = 7.5f;

    private void Update()
    {
        transform.Translate(direction * lightBallSpeed * Time.deltaTime);
    }
}

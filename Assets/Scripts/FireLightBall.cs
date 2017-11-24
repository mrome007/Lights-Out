using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Fire light ball.
/// </summary>
public class FireLightBall : MonoBehaviour 
{
    /// <summary>
    /// The direction.
    /// </summary>
    public Vector2 direction;

    /// <summary>
    /// The light ball speed.
    /// </summary>
    private float lightBallSpeed = 7.5f;

    /// <summary>
    /// Unity Update method.
    /// </summary>
    private void Update()
    {
        transform.Translate(direction * lightBallSpeed * Time.deltaTime);
    }
}

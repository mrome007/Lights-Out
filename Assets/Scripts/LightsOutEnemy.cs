using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Lights out enemy.
/// </summary>
public class LightsOutEnemy : MonoBehaviour 
{
    /// <summary>
    /// The sprite renderer.
    /// </summary>
    public SpriteRenderer spriteRenderer;

    /// <summary>
    /// The direction.
    /// </summary>
    private Vector2 direction;

    /// <summary>
    /// The speed.
    /// </summary>
    private float speed;

    /// <summary>
    /// Unity Start method.
    /// </summary>
    private void Start()
    {
        direction = Vector3.Normalize(direction);
        spriteRenderer.flipX = direction.x > 0;
    }

    /// <summary>
    /// Unity Update method.
    /// </summary>
    private void Update()
    {
        transform.Translate(speed * direction * Time.deltaTime);
    }

    /// <summary>
    /// Initialize the specified dir and spd.
    /// </summary>
    /// <param name="dir">Dir.</param>
    /// <param name="spd">Spd.</param>
    public void Initialize(Vector3 dir, float spd)
    {
        direction = dir;
        speed = spd;
    }
}

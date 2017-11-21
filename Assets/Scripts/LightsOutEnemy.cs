using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsOutEnemy : MonoBehaviour 
{
    public SpriteRenderer spriteRenderer;

    private Vector2 direction;
    private float speed;

    private void Start()
    {
        direction = Vector3.Normalize(direction);
        spriteRenderer.flipX = direction.x > 0;
    }

    private void Update()
    {
        transform.Translate(speed * direction * Time.deltaTime);
    }

    public void Initialize(Vector3 dir, float spd)
    {
        direction = dir;
        speed = spd;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YamiFireProjectile : MonoBehaviour 
{
    [SerializeField]
    private FireLightBall lightBallObject;

    [SerializeField]
    private float fireRate = 0.3f;

    private float fireTimer = 0f;

    private Vector2 projectileDirection;

    private void Start()
    {
        projectileDirection = Vector2.right;
    }

    private void Update()
    {
        var h = Input.GetAxisRaw("Horizontal");
        var v = Input.GetAxisRaw("Vertical");

        if(h != 0)
        {
            projectileDirection.x = h;
            projectileDirection.y = 0f;
        }
        else if(v != 0)
        {
            projectileDirection.x = 0f;
            projectileDirection.y = v;
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(fireTimer <= 0)
            {
                fireTimer = fireRate;
                var lightBall = Instantiate(lightBallObject, transform.position, Quaternion.identity);
                lightBall.direction = projectileDirection;
            }
        }

        fireTimer -= Time.deltaTime;

        if(fireTimer <= 0)
        {
            fireTimer = 0f;
        }
    }
}

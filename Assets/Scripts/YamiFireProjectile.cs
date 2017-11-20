using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YamiFireProjectile : MonoBehaviour 
{
    [SerializeField]
    private GameObject lightBallObject;

    private float fireRate = 0.25f;
    private float fireTimer = 0f;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(fireTimer <= 0)
            {
                fireTimer = fireRate;
                Instantiate(lightBallObject, transform.position, Quaternion.identity);
            }
        }

        fireTimer -= Time.deltaTime;

        if(fireTimer <= 0)
        {
            fireTimer = 0f;
        }
    }
}

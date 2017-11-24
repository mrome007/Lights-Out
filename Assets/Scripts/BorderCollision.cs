using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Border collision.
/// </summary>
public class BorderCollision : MonoBehaviour 
{
    /// <summary>
    /// Handles collision with the border.
    /// </summary>
    /// <param name="other">Other.</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag != "Player")
        {
            Destroy(other.gameObject);
        }
    }
}

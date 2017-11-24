using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Simple character movement.
/// </summary>
public class SimpleCharacterMovement : MonoBehaviour 
{
    /// <summary>
    /// The yami player.
    /// </summary>
    private YamiPlayer yamiPlayer;

    /// <summary>
    /// The sprite renderer.
    /// </summary>
    private SpriteRenderer spriteRenderer;

    /// <summary>
    /// The speed.
    /// </summary>
    private float speed = 5f;

    /// <summary>
    /// The movement vector.
    /// </summary>
    private Vector3 movementVector;

    /// <summary>
    /// Unity Awake method.
    /// </summary>
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if(spriteRenderer == null)
        {
            Debug.LogError("No Sprite Renderer");
        }

        yamiPlayer = GetComponent<YamiPlayer>();
        if(yamiPlayer == null)
        {
            Debug.LogError("No Player");
        }
    }

    /// <summary>
    /// Unity start method.
    /// </summary>
    private void Start()
    {
        movementVector = Vector3.zero;
    }
	
    /// <summary>
    /// Unity Update method.
    /// </summary>
	private void Update() 
    {
        if(yamiPlayer.YamiPlayerMode == YamiPlayer.Mode.NOPLAY)
        {
            return;
        }

        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");

        movementVector.x = h;
        movementVector.y = v;

        if(h != 0)
        {
            spriteRenderer.flipX = h < 0f;
        }

        transform.Translate(movementVector * speed * Time.deltaTime);
	}
}

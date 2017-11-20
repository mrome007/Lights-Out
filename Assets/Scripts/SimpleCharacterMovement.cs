using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCharacterMovement : MonoBehaviour 
{
    private SpriteRenderer spriteRenderer;
    private float Speed = 5f;
    private Vector3 movementVector;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if(spriteRenderer == null)
        {
            Debug.LogError("No Sprite Renderer");
        }
    }

    private void Start()
    {
        movementVector = Vector3.zero;
    }
	
	private void Update() 
    {
        var h = Input.GetAxisRaw("Horizontal");
        var v = Input.GetAxisRaw("Vertical");

        movementVector.x = h;
        movementVector.y = v;

        if(h != 0)
        {
            spriteRenderer.flipX = h < 0f;
        }

        transform.Translate(movementVector * Speed * Time.deltaTime);
	}
}

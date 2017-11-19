using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCharacterMovement : MonoBehaviour 
{
    private float Speed = 5f;
    private Vector3 movementVector;

    private void Start()
    {
        movementVector = Vector3.zero;
    }
	
	// Update is called once per frame
	void Update () 
    {
        var h = Input.GetAxisRaw("Horizontal");
        var v = Input.GetAxisRaw("Vertical");

        movementVector.x = h;
        movementVector.y = v;

        transform.Translate(movementVector * Speed * Time.deltaTime);
	}
}

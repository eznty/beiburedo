using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public Joystick joystick;
    public float speed = 2f;
    private Vector3 velocityVector = Vector3.zero; // Initial velocity of player

    public float maxVelocityChange = 4f;
    private Rigidbody rb;

    public float tiltAmount = 10f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Taking the joystick inputs
        float _xMovementInput = joystick.Horizontal;
        float _zMovementInput = joystick.Vertical;
        
        // Calculating velocity vectors
        Vector3 _movementHorizontal = transform.right * _xMovementInput;
        Vector3 _movementVertical = transform.forward * _zMovementInput;
        
        // Calculate the final movement velocity vector
        Vector3 _movementVelocityVector = (_movementHorizontal + _movementVertical).normalized * speed;
        
        // Apply movement
        Move(_movementVelocityVector);
        
        transform.rotation = Quaternion.Euler(joystick.Vertical * speed * tiltAmount, 0, -joystick.Horizontal * speed * tiltAmount);
    }

    void Move(Vector3 movementVelocityVector)
    {
        velocityVector = movementVelocityVector; 
    }

    private void FixedUpdate()
    {
        if (velocityVector != Vector3.zero)
        {
            // Get rigidbody's current velocity
            Vector3 velocity = rb.velocity; 
            Vector3 velocityChange = (velocityVector - velocity);
            
            // Applying a force by the amount of velocity change to reach the target velocity
            velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
            velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
            velocityChange.y = 0f;
                
            rb.AddForce(velocityChange, ForceMode.Acceleration);
        }
        
        
    }
}

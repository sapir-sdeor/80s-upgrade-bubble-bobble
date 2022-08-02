using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    /*
  * Mechanic that merge the user moves - left and right using arrow,
  * and jump using space
  */
    
    public float jump = 5;
    private float _moveInput;
    public Rigidbody2D rigidB;
    public float speed = 1;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigidB.velocity = Vector2.up * jump;
        }
    }

    private void FixedUpdate()
    {
        _moveInput = Input.GetAxisRaw("Horizontal");
        rigidB.velocity = new Vector2(_moveInput * speed, rigidB.velocity.y);
    }

}

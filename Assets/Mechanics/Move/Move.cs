using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    /*
     * Mechanic that moves the player left and right with the arrows
     */
    private float _moveInput;
    public Rigidbody2D rigidB;
    public float speed = 1;
    
    private void FixedUpdate()
    {
        _moveInput = Input.GetAxisRaw("Horizontal");
        rigidB.velocity = new Vector2(_moveInput * speed, rigidB.velocity.y);
    }
}

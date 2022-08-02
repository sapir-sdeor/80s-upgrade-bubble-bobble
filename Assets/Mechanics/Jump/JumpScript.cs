using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScript : MonoBehaviour
{
    /*
     * Mechanic that when pressing space the player will jump
     */
    
    public Rigidbody2D rigidB;
    public float jump;

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigidB.velocity = Vector2.up * jump;
        }
    }
}

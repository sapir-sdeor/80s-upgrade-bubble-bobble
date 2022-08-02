using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyJump : MonoBehaviour
{
    /*
     Mechanic that the enemy jump to the next platform
     need to call this mechanic when the enemy is under a platform
     */
    
    public float jumpSpeed = 1f;
    public Rigidbody2D rigidB;

    
    private void Start()
    {
        rigidB.velocity = Vector2.up * jumpSpeed;
    }


}

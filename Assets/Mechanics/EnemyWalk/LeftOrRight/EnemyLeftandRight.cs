using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLeftandRight : MonoBehaviour
{
    /*Mechanic that moves the enemy left and right without stopping,
     when the enemy get to the end of the platform he will change direction*/
    public float speed = 1f;
    public Vector3[] points;
    private int _index = 0; 
    
  
    void Update()
    {
        MoveLeftAndRight();
    }

    void MoveLeftAndRight()
    {
        transform.position = Vector2.MoveTowards(transform.position,
            points[_index], speed * Time.deltaTime);
        if (transform.position == points[_index])
        {
            if (_index == points.Length - 1)
            { _index = 0; }
            else
            { _index++; }
        }
    }

}

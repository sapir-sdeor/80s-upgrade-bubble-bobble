using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFall : MonoBehaviour
{
    /*Mechanic that moves the enemy to the end of the platform and make him fall,
    the limit floor is a location y when the lowest platform is*/
    
    public float speed;
    public Transform point;
    private float limitFloor = -3.0f;
    
    void Update()
    {
        if (transform.position.y > limitFloor)
        {
            transform.position = Vector2.MoveTowards(transform.position,
                point.position, speed * Time.deltaTime);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyS : MonoBehaviour
{   
    /*The enemy follow to the player position*/
    public float speed;
    private Transform player;
    
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }
    
    void Update()
    {
        if (transform.position != player.position)
        {
            transform.position = Vector2.MoveTowards(transform.position,
                player.position, speed*Time.deltaTime);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class Enemy : MonoBehaviour
{
    /*
     * This mechanic doesnt use, just a test
     */
    public Rigidbody2D rigidB;
    public float speed;
    private Transform _player;
    public Transform[] points;
    public int where_move = 0;
    //1-Right, 2-Left
    public int jump = 0;
    //jump - 1
    void Start()
    {
        _player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (gameObject.transform.position.x < _player.position.x || 
            gameObject.transform.position.x > _player.position.x)
        {
            where_move = 1;
        }
        if (gameObject.transform.position.y < _player.position.y)
        {
            jump = 1;
        }

    }

    private void FixedUpdate()
    {
        Vector2 pos = new Vector2(_player.position.x, transform.position.y);
        if (where_move == 1)
        {
            transform.position = Vector2.MoveTowards(transform.position,
                pos, speed * Time.deltaTime);
        }
        else if (jump == 1)
        {
            rigidB.velocity = Vector2.up * speed;
        }
        
    }

    /*transform.position = Vector2.MoveTowards(transform.position,
            new Vector2(_player.position.x,transform.position.y), speed * Time.deltaTime);
        
        transform.position = Vector3.up;*/
    
    String WhichFloor(Transform tran)
    {
        if (tran.position.y > points[4].position.y)
        {
            if (tran.position.y > points[2].position.y &&
                tran.position.y < points[1].position.y)
            {
                print("FLOOR2");
                return "Floor2";
            }

            if (tran.position.y > points[2].position.y &&
                tran.position.y > points[1].position.y)
            {
                print("FLOOR1");
                return "Floor1";
            }
            print("FLOOR3");
            return "Floor3";
        }
        return "None";
    }
}

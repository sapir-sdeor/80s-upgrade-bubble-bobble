using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Bubbles : MonoBehaviour
{
    
    public float speed = 5f;
    public Rigidbody2D rigidB;
    public float time;
    private int _chooseDirection;
    private float _firstPosition;
    private bool _up;

    /**
     * Initialized the ignore collision between the bubble and the enemy
     */
    private void Awake()
    {
        Physics2D.IgnoreLayerCollision(4,2,false);
    }

    /**
     * In the start the bubble move in the direction if the player
     */
    private void Start()
    {
        _chooseDirection = GameManager.direction switch
        {
            false =>  1,
            true => 2
        };
        _firstPosition = transform.position.y;
    }

    /**
     * In each frame move the bubble in the right direction,
     * Check if the bubble need to go up, or disappear
     */
    void Update()
    {
        if (transform.position.y > _firstPosition)
        {
            _up = true;
            gameObject.GetComponent<Collider2D>().enabled = false;
        }
        time += Time.deltaTime;
        if (time < 2)
        {
            rigidB.velocity = _chooseDirection switch
            {
                1 => Vector2.left * speed,
                2 => Vector2.right * speed,
                _ => rigidB.velocity
            }; 
        }
        else
        {
            rigidB.bodyType = RigidbodyType2D.Static;
            GameManager.MoveUp(gameObject);
            if (time > 4)
            { _up = true; }
            if (time > 6)
            { GameManager.MoveTo(gameObject); }
            if (time > 15)
            { gameObject.SetActive(false); }
        }

    }
    
    /**
     * If the bubble hit the enemy the bubble will disappear
     */
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") && !_up)
        {
            gameObject.SetActive(false);
        }
    }
}

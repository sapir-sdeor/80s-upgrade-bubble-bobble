using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class PlayerScript : MonoBehaviour
{
    private Vector2 _direction;
    public float jumpSpeed;
    private float _moveInput;
    public Rigidbody2D rigidB;
    public SpriteRenderer spriteR;
    public float speed = 1;
    public float speedStart = 0.2f;
    public Transform shotPoint;
    public GameObject bubblePrefab;
    private float _time;
    private float _timefordead;
    private static bool _isGround = true;
    private bool _start = true;
    private bool _dead;
    private Vector2 _startPosition;
    public Animator playerAnimator;

    /**
     * Initialized the start position of the player
     */
    private void Start()
    {
        _startPosition = new Vector2(-8, -4.3f);
        _start = true;
        _isGround = true;
    }

    
    /**
     * If it's the start of the level, the player move to the start position,
     * otherwise check the input player for shooting and jumping.
     * Also check if lives over
     */
    private void Update()
    {
        if (_start)
        {
            transform.position = Vector2.MoveTowards(transform.position,
                _startPosition, speedStart * Time.deltaTime);
            if (transform.position.x == _startPosition.x && transform.position.x == _startPosition.x)
            {
                _start = false;
                rigidB.gravityScale = 1;
                GetComponent<Collider2D>().isTrigger = false;
            }
        }

        if (!_start)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                playerAnimator.SetTrigger("shot");
                Shoot();
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow) && _isGround)
            { Jump(); }

        }

        if (GameManager.loseLive)
        {
            GameManager.loseLive = false;
            playerAnimator.SetTrigger("dead");
        }
    }

    /**
     * Move the player throw the user input
     */
    private void FixedUpdate()
    {
        if (_start) return;
        switch (GameManager.whereMove)
        {
            case 1:
                rigidB.AddForce(Vector2.left * speed);
                spriteR.flipX = false;
                playerAnimator.SetTrigger("move");
                break;
            case 2:
                rigidB.AddForce(Vector2.right * speed);
                spriteR.flipX = true;
                playerAnimator.SetTrigger("move");
                break;
        }
        GameManager.whereMove = 0;
    }

    /**
     * The player jump
     */
    private void Jump()
    {
        rigidB.velocity = Vector2.up * jumpSpeed;
        _isGround = false;
    }
    
    /*
     * The player shot
     */
    private void Shoot()
    {
        Instantiate(bubblePrefab, shotPoint.position, shotPoint.rotation);
    }

    
    private void OnCollisionEnter2D(Collision2D other)
    {
        CheckGround(other);
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Floor") && !_isGround)
        { _isGround = true; }
    }
    
    /**
     * Check where the player is and if his allowed to jump
     */
    private void CheckGround(Collision2D other)
    {
        if (other.gameObject.CompareTag("Floor") && !_isGround)
        { _isGround = true; }
    }
}

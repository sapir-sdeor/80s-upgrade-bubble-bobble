using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class PlayerUpgrade : MonoBehaviour
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
    private float _timeForDead;
    private float _boostTime;
    private int _boostUp;
    private bool _boost;
    private bool eaten;
    private bool _littleBoost;
    private static bool _isGround = true;
    private bool _win;
    private bool _dead;
    private Vector2 _startPosition;
    private Vector2 _holePosition;
    public Animator playerAnimator;
    

    /**
     * Initialized the start position of the player
     */
    private void Start()
    {
        _startPosition = new Vector2(0.33f, -3.8f);
        _isGround = true;
    }


    /**
     * If it's the start of the level, the player move to the start position,
     * otherwise check the input player for shooting and jumping.
     * Also check if lives over
     */
    private void Update()
    {
        if (eaten)
        {
            _boostTime += Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position,
                _holePosition, 2 * Time.deltaTime);
            if (_boostTime > 2 && GameManager.lives != 0)
            {
                GameManager.LoseLive();
            }
        }
        if (_win)
        {
            _boostTime += Time.deltaTime;
            if (_boostTime > 2)
            {
                GameManager.upgrade = false;
                GameManager.InitializedGame();
                SceneManager.LoadScene("Win");
            }
        }
        if (GameManager.start)
        {
            transform.position = Vector2.MoveTowards(transform.position,
                _startPosition, speedStart * Time.deltaTime);
            if (transform.position.x == _startPosition.x && transform.position.x == _startPosition.x)
            {
                GameManager.start = false;
                rigidB.gravityScale = 1;
                GetComponent<Collider2D>().isTrigger = false;
            }
        }

        if (_boost || _littleBoost)
        {
            _boostUp = _boost ? 10 : 20;
            rigidB.velocity = Vector2.up * _boostUp;
            _boostTime += Time.deltaTime;
            if (_boostTime > 0.5f && _littleBoost)
            {
                GetComponent<Collider2D>().isTrigger = false;
                _littleBoost = false;
                _boostTime = 0;
            }
            if (_boostTime > 3 && _boost)
            {
                GetComponent<Collider2D>().isTrigger = false;
                _boost = false;
                _boostTime = 0;
            }
        }

        if (!GameManager.start)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                playerAnimator.SetTrigger("shot");
                Shoot();
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow) && _isGround)
            {
                Jump();
            }

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
        if (GameManager.start) return;
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

    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("BubbleUp"))
        {
            playerAnimator.SetTrigger("bubble");
            other.gameObject.SetActive(false);
            GetComponent<Collider2D>().isTrigger = true;
            _boost = true;
        }
        if (other.CompareTag("littleBubble"))
        {
            other.gameObject.SetActive(false);
            GetComponent<Collider2D>().isTrigger = true;
            _littleBoost = true;
        }

        if (other.CompareTag("hole"))
        {
            playerAnimator.SetTrigger("eaten");
            eaten = true;
            _holePosition = other.transform.position;
            rigidB.bodyType = RigidbodyType2D.Static;
        }
        
        if (other.CompareTag("live"))
        {
            GameManager.lives += 1;
        }

        if (other.CompareTag("crown"))
        {
            other.gameObject.SetActive(false);
            playerAnimator.SetTrigger("crown");
            _win = true;
        }
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        CheckGround(other);
    }

    /**
     * Check where the player is and if his allowed to jump
     */
    private void CheckGround(Collision2D other)
    {
        if ((other.gameObject.CompareTag("Floor") || other.gameObject.CompareTag("Enemy")) && !_isGround)
        {
            _isGround = true;
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            transform.parent = other.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            transform.parent = null;
        }
    }

}
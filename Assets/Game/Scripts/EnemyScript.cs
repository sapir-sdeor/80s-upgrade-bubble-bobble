using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyScript : MonoBehaviour
{
    public Vector3[] points;
    private int _i;
    private int _index;
    public int score;
    public float speed = 1f;
    public bool enemyDead;
    public Rigidbody2D rigidB;
    public Animator enemyAnim;
    public string nameAnimation;
    private Vector2 _prevVelocity;
    
    private bool _change;
    public float boost = 10;
    private bool _becomeFruit;
    private bool _hit;

    public SpriteRenderer mySpriteR;
    
    private float _time;
    private float _timeForCollision;
    private bool _ignoreCollision;

    private String _enemyWalk = "Ismoved";

    /**
     * Initialized collision and animation parameters
     */
    private void Awake()
    {
        enemyAnim.SetBool(_enemyWalk, true);
        _ignoreCollision = false;
        Physics2D.IgnoreLayerCollision(1, 2, _ignoreCollision);
    }

    
    private void Start()
    {
        _prevVelocity = rigidB.velocity;
    }

    /**
     * Each frame will check if needed to ignore collision between
     * enemy and the player, and move the enemy according the situation
     */
    void Update()
    {
        if (_ignoreCollision)
        { _timeForCollision += Time.deltaTime; }
        if (_ignoreCollision && _timeForCollision > 6)
        {
            _ignoreCollision = false;
            Physics2D.IgnoreLayerCollision(1, 2, _ignoreCollision);
            _timeForCollision = 0;
        }
        
        if (!enemyDead && !_hit)
        { MoveLeftAndRight(); }
        
        if (enemyDead && !_hit)
        {
            _time += Time.deltaTime;
            if (_time < 6 && _time > 1)
            { GameManager.MoveUp(gameObject); }
            if (_time > 6)
            { GameManager.MoveTo(gameObject); }
            if (_time > 25)
            {
                enemyAnim.SetBool("enemyUp", false);
                _enemyWalk = "comeBack";
                speed += 2;
                enemyDead = false;
                _time = 0;
            }
        }
    }
    

    /**
     * Move the enemy left and right throw the points 
     */
    void MoveLeftAndRight()
    {
        enemyAnim.SetBool(_enemyWalk, true);
        transform.position = Vector2.MoveTowards(transform.position,
            points[_index], speed * Time.deltaTime);
       
        if (transform.position == points[_index])
        {
            gameObject.GetComponent<Collider2D>().isTrigger = false;
            rigidB.gravityScale = 1;
            mySpriteR.flipX = mySpriteR.flipX == false;
            if (_index == points.Length - 1)
            { _index = 0; }
            else
            { _index++; }
        }
    }
    
    /**
     * Check 3 situation -
     * If the player shoot the enemy,
     * If the player hit the enemy after the enemy dead
     * If the player collect the enemy after became fruit
     * and set up parameters according what happened
     */
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bubble") && !enemyDead)
        {
            enemyAnim.SetBool(_enemyWalk, false);
            enemyAnim.SetBool("enemyUp", true);
            enemyDead = true;
            other.isTrigger = true;
            rigidB.gravityScale = 0;
            gameObject.GetComponent<Collider2D>().isTrigger = true;
        }
        
        if (other.CompareTag("player") && enemyDead && !_hit)
        {
            _hit = true;
            enemyAnim.SetBool("enemyUp", false);
            enemyAnim.SetBool("enemyFall",true);
            gameObject.GetComponent<Collider2D>().isTrigger = false;
            rigidB.bodyType = RigidbodyType2D.Dynamic;
            rigidB.velocity = Vector2.MoveTowards(new Vector2(0, 0),
                new Vector2(1, 1), 3) * (boost + 3);
            _prevVelocity = rigidB.velocity;
        }
        
        if (other.gameObject.CompareTag("player") && _becomeFruit)
        {
            GameManager.totalScore += score;
            mySpriteR.flipX = false;
            enemyAnim.SetTrigger("collect");
            enemyAnim.SetInteger("score", score);
            Destroy(gameObject, 1);
        }
    }

    /**
     * Check 3 situations -
     * If the player lose live,
     * If the enemy is falling 
     * If the enemy is falling and not touching the floor
     * and set up parameters according what happened
     */
    private void OnCollisionEnter2D(Collision2D other)
    {
        Vector2 direction = other.GetContact(0).normal;
        if (other.gameObject.CompareTag("player") && !_ignoreCollision && !enemyDead)
        { 
            _ignoreCollision = true;
            Physics2D.IgnoreLayerCollision(1, 2, _ignoreCollision);
            GameManager.lives--;
            GameManager.loseLive = true;
        }
        
        else if (other.gameObject.CompareTag("Floor") && _hit && _i > 3 && direction.y == 1)
        {
            rigidB.gravityScale = 1;
            enemyAnim.SetBool("enemyFall",false);
            enemyAnim.SetTrigger(nameAnimation);
            rigidB.bodyType = RigidbodyType2D.Static;
            gameObject.GetComponent<Collider2D>().isTrigger = true;
            _becomeFruit = true;
        }

        else if (_hit && !_becomeFruit)
        { Reflect(other); }
    }

    /**
     * When the enemy is falling he change his velocity
     * according the colliders 
     */
    private void Reflect(Collision2D other)
    {
        _i++;
        rigidB.gravityScale = 0;
        ContactPoint2D contact = other.contacts[0];
        Vector2 contactNormal = contact.normal;
        Vector2 newVelocity = Vector2.Reflect(_prevVelocity, contactNormal);
        _prevVelocity = newVelocity;
        rigidB.velocity = _prevVelocity;
        rigidB.velocity = rigidB.velocity.normalized * boost;
    }

}

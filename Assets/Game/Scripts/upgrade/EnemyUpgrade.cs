using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class EnemyUpgrade : MonoBehaviour
{
    public Vector3[] points;
    private int _indLocation;
    public Vector3[] platforms;
    private int _i;
    private int _index;
    public float speed = 1f;
    public bool enemyDead;
    public Rigidbody2D rigidB;
    public Animator enemyAnim;
    private Vector2 _currPosition;
    private bool _hit;
    public SpriteRenderer mySpriteR;
    private float _time;
    private float _timeForCollision;
    private bool _ignoreCollision;

    private const String EnemyWalk = "Ismoved";

    /**
     * Initialized collision and animation parameters
     */
    private void Awake()
    {
        enemyAnim.SetBool(EnemyWalk, true);
        _ignoreCollision = false;
        Physics2D.IgnoreLayerCollision(1, 2, _ignoreCollision);
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
            if (_time > 1)
            { 
                MoveNextPlatform();
            }
        }
    }
    

    /**
     * Move the enemy left and right throw the points 
     */
    void MoveLeftAndRight()
    {
        enemyAnim.SetBool(EnemyWalk, true);
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


    void MoveNextPlatform()
    {
        transform.position = Vector2.MoveTowards(transform.position,
            platforms[_indLocation], 2 * Time.deltaTime);
        if (transform.position == platforms[_indLocation])
        {
            if (_indLocation == platforms.Length - 1)
            { _indLocation = 0; }
            else
            { _indLocation++; }
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
            enemyAnim.SetBool(EnemyWalk, false);
            enemyAnim.SetBool("enemyUp", true);
            Physics2D.IgnoreLayerCollision(1, 2, false);
            enemyDead = true;
            other.isTrigger = true;
            rigidB.gravityScale = 0;
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
        if (other.gameObject.CompareTag("player") && !_ignoreCollision && !enemyDead)
        { 
            _ignoreCollision = true;
            Physics2D.IgnoreLayerCollision(1, 2, _ignoreCollision);
            GameManager.lives--;
            GameManager.loseLive = true;
        }

    }
}

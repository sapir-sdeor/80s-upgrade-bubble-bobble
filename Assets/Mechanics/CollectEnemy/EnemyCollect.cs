using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyCollect : MonoBehaviour
{
    /*
     * This mechanic do the collect enemy, when the enemy loose he stay in a bubble
     * and then the player can jump on him and the enemy change into another object.
     */
    
    public Rigidbody2D rigidB;
    private Vector2 _prevVelocity;
    private int _index;
    private bool _change;
    public SpriteRenderer mySpriteR;
    public Sprite newSprite;
    public float boost;
    private bool _changeSprite;
    
    void Start()
    {
        _prevVelocity = rigidB.velocity;
        rigidB.gravityScale = 0;
    }

    void Update()
    {
        if (_index == 3)
        {
            rigidB.gravityScale = 1;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("player"))
        {
            if (_changeSprite)
            {
                gameObject.SetActive(false);
            }
            else
            {
                rigidB.velocity = Vector2.MoveTowards(new Vector2(0, 0),
                                new Vector2(1, 1), 3) * boost;
                _prevVelocity = rigidB.velocity;
               _change = true;
            }
            
        }
        else if (other.gameObject.CompareTag("Floor") || other.gameObject.CompareTag("Block"))
        {
            rigidB.bodyType = RigidbodyType2D.Static;
            mySpriteR.sprite = newSprite;
            _changeSprite = true;

        }

        else if (_change)
        {
            _index++;
            ContactPoint2D contact = other.contacts[0];
            Vector2 contactNormal = contact.normal;
            Vector2 newVelocity = Vector2.Reflect(_prevVelocity, contactNormal);
            _prevVelocity = newVelocity;
            rigidB.velocity = _prevVelocity;
            rigidB.velocity = rigidB.velocity.normalized * boost;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fruit : MonoBehaviour
{
    public Rigidbody2D rigidB;
    public float speed = 1;
    public int score;
    public Sprite scoreSprite;
    public SpriteRenderer spriteR;
    
   
    void Update()
    {
        if (rigidB.bodyType == RigidbodyType2D.Static)
        { return; }
        rigidB.velocity = Vector2.down * speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("player")) return;
        if (gameObject.CompareTag("live") || gameObject.CompareTag("clock"))
        {
            if (gameObject.CompareTag("clock"))
            {
                Timer timer = GameObject.Find("Timer").GetComponent<Timer>();
                timer.timeValue += 10;
            }
            rigidB.bodyType = RigidbodyType2D.Static;
            gameObject.SetActive(false);
        }
        else
        {
            spriteR.sprite = scoreSprite;
            GameManager.totalScore += score;
            rigidB.bodyType = RigidbodyType2D.Static;
            Destroy(gameObject, 0.5f);
        }
    }
}

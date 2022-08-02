using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    /*Mechanic the check if there is collision between the player and the enemy
     and if so the number of lives will decrease by one,
     the number of lives is a text the same as in the real game*/
    
    public TextMeshProUGUI livesText;
    public int livesNumbers = 3;

    private void Update()
    {
        if (livesNumbers == 0)
        {
            print("GameOver");
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            livesNumbers--;
            livesText.text = livesNumbers.ToString();
        }
    }
}

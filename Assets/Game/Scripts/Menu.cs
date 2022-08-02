using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    /**
     * Check if the user press ENTER, if so play the game
     * If the scene is GameOver stop playing the music of the game
     */
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "GameOver" || SceneManager.GetActiveScene().name == "Win")
        {
            GameObject[] objs = GameObject.FindGameObjectsWithTag("music");
            if (objs.Length == 1)
            {
                Destroy(objs[0]);
            }
        }
        if (Input.GetKey(KeyCode.Return))
        {
            SceneManager.LoadScene("Instructions");
        }
    }
}

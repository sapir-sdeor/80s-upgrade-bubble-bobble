using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class begin : MonoBehaviour
{
    private float _time;
    public GameObject audioObj;
    
    /**
     * In the beginning of the game the music will continue playing
     */
    void Update()
    {
        _time += Time.deltaTime;
        if (_time > 5)
        {
            SceneManager.LoadScene("Assets/Game/Scene/Game.unity");
            DontDestroyOnLoad(audioObj);
        }
       
    }
}

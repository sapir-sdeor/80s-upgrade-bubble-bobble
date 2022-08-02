using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Camera : MonoBehaviour
{
    public Transform player;

    private void LateUpdate()
    {
        if (GameManager.start) return;
        if (player.position.y > transform.position.y)
        {
            transform.position = new Vector3 (transform.position.x, player.position.y, transform.position.z);    
        }

        if (player.position.y < transform.position.y - 5f)
        {
            GameManager.lives -= 1;
            SceneManager.LoadScene("FinalLevel");
        }
    }
}

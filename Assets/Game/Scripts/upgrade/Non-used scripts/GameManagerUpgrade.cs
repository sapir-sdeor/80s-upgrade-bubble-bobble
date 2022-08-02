using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Runtime.InteropServices;
using System.Threading;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameManagerUpgrade : MonoBehaviour
{
    public static int whereMove;
    public static bool direction = true;
    private static GameManagerUpgrade _shared;
    public static int lives = GameManager.lives;
    public static bool loseLive;
    private static bool _hole;
    private static float _animTime;
    public static int totalScore = GameManager.totalScore;
    public string nextScene;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public GameObject[] fruitPrefab;
    public Transform cameraPos;
    public static bool start = true;
    public GameObject pausePanel;

    void Start()
    {
        start = true;
        _shared = this;
        StartCoroutine(FallFruit());
    }

    /**
     * Update each frame the lives, scores, user input
     * and check if the game over or the player won
     */
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (Time.timeScale == 1)
            {
                pausePanel.SetActive(true);
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
                pausePanel.SetActive(false);
            }
        }
        if (livesText)
        { livesText.text = lives.ToString(); }

        if (scoreText)
        { scoreText.text = totalScore.ToString(); }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            whereMove = 1;
            direction = false;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            whereMove = 2;
            direction = true;
        }
        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            if (nextScene == "Win")
            {
                InitializedGame();
            }
            SceneManager.LoadScene(nextScene);
        }
        
        if (lives != 0) return;
        InitializedGame();
        SceneManager.LoadScene("GameOver");
    }
    
    public static void LoseLive()
    {
        lives -= 1;
        SceneManager.LoadScene("FinalLevel");
    }
    /**
     * Initialized the Game parameter - lives and scores
     */
    private static void InitializedGame()
    {
        lives = 3;
        totalScore = 0;
        GameManager.lives = 3;
        GameManager.totalScore = 0;
    }

    private IEnumerator FallFruit()
    {
        while (true)
        {
            int randomFruit = Random.Range(0, fruitPrefab.Length);
            GameObject fruit = fruitPrefab[randomFruit];
            float randomTime = Random.Range(2f, 10f);
            float randomPosition = Random.Range(-6.5f, 8f);

            yield return new WaitForSeconds(randomTime);
            Instantiate(fruit, new Vector3(randomPosition, cameraPos.position.y + 5, transform.position.z)
                , Quaternion.identity);
        }
    }
    
}

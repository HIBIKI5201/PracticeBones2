using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReStartManager : MonoBehaviour
{
    [SerializeField] private Canvas GameOverCanvas;
    public static bool gameOver;
    void Start()
    {
        GameOverCanvas.enabled = false;
        gameOver = false;
    }

    public void GameOver()
    {
        GameOverCanvas.enabled = true;
        gameOver = true;
    }

    public void ReStartButtonClick()
    {
        Debug.Log("ReStartButtonClick");

        PlayerController.Health = 0;
        PlayerController.Score = 0;
        StarSpawner.StarCount = 0;

        Debug.Log($"health : {PlayerController.Health}\nScore : {PlayerController.Score}\nStarCount : {StarSpawner.StarCount}");

        SceneManager.LoadScene("SampleScene");
    }

    void Update()
    {
        
    }
}

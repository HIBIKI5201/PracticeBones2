using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    void Update()
    {
        
    }
}

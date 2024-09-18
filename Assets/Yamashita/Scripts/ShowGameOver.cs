using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowGameOver : MonoBehaviour
{

    public GameObject gameOverCanvas;

    private void Start()
    {
        gameOverCanvas.SetActive(false);
    }

    public void GameOverShowPanel()
    {
        Time.timeScale = 0f;
        gameOverCanvas.SetActive(true);
    }
}

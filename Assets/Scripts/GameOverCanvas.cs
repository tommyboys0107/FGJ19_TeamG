using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverCanvas : MonoBehaviour
{
    public GameObject gameOverUI;

    // Update is called once per frame
    void Update()
    {
        switch ( GameManager.gameState )
        {
            case GameManager.GameState.Playing:
                gameOverUI.SetActive( false );
                Time.timeScale = 1.0f;
                break;
            case GameManager.GameState.GameOver:
                gameOverUI.SetActive( true );
                Time.timeScale = 0f;
                break;
        }
    }
}

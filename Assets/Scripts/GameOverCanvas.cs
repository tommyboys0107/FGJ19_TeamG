using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class GameOverCanvas : MonoBehaviour
{
    public GameObject gameOverUI;

    public GameObject playImage;

    [SerializeField] private UnityEvent Onstart;

    void Start ()
    {
        Onstart.Invoke();
    }

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
                GameObject btnSelectedMain = GameObject.Find( EventSystem.current.currentSelectedGameObject.name );
                playImage.transform.position = btnSelectedMain.transform.position + new Vector3( -( btnSelectedMain.transform.position.x * 3 / 10 ), 0, 0 );
                Time.timeScale = 0f;
                break;
        }
    }
}

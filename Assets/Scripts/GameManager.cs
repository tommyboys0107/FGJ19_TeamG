using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject playImage;

    private float nextCheckTime = 2.0f;
    private float period = 2.0f;

    public bool isStart= false;

    public enum GameState
    {
        Menu,
        Playing,
        GameOver
    }
    public static GameState gameState = GameState.Menu;

    void Awake ()
    {
        if ( instance == null )
        {
            instance = this;
            DontDestroyOnLoad( this );
            name = "FirstManager";
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch( gameState )
        {
            case GameState.Menu:
                isStart = false;
                Time.timeScale = 1.0f;
                break;
            case GameState.Playing:
                if ( Time.time > nextCheckTime )
                {
                    nextCheckTime += period;  
                    GameObject [] player = GameObject.FindGameObjectsWithTag( "Player" );
                    if ( player.Length == 4 )
                    {
                        isStart = true;
                    }
                    if ( player.Length <= 1 && isStart == true)
                    {
                        gameState = GameState.GameOver;
                    }
                }
                break;
            case GameState.GameOver:
                isStart = false;
                break;
        }

        if(Input.GetKeyDown( "g" ))
        {
            gameState = GameState.GameOver;
        }
    }
}

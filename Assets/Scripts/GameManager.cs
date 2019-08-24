using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

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
                Time.timeScale = 1.0f;
                break;
            case GameState.Playing:
                Time.timeScale = 1.0f;
                break;
            case GameState.GameOver:
                Time.timeScale = 0f;
                break;
        }
    }
}

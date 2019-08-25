using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject playImage;

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
                GameObject btnSelected = GameObject.Find( EventSystem.current.currentSelectedGameObject.name );
                playImage.transform.position = btnSelected.transform.position + new Vector3( -130, 0, 0 );
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

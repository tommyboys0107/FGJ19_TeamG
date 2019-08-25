using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject playImage;

    private float nextCheckTime = 2.0f;
    private float period = 2.0f;

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
                playImage.transform.position = btnSelected.transform.position + new Vector3( -( btnSelected.transform.position.x * 5 / 10 ), 0, 0 );
                Time.timeScale = 1.0f;
                break;
            case GameState.Playing:
                if ( Time.time > nextCheckTime )
                {
                    nextCheckTime += period;
                    GameObject [] player = GameObject.FindGameObjectsWithTag( "Player" );
                    if(player.Length <= 1)
                    {
                        gameState = GameState.GameOver;
                    }
                }
                Time.timeScale = 1.0f;
                break;
            case GameState.GameOver:
                GameObject btnSelectedMain = GameObject.Find( EventSystem.current.currentSelectedGameObject.name );
                playImage.transform.position = btnSelectedMain.transform.position + new Vector3( -(btnSelectedMain.transform.position.x * 3 / 10), 0, 0 );
                Time.timeScale = 0f;
                break;
        }

        if(Input.GetKeyDown( "g" ))
        {
            gameState = GameState.GameOver;
        }
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ButtonCallback:MonoBehaviour
{
    [SerializeField] private UnityEvent Onstart;

    void Start ()
    {
        Onstart.Invoke();
    }

    //btn callback
    public void startButtonCallback ()
    {
        SceneManager.LoadScene( 1 );
        GameManager.gameState = GameManager.GameState.Playing;
    }
    public void exitButtonCallback ()
    {
        Application.Quit();
        Debug.Log( "Exit game!" );
    }
    public void RestartButtonCallback ()
    {
        SceneManager.LoadScene( 1 );
        GameManager.gameState = GameManager.GameState.Playing;
    }
    public void menuButtonCallback ()
    {
        SceneManager.LoadScene( 0 );
        GameManager.gameState = GameManager.GameState.Menu;
    }

    //temp
    public void GAMEOVERButtonCallback ()
    {
        GameManager.gameState = GameManager.GameState.GameOver;
    }
}

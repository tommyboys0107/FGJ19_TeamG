using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ButtonCallback:MonoBehaviour
{
    [SerializeField] private UnityEvent Onstart;

    public GameObject rewiredInputManager;

    void Start ()
    {
        Onstart.Invoke();
    }

    //btn callback
    public void startButtonCallback ()
    {
        GameObject.FindWithTag("SoundManager").SendMessage("PlaySound2D", 6);
        SceneManager.LoadScene( 1 );
        GameManager.gameState = GameManager.GameState.Playing;
    }
    public void exitButtonCallback ()
    {
        Application.Quit();
        Debug.Log( "Exit game!" );
        rewiredInputManager.SetActive( false );
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
}

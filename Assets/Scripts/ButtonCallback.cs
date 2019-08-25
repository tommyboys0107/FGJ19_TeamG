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
        GameObject.FindWithTag( "SE" ).GetComponent<SoundManger>().PlaySound2D( 5, 0.5f );
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
}

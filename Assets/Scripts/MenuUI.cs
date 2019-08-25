using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuUI : MonoBehaviour
{
    public GameObject playImage;

    private void Update ()
    {
        switch ( GameManager.gameState )
        {
            case GameManager.GameState.Menu:
                GameObject btnSelected = GameObject.Find( EventSystem.current.currentSelectedGameObject.name );
                playImage.transform.position = btnSelected.transform.position + new Vector3( -( btnSelected.transform.position.x * 5 / 10 ), 0, 0 );
                break;
        }
    }
}

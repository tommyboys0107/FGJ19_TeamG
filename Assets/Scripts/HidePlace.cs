using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidePlace : MonoBehaviour
{
    public static int furnitureAmount = 0;
    public int hideAmount = 0;
    public GameObject hidePlacePrefab;

    private void Awake ()
    {
        for(int i=0 ; i<furnitureAmount ; i++ )
        {
            GameObject furniture = GameObject.Find( "furniture" + i );
            if(Obstacle.canBlockLeft == true)
            {
                GameObject hidePlace = Instantiate( hidePlacePrefab, furniture.transform.position + new Vector3(  - ( furniture.transform.localScale.x / 2 ), 0, 0 ), Quaternion.identity );
                hidePlace.transform.SetParent( furniture.transform );
                hidePlace.transform.localScale = new Vector3( 1, furniture.transform.localScale.y, furniture.transform.localScale.z );
            }
            if ( Obstacle.canBlockRight== true )
            {
                GameObject hidePlace = Instantiate( hidePlacePrefab, furniture.transform.position + new Vector3( (furniture.transform.localScale.x / 2), 0, 0 ), Quaternion.identity );
                hidePlace.transform.SetParent( furniture.transform );
                hidePlace.transform.localScale = new Vector3( 1, furniture.transform.localScale.y, furniture.transform.localScale.z );
            }
            if ( Obstacle.canBlockUp == true )
            {
                GameObject hidePlace = Instantiate( hidePlacePrefab, furniture.transform.position + new Vector3( 0, -(furniture.transform.localScale.y / 2), 0 ), Quaternion.identity );
                hidePlace.transform.SetParent( furniture.transform );
                hidePlace.transform.localScale = new Vector3( furniture.transform.localScale.x, 1, furniture.transform.localScale.z);
            }
            if ( Obstacle.canBlockForward == true )
            {
                GameObject hidePlace = Instantiate( hidePlacePrefab, furniture.transform.position + new Vector3( 0, 0, -( furniture.transform.localScale.z / 2 ) ), Quaternion.identity );
                hidePlace.transform.SetParent( furniture.transform );
                hidePlace.transform.localScale = new Vector3( furniture.transform.localScale.x, furniture.transform.localScale.y, 1 );
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidePlaceSetup : MonoBehaviour
{
    public GameObject hidePlacePrefab;

    private void Awake ()
    {
        GameObject [] furniture = GameObject.FindGameObjectsWithTag( "furniture" );
        int furnitureAmount = furniture.Length;
        for ( int i = 0 ; i < furnitureAmount ; i++ )
        {
            if ( furniture [i].GetComponent<Obstacle>().canBlockLeft == true )
            {
                GameObject hidePlace = Instantiate( hidePlacePrefab, furniture [i].transform.position + new Vector3( -( furniture [i].transform.localScale.x / 2 ), 0, 0 ), Quaternion.identity );
                hidePlace.name = "hidePlace1";
                hidePlace.transform.SetParent( furniture [i].transform );
                hidePlace.transform.localScale = new Vector3( 1, furniture [i].transform.localScale.y, furniture [i].transform.localScale.z );
            }
            if ( furniture [i].GetComponent<Obstacle>().canBlockRight == true )
            {
                GameObject hidePlace = Instantiate( hidePlacePrefab, furniture [i].transform.position + new Vector3( ( furniture [i].transform.localScale.x / 2 ), 0, 0 ), Quaternion.identity );
                hidePlace.name = "hidePlace2";
                hidePlace.transform.SetParent( furniture [i].transform );
                hidePlace.transform.localScale = new Vector3( 1, furniture [i].transform.localScale.y, furniture [i].transform.localScale.z );
            }
            if ( furniture [i].GetComponent<Obstacle>().canBlockUp == true )
            {
                GameObject hidePlace = Instantiate( hidePlacePrefab, furniture [i].transform.position + new Vector3( 0, -( furniture [i].transform.localScale.y / 2 ), 0 ), Quaternion.identity );
                hidePlace.name = "hidePlace3";
                hidePlace.transform.SetParent( furniture [i].transform );
                hidePlace.transform.localScale = new Vector3( furniture [i].transform.localScale.x, 1, furniture [i].transform.localScale.z );
            }
            if ( furniture [i].GetComponent<Obstacle>().canBlockDown == true )
            {
                GameObject hidePlace = Instantiate( hidePlacePrefab, furniture [i].transform.position + new Vector3( 0, ( furniture [i].transform.localScale.y / 2 ), 0 ), Quaternion.identity );
                hidePlace.name = "hidePlace4";
                hidePlace.transform.SetParent( furniture [i].transform );
                hidePlace.transform.localScale = new Vector3( furniture [i].transform.localScale.x, 1, furniture [i].transform.localScale.z );
            }
            if ( furniture [i].GetComponent<Obstacle>().canBlockForward == true )
            {
                GameObject hidePlace = Instantiate( hidePlacePrefab, furniture [i].transform.position + new Vector3( 0, 0, -( furniture [i].transform.localScale.z / 2 ) ), Quaternion.identity );
                hidePlace.name = "hidePlace5";
                hidePlace.transform.SetParent( furniture [i].transform );
                hidePlace.transform.localScale = new Vector3( furniture [i].transform.localScale.x, furniture [i].transform.localScale.y, 1 );
            }
            if ( furniture [i].GetComponent<Obstacle>().canBlockBackward == true )
            {
                GameObject hidePlace = Instantiate( hidePlacePrefab, furniture [i].transform.position + new Vector3( 0, 0, ( furniture [i].transform.localScale.z / 2 ) ), Quaternion.identity );
                hidePlace.name = "hidePlace6";
                hidePlace.transform.SetParent( furniture [i].transform );
                hidePlace.transform.localScale = new Vector3( furniture [i].transform.localScale.x, furniture [i].transform.localScale.y, 1 );
            }
        }
    }
}

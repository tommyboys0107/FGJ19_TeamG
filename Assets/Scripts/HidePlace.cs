 using UnityEngine;

public class HidePlace : MonoBehaviour
{
    public GameObject hidePlacePrefab;

    private void Awake ()
    {
        GameObject [] furniture = GameObject.FindGameObjectsWithTag( "furniture" );
        int furnitureAmount = furniture.Length;
        for (int i=0 ; i<furnitureAmount ; i++ )
        {
            if( furniture[i].GetComponent<Obstacle>().canBlockLeft == true )
            {
                GameObject hidePlace = Instantiate( hidePlacePrefab, furniture[i].transform.position + new Vector3(  -(furniture[i].transform.localScale.x/2), 0, 0 ), Quaternion.identity );
                hidePlace.name = ("hidingBlock" + i);
                hidePlace.transform.SetParent( furniture[i].transform );
                hidePlace.transform.localScale = new Vector3( 1, furniture[i].transform.localScale.y, furniture[i].transform.localScale.z );
            }
            if ( furniture[i].GetComponent<Obstacle>().canBlockRight == true )
            {
                GameObject hidePlace = Instantiate( hidePlacePrefab, furniture[i].transform.position + new Vector3( (furniture[i].transform.localScale.x / 2), 0, 0 ), Quaternion.identity );
                hidePlace.name = ("hidingBlock" + i);
                hidePlace.transform.SetParent( furniture[i].transform );
                hidePlace.transform.localScale = new Vector3( 1, furniture[i].transform.localScale.y, furniture[i].transform.localScale.z );
            }
            if ( furniture[i].GetComponent<Obstacle>().canBlockUp == true )
            {
                GameObject hidePlace = Instantiate( hidePlacePrefab, furniture[i].transform.position + new Vector3( 0, -(furniture[i].transform.localScale.y / 2), 0 ), Quaternion.identity );
                hidePlace.name = ("hidingBlock" + i);
                hidePlace.transform.SetParent( furniture[i].transform );
                hidePlace.transform.localScale = new Vector3( furniture[i].transform.localScale.x, 1, furniture[i].transform.localScale.z);
            }
            if ( furniture [i].GetComponent<Obstacle>().canBlockDown == true )
            {
                GameObject hidePlace = Instantiate( hidePlacePrefab, furniture [i].transform.position + new Vector3( 0, (furniture [i].transform.localScale.y/2), 0 ), Quaternion.identity );
                hidePlace.name = ("hidingBlock" + i);
                hidePlace.transform.SetParent( furniture [i].transform );
                hidePlace.transform.localScale = new Vector3( furniture [i].transform.localScale.x, 1, furniture [i].transform.localScale.z );
            }
            if ( furniture[i].GetComponent<Obstacle>().canBlockForward == true )
            {
                GameObject hidePlace = Instantiate( hidePlacePrefab, furniture[i].transform.position + new Vector3( 0, 0, -(furniture[i].transform.localScale.z/2) ), Quaternion.identity );
                hidePlace.name = ("hidingBlock" + i);
                hidePlace.transform.SetParent( furniture[i].transform );
                hidePlace.transform.localScale = new Vector3( furniture [i].transform.localScale.x, furniture [i].transform.localScale.y, 1 );
            }
            if ( furniture [i].GetComponent<Obstacle>().canBlockBackward == true )
            {
                GameObject hidePlace = Instantiate( hidePlacePrefab, furniture [i].transform.position + new Vector3( 0, 0, ( furniture [i].transform.localScale.z / 2 ) ), Quaternion.identity );
                hidePlace.name = ("hidingBlock" + i);
                hidePlace.transform.SetParent( furniture [i].transform );
                hidePlace.transform.localScale = new Vector3( furniture [i].transform.localScale.x, furniture [i].transform.localScale.y, 1 );
            }
        }
    }
}

using UnityEngine;

public class Obstacle : MonoBehaviour
{ 
    public bool canBlockLeft = false;
    public bool canBlockRight = false;
    public bool canBlockUp = false;
    public bool canBlockDown = false;
    public bool canBlockForward = false;
    public bool canBlockBackward = false;
    public int hideAmount = 0;  // how many players can hide in it 
    private int occupied = 0; // how many players hide in it

    public GameObject hidePlacePrefab;

    public bool Occupy () // call it when player try to occupy a hide place
    {
        if(occupied<hideAmount)
        {
            occupied++;
            return true;
        }
        return false;
    }
    public void Release() // call it when player leave a hide place
    {
        occupied--;
    }

    private void Awake ()
    {
        GameObject [] furniture = GameObject.FindGameObjectsWithTag( "furniture" );
        int furnitureAmount = furniture.Length;
        for ( int i = 0 ; i < furnitureAmount ; i++ )
        {
            if ( furniture [i].GetComponent<Obstacle>().canBlockLeft == true )
            {
                GameObject hidePlace = Instantiate( hidePlacePrefab, furniture [i].transform.position + new Vector3( -( furniture [i].transform.localScale.x / 2 )-( hidePlacePrefab.transform.localScale.x/2), 0, 0 ), Quaternion.identity );
                hidePlace.name = "hidePlace1";
                hidePlace.GetComponent<HidePlace>().BlockLeft = true;
                hidePlace.transform.SetParent( furniture [i].transform );
                hidePlace.transform.localScale = new Vector3( hidePlacePrefab.transform.localScale.x, furniture [i].transform.localScale.y, furniture [i].transform.localScale.z );
            }
            if ( furniture [i].GetComponent<Obstacle>().canBlockRight == true )
            {
                GameObject hidePlace = Instantiate( hidePlacePrefab, furniture [i].transform.position + new Vector3( ( furniture [i].transform.localScale.x / 2 ) + ( hidePlacePrefab.transform.localScale.x / 2 ), 0, 0 ), Quaternion.identity );
                hidePlace.name = "hidePlace2";
                hidePlace.GetComponent<HidePlace>().BlockRight = true;
                hidePlace.transform.SetParent( furniture [i].transform );
                hidePlace.transform.localScale = new Vector3( hidePlacePrefab.transform.localScale.x, furniture [i].transform.localScale.y, furniture [i].transform.localScale.z );
            }
            if ( furniture [i].GetComponent<Obstacle>().canBlockUp == true )
            {
                GameObject hidePlace = Instantiate( hidePlacePrefab, furniture [i].transform.position + new Vector3( 0, -( furniture [i].transform.localScale.y / 2 ) - ( hidePlacePrefab.transform.localScale.y / 2 ), 0 ), Quaternion.identity );
                hidePlace.name = "hidePlace3";
                hidePlace.GetComponent<HidePlace>().BlockUp = true;
                hidePlace.transform.SetParent( furniture [i].transform );
                hidePlace.transform.localScale = new Vector3( furniture [i].transform.localScale.x, hidePlacePrefab.transform.localScale.y, furniture [i].transform.localScale.z );
            }
            if ( furniture [i].GetComponent<Obstacle>().canBlockDown == true )
            {
                GameObject hidePlace = Instantiate( hidePlacePrefab, furniture [i].transform.position + new Vector3( 0, ( furniture [i].transform.localScale.y / 2 ) + ( hidePlacePrefab.transform.localScale.y / 2 ), 0 ), Quaternion.identity );
                hidePlace.name = "hidePlace4";
                hidePlace.GetComponent<HidePlace>().BlockDown = true;
                hidePlace.transform.SetParent( furniture [i].transform );
                hidePlace.transform.localScale = new Vector3( furniture [i].transform.localScale.x, hidePlacePrefab.transform.localScale.y, furniture [i].transform.localScale.z );
            }
            if ( furniture [i].GetComponent<Obstacle>().canBlockForward == true )
            {
                GameObject hidePlace = Instantiate( hidePlacePrefab, furniture [i].transform.position + new Vector3( 0, 0, -( furniture [i].transform.localScale.z / 2 ) - ( hidePlacePrefab.transform.localScale.z / 2 ) ), Quaternion.identity );
                hidePlace.name = "hidePlace5";
                hidePlace.GetComponent<HidePlace>().BlockForward = true;
                hidePlace.transform.SetParent( furniture [i].transform );
                hidePlace.transform.localScale = new Vector3( furniture [i].transform.localScale.x, furniture [i].transform.localScale.y, hidePlacePrefab.transform.localScale.z );
            }
            if ( furniture [i].GetComponent<Obstacle>().canBlockBackward == true )
            {
                GameObject hidePlace = Instantiate( hidePlacePrefab, furniture [i].transform.position + new Vector3( 0, 0, ( furniture [i].transform.localScale.z / 2 ) + ( hidePlacePrefab.transform.localScale.z / 2 ) ) , Quaternion.identity );
                hidePlace.name = "hidePlace6";
                hidePlace.GetComponent<HidePlace>().BlockBackward = true;
                hidePlace.transform.SetParent( furniture [i].transform );
                hidePlace.transform.localScale = new Vector3( furniture [i].transform.localScale.x, furniture [i].transform.localScale.y, hidePlacePrefab.transform.localScale.z );
            }
        }
    }
}

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
        if(occupied < hideAmount)
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
        if ( gameObject.GetComponent<Obstacle>().canBlockLeft == true )
        {
            GameObject hidePlace = Instantiate( hidePlacePrefab, gameObject.transform.position + new Vector3( -( gameObject.transform.localScale.x / 2 )-( hidePlacePrefab.transform.localScale.x/2), 0, 0 ), Quaternion.identity );
            hidePlace.name = "hidePlace1";
            hidePlace.GetComponent<HidePlace>().BlockLeft = true;
            hidePlace.transform.SetParent(gameObject.transform );
            hidePlace.transform.localScale = new Vector3( hidePlacePrefab.transform.localScale.x, gameObject.transform.localScale.y, gameObject.transform.localScale.z );
        }
        if (gameObject.GetComponent<Obstacle>().canBlockRight == true )
        {
            GameObject hidePlace = Instantiate( hidePlacePrefab, gameObject.transform.position + new Vector3( ( gameObject.transform.localScale.x / 2 ) + ( hidePlacePrefab.transform.localScale.x / 2 ), 0, 0 ), Quaternion.identity );
            hidePlace.name = "hidePlace2";
            hidePlace.GetComponent<HidePlace>().BlockRight = true;
            hidePlace.transform.SetParent( gameObject.transform );
            hidePlace.transform.localScale = new Vector3( hidePlacePrefab.transform.localScale.x, gameObject.transform.localScale.y, gameObject.transform.localScale.z );
        }
        if (gameObject.GetComponent<Obstacle>().canBlockUp == true )
        {
            GameObject hidePlace = Instantiate( hidePlacePrefab, gameObject.transform.position + new Vector3( 0, -( gameObject.transform.localScale.y / 2 ) - ( hidePlacePrefab.transform.localScale.y / 2 ), 0 ), Quaternion.identity );
            hidePlace.name = "hidePlace3";
            hidePlace.GetComponent<HidePlace>().BlockUp = true;
            hidePlace.transform.SetParent( gameObject.transform );
            hidePlace.transform.localScale = new Vector3( gameObject.transform.localScale.x, hidePlacePrefab.transform.localScale.y, gameObject.transform.localScale.z );
        }
        if ( gameObject.GetComponent<Obstacle>().canBlockDown == true )
        {
            GameObject hidePlace = Instantiate( hidePlacePrefab, gameObject.transform.position + new Vector3( 0, ( gameObject.transform.localScale.y / 2 ) + ( hidePlacePrefab.transform.localScale.y / 2 ), 0 ), Quaternion.identity );
            hidePlace.name = "hidePlace4";
            hidePlace.GetComponent<HidePlace>().BlockDown = true;
            hidePlace.transform.SetParent( gameObject.transform );
            hidePlace.transform.localScale = new Vector3( gameObject.transform.localScale.x, hidePlacePrefab.transform.localScale.y, gameObject.transform.localScale.z );
        }
        if ( gameObject.GetComponent<Obstacle>().canBlockForward == true )
        {
            GameObject hidePlace = Instantiate( hidePlacePrefab, gameObject.transform.position + new Vector3( 0, 0, -( gameObject.transform.localScale.z / 2 ) - ( hidePlacePrefab.transform.localScale.z / 2 ) ), Quaternion.identity );
            hidePlace.name = "hidePlace5";
            hidePlace.GetComponent<HidePlace>().BlockForward = true;
            hidePlace.transform.SetParent( gameObject.transform );
            hidePlace.transform.localScale = new Vector3( gameObject.transform.localScale.x, gameObject.transform.localScale.y, hidePlacePrefab.transform.localScale.z );
        }
        if ( gameObject.GetComponent<Obstacle>().canBlockBackward == true )
        {
            GameObject hidePlace = Instantiate( hidePlacePrefab, gameObject.transform.position + new Vector3( 0, 0, ( gameObject.transform.localScale.z / 2 ) + ( hidePlacePrefab.transform.localScale.z / 2 ) ) , Quaternion.identity );
            hidePlace.name = "hidePlace6";
            hidePlace.GetComponent<HidePlace>().BlockBackward = true;
            hidePlace.transform.SetParent( gameObject.transform );
            hidePlace.transform.localScale = new Vector3( gameObject.transform.localScale.x, gameObject.transform.localScale.y, hidePlacePrefab.transform.localScale.z );
        }
    }
}

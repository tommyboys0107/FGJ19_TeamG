using UnityEngine;

public class Obstacle : MonoBehaviour
{ 
    public bool canBlockLeft = false;
    public bool canBlockRight = false;
    public bool canBlockUp = false;
    public bool canBlockDown = false;
    public bool canBlockForward = false;
    public bool canBlockBackward = false;

    public GameObject hidePlacePrefab;

    private void Awake ()
    {
        if ( gameObject.GetComponent<Obstacle>().canBlockLeft == true )
        {
            GameObject hidePlace = Instantiate( hidePlacePrefab, gameObject.transform.position + Vector3.left, Quaternion.identity );
            hidePlace.name = "hidePlace1";
            hidePlace.GetComponent<HidePlace>().BlockLeft = true;
            hidePlace.transform.SetParent(gameObject.transform );
        }
        if (gameObject.GetComponent<Obstacle>().canBlockRight == true )
        {
            GameObject hidePlace = Instantiate( hidePlacePrefab, gameObject.transform.position + Vector3.right, Quaternion.identity );
            hidePlace.name = "hidePlace2";
            hidePlace.GetComponent<HidePlace>().BlockRight = true;
            hidePlace.transform.SetParent( gameObject.transform );
        }
        if (gameObject.GetComponent<Obstacle>().canBlockUp == true )
        {
            GameObject hidePlace = Instantiate( hidePlacePrefab, gameObject.transform.position + Vector3.up, Quaternion.identity );
            hidePlace.name = "hidePlace3";
            hidePlace.GetComponent<HidePlace>().BlockUp = true;
            hidePlace.transform.SetParent( gameObject.transform );
        }
        if ( gameObject.GetComponent<Obstacle>().canBlockDown == true )
        {
            GameObject hidePlace = Instantiate( hidePlacePrefab, gameObject.transform.position + Vector3.down, Quaternion.identity);
            hidePlace.name = "hidePlace4";
            hidePlace.GetComponent<HidePlace>().BlockDown = true;
            hidePlace.transform.SetParent( gameObject.transform );
        }
        if ( gameObject.GetComponent<Obstacle>().canBlockForward == true )
        {
            GameObject hidePlace = Instantiate( hidePlacePrefab, gameObject.transform.position + Vector3.forward, Quaternion.identity );
            hidePlace.name = "hidePlace5";
            hidePlace.GetComponent<HidePlace>().BlockForward = true;
            hidePlace.transform.SetParent( gameObject.transform );
        }
        if ( gameObject.GetComponent<Obstacle>().canBlockBackward == true )
        {
            GameObject hidePlace = Instantiate( hidePlacePrefab, gameObject.transform.position + Vector3.back, Quaternion.identity );
            hidePlace.name = "hidePlace6";
            hidePlace.GetComponent<HidePlace>().BlockBackward = true;
            hidePlace.transform.SetParent( gameObject.transform );
        }
    }
}

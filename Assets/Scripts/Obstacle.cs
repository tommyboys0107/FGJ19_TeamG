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
        Obstacle obstacle = gameObject.GetComponent<Obstacle>();
        BoxCollider boxCollider = gameObject.GetComponent<BoxCollider>();

        if (obstacle.canBlockLeft == true )
        {
            GameObject hidePlace = Instantiate( hidePlacePrefab, boxCollider.bounds.center + new Vector3(boxCollider.bounds.extents.x + 0.5f, 0.0f, 0.0f), Quaternion.identity );
            hidePlace.name = "hidePlace1";
            hidePlace.GetComponent<HidePlace>().BlockLeft = true;
            hidePlace.transform.SetParent(gameObject.transform );
        }
        if (obstacle.canBlockRight == true )
        {
            GameObject hidePlace = Instantiate( hidePlacePrefab, boxCollider.bounds.center - new Vector3(boxCollider.bounds.extents.x + 0.5f, 0.0f, 0.0f), Quaternion.identity );
            hidePlace.name = "hidePlace2";
            hidePlace.GetComponent<HidePlace>().BlockRight = true;
            hidePlace.transform.SetParent( gameObject.transform );
        }
        if (obstacle.canBlockUp == true )
        {
            GameObject hidePlace = Instantiate( hidePlacePrefab, boxCollider.bounds.center + new Vector3(0.0f, boxCollider.bounds.extents.y + 0.5f, 0.0f), Quaternion.identity );
            hidePlace.name = "hidePlace3";
            hidePlace.GetComponent<HidePlace>().BlockUp = true;
            hidePlace.transform.SetParent( gameObject.transform );
        }
        if (obstacle.canBlockDown == true )
        {
            GameObject hidePlace = Instantiate( hidePlacePrefab, boxCollider.bounds.center - new Vector3(0.0f, boxCollider.bounds.extents.y + 0.5f, 0.0f), Quaternion.identity);
            hidePlace.name = "hidePlace4";
            hidePlace.GetComponent<HidePlace>().BlockDown = true;
            hidePlace.transform.SetParent( gameObject.transform );
        }
        if (obstacle.canBlockForward == true )
        {
            GameObject hidePlace = Instantiate( hidePlacePrefab, boxCollider.bounds.center + new Vector3(0.0f, 0.0f, boxCollider.bounds.extents.z + 0.5f), Quaternion.identity );
            hidePlace.name = "hidePlace5";
            hidePlace.GetComponent<HidePlace>().BlockForward = true;
            hidePlace.transform.SetParent( gameObject.transform );
        }
        if (obstacle.canBlockBackward == true )
        {
            GameObject hidePlace = Instantiate( hidePlacePrefab, boxCollider.bounds.center - new Vector3(0.0f, 0.0f, boxCollider.bounds.extents.z + 0.5f), Quaternion.identity );
            hidePlace.name = "hidePlace6";
            hidePlace.GetComponent<HidePlace>().BlockBackward = true;
            hidePlace.transform.SetParent( gameObject.transform );
        }
    }
}

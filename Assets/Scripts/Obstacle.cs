using UnityEngine;

public class Obstacle : MonoBehaviour
{ 
    public bool canBlockLeft = false;
    public bool canBlockRight = false;
    public bool canBlockUp = false;
    public bool canBlockDown = false;
    public bool canBlockForward = false;
    public bool canBlockBackward = false;
    public int hideAmount = 0; // change later
    private int occupied = 0;

    public bool Occupy ()
    {
        if(occupied<hideAmount)
        {
            occupied++;
            return true;
        }
        return false;
    }
    public void Release()
    {
        occupied--;
    }
}

 using UnityEngine;

public class HidePlace : MonoBehaviour
{
    public bool BlockLeft = false;
    public bool BlockRight = false;
    public bool BlockUp = false;
    public bool BlockDown = false;
    public bool BlockForward = false;
    public bool BlockBackward = false;
    public int hideAmount = 0;  // how many players can hide in it 

    private int occupied = 0; // how many players hide in it


    public bool Occupy() // call it when player try to occupy a hide place
    {
        if (occupied < hideAmount)
        {
            occupied++;
            return true;
        }
        return false;
    }
    public void Release() // call it when player leave a hide place
    {
        occupied = Mathf.Min(occupied - 1, 0);
    }
}

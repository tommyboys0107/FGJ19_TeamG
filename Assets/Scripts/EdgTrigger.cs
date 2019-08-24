using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgTrigger : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Girl")
        {
            other.GetComponent<GirlMove>().isCorner = true;
            print(other.GetComponent<GirlMove>().isCorner);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Girl")
        {
            other.GetComponent<GirlMove>().isCorner = false;
            print(other.GetComponent<GirlMove>().isCorner);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgTrigger : MonoBehaviour
{
    [SerializeField]
    int edgeNumber;

    System.Random crandom = new System.Random();
    int r;
    Vector3 min;
    Vector3 max;

    // Start is called before the first frame update
    void Start()
    {
        switch (edgeNumber) {
            case 1:
                min = new Vector3(0, 180, 0);
                max = new Vector3(0, 90, 0);
                break;
            case 2:
                min = new Vector3(0, 90, 0);
                max = new Vector3(0, 180, 0);
                break;
            case 3:
                min = new Vector3(0, 180, 0);
                max = new Vector3(0, -90, 0);
                break;
            case 4:
                min = new Vector3(0, 0, 0);
                max = new Vector3(0, 90, 0);
                break;
        }
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

            r = crandom.Next(0, 2);
            if (r == 0)
            {
                other.transform.rotation = Quaternion.Euler(min);
            }
            else if (r == 1)
            {
                other.transform.rotation = Quaternion.Euler(max);
            }

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

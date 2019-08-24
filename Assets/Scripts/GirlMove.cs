using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GirlMove : MonoBehaviour
{
    bool isWait;
    public bool isCorner;
    Rigidbody rb;
    [SerializeField]
    float speed = 10;

    System.Random crandom = new System.Random();
    int r;
    // Start is called before the first frame update
    void Start()
    {
        isWait = false;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isWait == false)
        {
            //random
            r = crandom.Next(0, 2);
            isWait = true;
            if (r == 0) {
                GoUp();
                print(r);
            }
            else if (r == 1)
            {
                GoDown();
                print(r);
            }

        }
    }
    
    void GoUp()
    {
        rb.velocity = Vector3.zero;
        rb.AddRelativeForce(Vector3.forward * speed);
        StartCoroutine(Wait(3f));
    }
    void GoDown()
    {
        rb.velocity = Vector3.zero;
        rb.AddRelativeForce(Vector3.forward * -speed);
        StartCoroutine(Wait(3f));
    }

    IEnumerator Wait(float n) {
        yield return new WaitForSeconds(n);
        isWait = false;
    }
}

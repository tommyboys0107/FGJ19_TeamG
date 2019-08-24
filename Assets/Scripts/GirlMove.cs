using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GirlMove : MonoBehaviour
{
    bool isWait;
    bool isSit;
    public bool isCorner;
    Rigidbody rb;
    [SerializeField]
    float speed = 10;
    Animator girlAnimation;

    System.Random crandom = new System.Random();
    int r;
    // Start is called before the first frame update
    void Start()
    {
        isWait = false;
        isSit = false;
        rb = GetComponent<Rigidbody>();
        girlAnimation = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) { GoSit(); }
        if (isWait == false && isSit == false)
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
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, 0, 0f), 1f);
        rb.AddRelativeForce(Vector3.forward * speed);
        StartCoroutine(Wait(3f));
    }
    void GoDown()
    {
        rb.velocity = Vector3.zero;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f,180,0f), 1f);
        rb.AddRelativeForce(Vector3.forward * -speed);
        StartCoroutine(Wait(3f));
    }

    IEnumerator Wait(float n) {
        yield return new WaitForSeconds(n);
        isWait = false;
    }
    void GoSit()
    {
        isSit = true;
        rb.velocity = Vector3.zero;
        girlAnimation.SetTrigger("sit"); 
    }
}

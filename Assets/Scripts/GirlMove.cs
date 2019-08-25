using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using CliffLeeCL;

public class GirlMove : MonoBehaviour
{
    public GameObject[] players;
    PlayerController [] playerController;
    HidePlace[] hidePlaces;
    List<GameObject> grabee = new List<GameObject>();
    public Transform handPos;

    bool isWait;
    bool isSit;
    public bool isCorner;
    Rigidbody rb;
    [SerializeField]
    float speed = 10;
    Animator girlAnimation;
    public GameObject roomPos;
    Shake shake;

    System.Random crandom = new System.Random();
    int r,garbr;
    // Start is called before the first frame update
    void Start()
    {
        isWait = false;
        isSit = false;
        rb = GetComponent<Rigidbody>();
        girlAnimation = GetComponent<Animator>();
        shake = roomPos.GetComponent<Shake>();
        playerController = new PlayerController[players.Length];
        hidePlaces = new HidePlace[playerController.Length];
        for (int i=0; i<players.Length; i++)
        {
            playerController[i] = players[i].GetComponent<PlayerController>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) { GoSit(); Grab(); }
        print(isSit);
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
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, 90, 0f), 1f);
        rb.AddRelativeForce(Vector3.forward * speed);
        StartCoroutine(Wait(3f));
    }
    void GoDown()
    {
        rb.velocity = Vector3.zero;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f,-90,0f), 1f);
        rb.AddRelativeForce(Vector3.forward * -speed);
        StartCoroutine(Wait(3f));
    }

    IEnumerator Wait(float n) {
        yield return new WaitForSeconds(n);
        isWait = false;
    }
    void GoSit()
    {
        Vector3 relativePos = new Vector3(roomPos.transform.position.x - transform.position.x, 0, roomPos.transform.position.z - transform.position.z);
        shake.Shaking();
        // the second argument, upwards, defaults to Vector3.up
        Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
        transform.rotation = rotation;
        isSit = true;
        rb.velocity = Vector3.zero;
        girlAnimation.SetTrigger("sit"); 
    }
    void Grab() {
        for (int i = 0; i < playerController.Length; i++)
        {
            hidePlaces[i]=playerController[i].GetHidePlace();
        }
        for (int i = 0; i < hidePlaces.Length; i++)
        {
            grabee.Add(players[i]);
        }
        garbr = crandom.Next(0, grabee.Count);
        handPos.position = grabee[garbr].transform.position;
        StartCoroutine(FlyToHand());
    }
    IEnumerator FlyToHand() {
        yield return new WaitForSeconds(5f);
        Rigidbody r = grabee[garbr].GetComponent<Rigidbody>();
        Vector3 dir = new Vector3(grabee[garbr].transform.position.x - transform.position.x, 10, grabee[garbr].transform.position.z - transform.position.z);
        r.AddForce(-dir * 10000 * Time.deltaTime);
    } 
}

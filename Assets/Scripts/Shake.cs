using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Shake : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
            transform.DOShakePosition(1.0f,0.5f,999);
    }
    public void Shaking()
    {
        transform.DOShakePosition(1.0f, 0.5f, 999);
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTrigger : MonoBehaviour
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
        Kill(other);
    }

    IEnumerator Kill(Collider other) {
        yield return new WaitForSeconds(2f);
        Destroy(other.gameObject);
    }
}

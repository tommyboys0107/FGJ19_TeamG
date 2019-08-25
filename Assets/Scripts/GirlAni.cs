using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GirlAni : MonoBehaviour
{
    public Animator boxAni;

    BioIK.BioIK ik;
    // Start is called before the first frame update
    void Start()
    {
        ik = GetComponentInChildren<BioIK.BioIK>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void openBox(){
        boxAni.SetTrigger("open");
    }
    void closeBox()
    {
        boxAni.SetTrigger("close");
    }

    void turnOnIK() {
        ik.enabled = true;
    }
    void turnOffIK()
    {
        ik.enabled = false;
    }

}

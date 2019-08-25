using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GirlAni : MonoBehaviour
{
    public GameObject roomPos;
    Shake shake;
    public Animator boxAni;
    public SoundManger sm;
    BioIK.BioIK ik;
    // Start is called before the first frame update
    void Start()
    {
        ik = GetComponentInChildren<BioIK.BioIK>();
        shake = roomPos.GetComponent<Shake>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void shakeAni() {
        shake.Shaking();
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

    void playSimpleLaugh() {
        sm.SendMessage("PlaySound2D", 7);
    }
    void playShyLaugh()
    {
        sm.SendMessage("PlaySound2D", 1);
    }
    void playBigLaugh()
    {
        sm.SendMessage("PlaySound2D", 2);
    }
    void ShakePlay() {
        sm.SendMessage("PlaySound2D", 3);
    }
    void BeatPlay()
    {
        sm.SendMessage("PlaySound2D", 4);
    }


}

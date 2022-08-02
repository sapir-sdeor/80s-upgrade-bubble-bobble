using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class hurryUp : MonoBehaviour
{
    public Animator hurryAnimator;
    public Transform cameraPos;
   
    void Update()
    {
        Vector3 pos = cameraPos.position;
        transform.position = new Vector3 (pos.x, pos.y, transform.position.z); 
        Timer timer = GameObject.Find("Timer").GetComponent<Timer>();
        if (timer.timeValue < 10)
        { hurryAnimator.SetTrigger("hurry"); }
    }
}

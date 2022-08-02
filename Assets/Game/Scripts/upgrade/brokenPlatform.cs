using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class brokenPlatform : MonoBehaviour
{
    private bool _collision;
    private float _timeForBreak;
    public Animator platformAnim;

    void Update()
    {
        if (_collision)
        {
            _timeForBreak += Time.deltaTime;
            if (_timeForBreak > 2)
            {
                gameObject.SetActive(false);
            }
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("player"))
        {
            _collision = true;
            platformAnim.SetTrigger("break");
        }
    }
}

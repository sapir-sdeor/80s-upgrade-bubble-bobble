using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    /*
     * Mechanic that create the bubbles that the player shot with
     */
    public Transform bubblePoint;
    public GameObject bubblePrefab;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shooting();
        }
    }

    private void Shooting()
    {
        Instantiate(bubblePrefab, bubblePoint.position, bubblePoint.rotation);
    }
}

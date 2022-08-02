using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBubble : MonoBehaviour
{
    /*
     * Mechanic that move up the enemy (do after the player shot the enemy)
     */
    private float _xLimit = 0;
    private float _yLimit = 2.6f;
    public float speed = 0.002f;

    void Update()
    {
        if (gameObject.transform.position.y < _yLimit - 0.5f)
        { MoveUp(); }
        else if (gameObject.transform.position.x < _xLimit)
        { MoveToPosition(); }
        else if (gameObject.transform.position.x > _xLimit)
        { MoveToPosition(); }
    }

    private void MoveUp()
    {
        var myPos = gameObject.transform.position;
        Vector2 pos = new Vector2(myPos.x, _yLimit);
        myPos = Vector3.Lerp(myPos, pos, speed);
        gameObject.transform.position = myPos;
    }
    
    private void MoveToPosition()
    {
        var myPos = gameObject.transform.position;
        Vector2 pos = new Vector2(_xLimit, _yLimit);
        myPos = Vector3.Lerp(myPos, pos, speed);
        gameObject.transform.position = myPos;
    }
    
}

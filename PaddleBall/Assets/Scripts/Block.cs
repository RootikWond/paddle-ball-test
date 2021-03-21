using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public int lifeAmount = 1;
    public event Action<Block> BlockRemoved;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer.Equals(LayerMask.NameToLayer("Ball")))
        {
            var ball = collision.gameObject.GetComponent<BallController>();
            lifeAmount -= ball.damage;
            if(lifeAmount <= 0)
            { 
                BlockRemoved?.Invoke(this);
                gameObject.SetActive(false);
               
            }
        }
    }
}

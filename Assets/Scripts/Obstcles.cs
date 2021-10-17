using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstcles : MonoBehaviour
{
    Ball ball;
    SpriteRenderer spriteRenderer;
    BoxCollider2D boxCollider2;
    public float turnSpeed;
    Transform spwanPos, spwanPos1;
    
    void Start()
    {
        ball = FindObjectOfType<Ball>();
        boxCollider2 = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        

    }

    // Update is called once per frame
    void Update()
    {
        CheckBallColor();
        transform.Rotate(Vector3.forward, turnSpeed * Time.deltaTime);
    }

    

    void CheckBallColor()
    {
        if(ball.ballColor.color == spriteRenderer.color)
        {
            
            boxCollider2.isTrigger = true;
        }else
        {
            boxCollider2.isTrigger = false;
        }


    }
}

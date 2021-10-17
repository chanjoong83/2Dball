using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goul : MonoBehaviour
{
    Ball ball;
    public SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        ball = FindObjectOfType<Ball>();
    }

    // Update is called once per frame


    private void OnTriggerEnter(Collider other)
    {
        if((other.gameObject.tag =="Ball")&& ball.ballColor.color == spriteRenderer.color)
        {
            
        }
    }
    void Update()
    {
        
    }
}

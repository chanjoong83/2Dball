using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowBallArea : MonoBehaviour
{
    // Start is called before the first frame update
    public Ball ball;
    public float areaSpeed;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Ball")
        {
           
            ball.rb.drag = areaSpeed;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag =="Ball")
        {
            ball.rb.drag = 0f;
        }
    }
}
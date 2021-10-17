using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCamera : MonoBehaviour
{
    Ball ball;

    public Transform cameraMoving;
    public Transform ohtertal;
    public SpriteRenderer spriteRenderer;
    public bool holdBall;
    public Camera camera;
    

    void Start()
    {
     //   camera = GetComponent<Camera>();
        ball = FindObjectOfType<Ball>();
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    private void Update()
    {
        if (holdBall)
        {
            ball.rb.velocity = Vector2.zero;
            ball.transform.position = Vector2.MoveTowards(ball.transform.position, transform.position, 0.1f);
            if(ball.transform.position == transform.position)
            {
                camera.gameObject.transform.position = Vector3.MoveTowards(camera.gameObject.transform.position, cameraMoving.position, 0.1f);
                if(camera.gameObject.transform.position == cameraMoving.transform.position)
                {
                    ball.transform.position = ohtertal.position;
                    holdBall = false;
                }
                //  
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.tag == "Ball") && (ball.ballColor.color == spriteRenderer.color))
        {
            holdBall = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
       // if ((collision.gameObject.tag == "Ball") && (ball.ballColor.color == spriteRenderer.color))
        {
        //    holdBall = false;
        }
    }



    IEnumerator CameraMoving()
    {
         
        yield return new WaitForSeconds(0.5f);
        //Camera camera = camera.transform.position
    }
}

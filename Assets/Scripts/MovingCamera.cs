using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCamera : MonoBehaviour
{
    Ball ball;
    public SpriteRenderer spriteRenderer;
    bool holdBall;

    void Start()
    {
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

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.tag == "Ball") && (ball.ballColor.color == spriteRenderer.color))
        {
            holdBall = true;
        }
    }

    IEnumerator CameraMoving()
    {
        yield return new WaitForSeconds(0.5f);
        //Camera camera = camera.transform.position
    }
}

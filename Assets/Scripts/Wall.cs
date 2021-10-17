using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public SpriteRenderer spriteRendererWall;
    GameManager gameManager;
    public List<Color> colors;
    public Ball ball;
    //public Color color;

    void Start()
    {
        spriteRendererWall = GetComponent<SpriteRenderer>();
        
        
        //ball = FindObjectOfType<Ball>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            Color color;
          //  spriteRendererWall.color = colors[Random.Range(0, colors.Count)];
            
            color = spriteRendererWall.color;
            //ball.spriteRendererWall.color = color;
            collision.gameObject.GetComponent<SpriteRenderer>().color = color;
            ball.ballCount--;
            ball.ballCountText.text = ball.ballCount.ToString();
            //if(ball.ballCount<=0)
            //{
            //    Destroy(ball.gameObject);
            //}

        }
    }
   


}

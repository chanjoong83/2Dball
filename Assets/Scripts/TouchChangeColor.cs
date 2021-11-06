using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchChangeColor : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] Ball ball;
    public Color[] colors;
    SpriteRenderer spriteRenderer;
    BoxCollider2D boxCollider2D;
    public bool TCC;

    int i;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        ball = FindObjectOfType<Ball>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            
            i++;
            spriteRenderer.color = colors[i];
            if (i == 3)
            {
                if(TCC)
                {
                    Destroy(this.gameObject);
                }
                i = 0;
            }
            CheckBallColor();
        }   
    }
    private void Update()
    {
        CheckBallColor();
    }

    void CheckBallColor()
    {
        if (ball.ballColor.color == spriteRenderer.color)
        {
            //Debug.Log("°°´Ù.");
            boxCollider2D.isTrigger = true;
        }
        else
        {
            boxCollider2D.isTrigger = false;
        }


    }
}



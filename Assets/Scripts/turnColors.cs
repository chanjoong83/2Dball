using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turnColors : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    public List<Color> colors;
    public bool m_RandomColor;
    Ball ball;

    BoxCollider2D boxCollider2;
    void Start()
    {
        ball = FindObjectOfType<Ball>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider2 = GetComponent<BoxCollider2D>();
        StartCoroutine(ChangeColor());
    }

    IEnumerator ChangeColor()
    {
        if(m_RandomColor)
        {
            while (true)
            {
                int randomColor = Random.Range(0, 3);
                yield return new WaitForSeconds(1);
                spriteRenderer.color = colors[randomColor];
            }
        }
        else
        {
            while (true)
            {
                for (int i = 0; i < colors.Count; i++)
                {

                    yield return new WaitForSeconds(1);
                    spriteRenderer.color = colors[i];

                }
            }
        }
        
        
       
    }

    void Update()
    {
        CheckBallColor();
       
    }

    void CheckBallColor()
    {
        if (ball.ballColor.color == spriteRenderer.color)
        {

            boxCollider2.isTrigger = true;
        }
        else
        {
            boxCollider2.isTrigger = false;
        }


    }
}

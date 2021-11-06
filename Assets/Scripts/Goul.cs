using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goul : MonoBehaviour
{
    Ball ball;
    public SpriteRenderer spriteRenderer;
    
    
    bool holdBall;
    int currentSceneNumber;


    // Start is called before the first frame update
    void Start()
    {
        ball = FindObjectOfType<Ball>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentSceneNumber = SceneManager.GetActiveScene().buildIndex;
    }

    // Update is called once per frame


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.tag == "Ball") && (ball.ballColor.color == spriteRenderer.color))
        {
            holdBall = true;
        }
    }
   

    private void Update()
    {
        if(holdBall)
        {
            ball.rb.velocity = Vector2.zero;
            //  ball.transform.position = Vector2.MoveTowards(ball.transform.position, transform.position, 0.1f);
            StartCoroutine(delayScene());
        }
    }
    public IEnumerator delayScene()
    {

        yield return new WaitForSeconds(0.4f);
        NextSences();
    }

    public void NextSences()
    {
        Debug.Log(currentSceneNumber);
        SceneManager.LoadScene(currentSceneNumber + 1);
    }

  
}

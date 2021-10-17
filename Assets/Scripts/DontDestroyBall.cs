using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyBall : MonoBehaviour
{
    float randomX, randomY;
    public float speed;

    Rigidbody2D rb;
    public GameObject preBall; 
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void MovingBall()
    {
        Physics2D.IgnoreLayerCollision(2, 2);
        rb = GetComponent<Rigidbody2D>();

        randomX = Random.Range(-1f, 1f);
        randomY = Random.Range(-1f, 1f);

        Vector2 dir = new Vector2(randomX, randomY).normalized;

        rb.AddForce(dir * speed);
    }
}

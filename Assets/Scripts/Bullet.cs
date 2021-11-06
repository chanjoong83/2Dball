using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update

    public Rigidbody2D rigidbody2D;
    public Ball ball;
    Vector2 dir;
    
    [SerializeField]int speed;
    [SerializeField] LayerMask m_layerMask;
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        SearchBall();
     //   ball = FindObjectOfType<Ball>();
    }


    void SearchBall()
    {
        Collider2D collider2D = Physics2D.OverlapCircle(transform.position, 300, m_layerMask);
        Vector2 dir = (collider2D.transform.position - transform.position).normalized;
        rigidbody2D.AddForce(dir * speed );
        

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Wall")
        {
            Destroy(this.gameObject);

        }
    
    }
}

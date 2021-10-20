using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObstacleMoveBall : MonoBehaviour
{
    float randomX, randomY;
    public float speed ;

    Rigidbody2D rb;
    
    
    public int ballCount;
    public Text ballCountText;

    //public bool boolExplosion;


    [SerializeField] GameObject m_goPrefab;
    [SerializeField] float m_force = 0;
    [SerializeField] Vector3 m_offset = Vector3.zero;

    void Start()
    {
        MovingBall();
    }

    // Update is called once per frame
    public void Explosion()
    {
        
        GameObject t_clone = Instantiate(m_goPrefab, transform.position, Quaternion.identity);
        Rigidbody2D[] t_rigids = t_clone.GetComponentsInChildren<Rigidbody2D>();
        
        
        for (int i = 0; i < t_rigids.Length; i++)
        {
            
            randomX = Random.Range(-1f, 1f);
            randomY = Random.Range(-1f, 1f);

            Vector2 dir = new Vector2(randomX, randomY).normalized;

            //  rb.AddForce(dir * speed);
          //  t_rigids[i].AddExplosionForce(m_force, transform.position + m_offset, 100f);
            t_rigids[i].AddForce(dir * speed);
           // t_rigids[i].AddForceAtPosition(dir, transform.position);
        }
        Debug.Log("4");
        gameObject.SetActive(false);

    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            ballCount--;
            ballCountText.text = ballCount.ToString();
            if(ballCount==0)
            {
                
              
                Explosion();
            }
            
            //gameObject.SetActive(false);
            
        }
    }

    void MovingBall()
    {
        rb = GetComponent<Rigidbody2D>();

        randomX = Random.Range(-1f, 1f);
        randomY = Random.Range(-1f, 1f);

        Vector2 dir = new Vector2(randomX, randomY).normalized;

        rb.AddForce(dir * speed);
    }
}

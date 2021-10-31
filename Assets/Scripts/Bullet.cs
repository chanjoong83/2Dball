using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update

    public Rigidbody2D rigidbody2D;
    public Ball ball;
    Vector2 dir;
    
    [SerializeField]
    int speed;
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        Balltarget();
     //   ball = FindObjectOfType<Ball>();
    }
    private void Update()
    {
        Balltarget();
    }



    void Balltarget()
    {

       Vector2 dir = (ball.transform.position- transform.position).normalized;
        rigidbody2D.AddForce(dir*speed*Time.deltaTime);
        Debug.Log(dir);
    }
}

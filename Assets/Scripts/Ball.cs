using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Ball : MonoBehaviour
{
    [SerializeField] 
    //[Range(500f, 2000f)] 
    float speed;

    public GameObject asd;

    public Rigidbody2D rb;
    float distance;
    //public List<Color> colors;
    public SpriteRenderer ballColor;
    Wall wall;
    public int ballCount;
    public Text ballCountText;
    public GameObject Arrow;

    Vector3 fristMousePos, MousePos ,dir;
    public bool hole, isMouse;
    Vector3 goulPos;

    bool cameraMove;

    public Transform targetPos;
    public Transform targetPos1;




    void Start()
    {
        //ballCountText = GetComponent<Text>();
      //  colors = new List<Color>();
        ballColor = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        

     //   randomX = Random.Range(-1f, 1f);
      //  randomY = Random.Range(-1f, 1f);

      //  Vector2 dir = new Vector2(randomX, randomY).normalized;

     //   rb.AddForce(dir * speed);
        wall = FindObjectOfType<Wall>();
        //RandomColor();
        ballCountText.text = ballCount.ToString(); 

    }


    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            
            fristMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 2);

            Arrow.SetActive(true);
            // dir = (transform.position - fristMousePos).normalized;


        }
        if(Input.GetMouseButton(0))
        {
            MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 2);
            dir = (fristMousePos - MousePos).normalized;
            distance = (fristMousePos-MousePos).magnitude;
            distance = Mathf.Clamp(distance, 1, 3);
            Arrow.transform.localScale = new Vector3(distance,1, 1);
            Arrow.transform.right = dir;
            
        }
       

        if(Input.GetMouseButtonUp(0))
        {
            
            Arrow.SetActive(false);
            
           
            
           speed =  VelocityPos(fristMousePos, MousePos);
            
            rb.AddForce(dir * speed*100);
        }
        if(ballCount <=0)
        {
            Destroy(this.gameObject);
            //SceneManager.LoadScene(0);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

            


        }
        //if(hole)
        //{
        //    transform.position = Vector2.MoveTowards(transform.position, goulPos, 0.1f);
        //}
 
    }

    float VelocityPos(Vector3 a, Vector3 b)
    {
        float distance;
        distance = Vector2.Distance(a, b);
      
        return distance; 
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag =="Goul")
        collision.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
    }
    //private void OnTriggerEnter2D(Collider2D collision)
    //{

      
    //    if ((collision.gameObject.tag == "Goul") && (ballColor.color == collision.gameObject.GetComponent<SpriteRenderer>().color))
    //    {
    //        hole = true;
    //        rb.velocity = Vector2.zero;
    //        goulPos =collision.gameObject.transform.position;
            
    //     //   StartCoroutine(delayScene());
            
            
    //    }else if((collision.gameObject.tag == "Goul1") && (ballColor.color == collision.gameObject.GetComponent<SpriteRenderer>().color))
    //    {
    //        hole = true;
    //        rb.velocity = Vector2.zero;
    //        goulPos = collision.gameObject.transform.position;
    //        cameraMove = true;
            
            
    //    }
    //}

    //private void FixedUpdate()
    //{
    //    if(cameraMove)
    //    {
    //        Vector3 aa = targetPos.position;
    //        asd.transform.position = Vector2.Lerp(asd.transform.position, aa, Time.deltaTime * 2f);
    //    }
    //    if ((asd.transform.position - targetPos.position).magnitude < 0.3f) ;
    //    {
    //        StartCoroutine("wait");
    //        hole = false;
    //        transform.position = targetPos.position;
            
    //    }

   
        
    //}

    IEnumerator wait()
    {
        yield return new WaitForSeconds(1.5f);
    }




    //void RandomColor()
    //{
    //    int color =  Random.Range(0, colors.Count);
    //    ballColor.color = colors[color];
    //}
}

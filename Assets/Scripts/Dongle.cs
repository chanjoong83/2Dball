using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dongle : MonoBehaviour
{
    Rigidbody2D rigidbody;

    Animator animator;
    CircleCollider2D circleCollider2D;
    SpriteRenderer spriteRenderer;
    

    public int level;
    public GameManager gameManager;
    public ParticleSystem effect;


    bool isDrag;
    bool isMerge;
    bool isAttach;
    float deadTime;
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        circleCollider2D = GetComponent<CircleCollider2D>();
    }

    private void OnEnable()
    {
        animator.SetInteger("Level", level);
    }

    // Update is called once per frame
    void Update()
    {
        if(isDrag)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //x축 최대값 적용
            float LeftBorder = -4.2f + transform.localScale.x / 2f;
            float rightBorder = 4.2f - transform.localScale.x / 2f;

            if(mousePos.x < LeftBorder)
            {
                mousePos.x = LeftBorder;
            }else if (mousePos.x > rightBorder)
            {
                mousePos.x = rightBorder;
            }

            mousePos.y = 8;
            mousePos.z = 0;
            transform.position = Vector3.Lerp(transform.position, mousePos, 0.5f);

        }
        
    }

    public void Drag()
    {
        isDrag = true;
    }
    public void Drop()
    {
        isDrag = false;
        rigidbody.simulated = true;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag =="Dongle")
        {
            Dongle other = collision.gameObject.GetComponent<Dongle>();
            if(level ==other.level && !isMerge && !other.isMerge && level <7)
            {
                //레벨업
                float meX = transform.position.x;
                float meY = transform.position.y;

                float otherX = other.transform.position.x;
                float otherY = other.transform.position.y;

                if(meY <otherY || (meY == otherY && meX >otherX))
                {
                    other.Hide(transform.position);
                    LevelUp();
                }
            }
        }
    }
    void LevelUp()
    {
        isMerge = true;
        rigidbody.velocity = Vector2.zero;
        rigidbody.angularVelocity = 0;

        StartCoroutine("LevelUpRoutine");
    }

    IEnumerator LevelUpRoutine()
    {
        yield return new WaitForSeconds(0.2f);
        animator.SetInteger("Level", level + 1);
        gameManager.PlaySfx(GameManager.Sfx.LevelUp);
        EffectPlay();
        yield return new WaitForSeconds(0.3f);
        level++;
        gameManager.maxLevel = Mathf.Max(gameManager.maxLevel, level);

        //최대 레벨 갱신
        isMerge = false;
    }

    public void Hide(Vector3 targetPos)
    {
        isMerge = true;
        rigidbody.simulated = false;
        circleCollider2D.enabled = false;
        StartCoroutine("HideRoutine", targetPos);
        if(targetPos==Vector3.up*100)
        {
            EffectPlay();
        }
    }
    IEnumerator HideRoutine(Vector3 targetPos)
    {
        int timeCount = 0;
        while(timeCount <20)
        {
            timeCount++;
            if(targetPos !=Vector3.up*100)
            {
                transform.position = Vector3.Lerp(transform.position, targetPos, 0.5f);
            }
            //게임 오버일때
            else if(targetPos ==Vector3.up*100)
            {
                transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, 0.25f);
            }
            
            yield return null;
        }
        gameManager.score += (int)Mathf.Pow(2, level);

        gameObject.SetActive(false);

        isMerge = false;
    }

    private void OnDisable()
    {
        level = 0;
        deadTime = 0;

        transform.localPosition = Vector3.zero;
        transform.localScale = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        rigidbody.simulated = false;
        rigidbody.velocity = Vector2.zero;
        rigidbody.angularVelocity = 0;
        circleCollider2D.enabled = true;
    }


    void EffectPlay()
    {
        effect.transform.position = transform.position;
        effect.transform.localScale = transform.lossyScale;
        effect.Play();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        StartCoroutine("AttachRoutine");
        if(collision.gameObject.tag == "Wall")
        {
            ChangeColor();
        }
    }



    IEnumerator AttachRoutine()
    {
        if (isAttach)
            yield break;
        isAttach = true;
        
        gameManager.PlaySfx(GameManager.Sfx.Attach);
        yield return new WaitForSeconds(0.2f);
        
        isAttach = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag =="Finish")
        {
            deadTime += Time.deltaTime;
            if (deadTime > 2)
            {
                spriteRenderer.color = new Color(0.9f, 0.2f, 0.2f);
            }
            if (deadTime > 5)
            {
                gameManager.Result();
            }
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag =="Finish")
        {
            deadTime = 0;
            spriteRenderer.color = Color.white;
        }
        
    }

    void ChangeColor()
    {
        spriteRenderer.color = gameManager.NextColor();
    }
}

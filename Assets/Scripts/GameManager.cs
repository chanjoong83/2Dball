using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("-----------[Core]")]
    public int maxLevel;
    public bool isOver;
    public int score;


    [Header("-----------[Object Pooling]")]
    public GameObject donglePrefab;
    public Transform dongleGroup;
    public GameObject effectPrefab;
    public Transform effectGroup;
    

    [Range(1,30)]
    public int poolSize;
    public List<Dongle> donglePool;
    public List<ParticleSystem> effectPool;
    public List<Color> colors;
    Dongle lastDongle;
    int poolCursor;


    [Header("---------[BGM]")]
    public AudioSource bgmPlayer;
    public AudioSource[] sfxPlayers;
    public AudioClip[] sfxClips;
    int sfxrCursor;

    [Header("---------[UI]")]
    public GameObject floor;
    public GameObject line;
    public GameObject startGroup;
    public GameObject endGroup;

    public Text scoreText;
    public Text maxScoreText;
    public Text subScoreText;
    

    public enum Sfx 
    {
        LevelUp, Next, Attach, Button, Over
    };

    private void Awake()
    {
        //플레임 설정
        Application.targetFrameRate = 60;

        //오브젝트 풀 시작
        donglePool = new List<Dongle>();
        effectPool = new List<ParticleSystem>();
     //   colors = new List<Color>();
      


        for (int i=0; i<poolSize; i++)
        {
            MakeDongle(i);
        }

        //최대 점수 설정
        if(!PlayerPrefs.HasKey("MaxScore"))
        {
            PlayerPrefs.SetInt("MaxScore", 0);
        }
        Debug.Log(PlayerPrefs.GetInt("MaxScore"));
        maxScoreText.text = PlayerPrefs.GetInt("MaxScore").ToString();
      
    }

    public Color NextColor()
    {
        int i = Random.Range(0, colors.Count);
        Debug.Log(i);
        return colors[i];
    }

    Dongle MakeDongle(int id)
    {
        GameObject instantEffect = Instantiate(effectPrefab, effectGroup);
        ParticleSystem instantEffectParticle = instantEffect.GetComponent<ParticleSystem>();
        instantEffect.name = "Effect" + id;
        effectPool.Add(instantEffectParticle);

        GameObject instantDongle = Instantiate(donglePrefab, dongleGroup);
        Dongle instantDongleLogic = instantDongle.GetComponent<Dongle>();
        instantDongle.name = "Dongle" + id;
        instantDongleLogic.gameManager = this;
        instantDongleLogic.effect = instantEffectParticle;

        donglePool.Add(instantDongleLogic);
        return instantDongleLogic;
    }

    Dongle GetDongle()
    {
        for(int i=0; i<donglePool.Count; i++)
        {
            poolCursor = (poolCursor + 1) % donglePool.Count;
            if(!donglePool[poolCursor].gameObject.activeSelf)
            {
                return donglePool[poolCursor];
            }
        }
        return MakeDongle(donglePool.Count);
    }



    public void GameStart()
    {
        startGroup.SetActive(false);
        floor.gameObject.SetActive(true);
        line.gameObject.SetActive(true);
        scoreText.gameObject.SetActive(true);
        maxScoreText.gameObject.SetActive(true);
        //효과음
        PlaySfx(Sfx.Button);

        bgmPlayer.Play();
        Invoke("NextDongle", 1.5f);
    }


    void NextDongle()
    {
        if(isOver)
        {
            return;
        }

        lastDongle = GetDongle();
        lastDongle.level = Random.Range(0, maxLevel);
        lastDongle.gameObject.SetActive(true);     
        StartCoroutine("WaitNext");

        PlaySfx(Sfx.Next);
    }

    IEnumerator WaitNext()
    {
        while(lastDongle !=null)
        {
            yield return null;
        }
        yield return new WaitForSeconds(2.5f);

        NextDongle();
    }


    public void TouchDown()
    {
        if (lastDongle == null)
        {
            return;
        }
            
        lastDongle.Drag();
    }
    public void TouchUp()
    {
        if (lastDongle == null)
        {
            return;
        }
            
        lastDongle.Drop();
        lastDongle = null;
    }
    public void Result()
    {
        
        isOver = true;
        bgmPlayer.Stop();
        StartCoroutine("ResultRoutin");
    }

    IEnumerator ResultRoutin()
    {
        //남아 있는 동글을 순차적으로 지우면서 결산
        for (int i = 0; i < donglePool.Count; i++)
        {
            if (donglePool[i].gameObject.activeSelf)
            {
                donglePool[i].Hide(Vector3.up * 100);
                yield return new WaitForSeconds(0.1f);
            }
        }
        yield return new WaitForSeconds(1f);
        //점수 적용
        subScoreText.text = "점수 : " + scoreText.text;
        //최대 점수 갱신
        int maxScore = Mathf.Max(PlayerPrefs.GetInt("MaxScore"), score);
        PlayerPrefs.SetInt("MaxScore", maxScore);
        //ui뛰우기
        endGroup.SetActive(true);

        PlaySfx(Sfx.Over);
    }

    public void Reset()
    {
        
        PlaySfx(Sfx.Button);
        StartCoroutine(ResetRoutine());
    }

    IEnumerator ResetRoutine()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(0);
    }

    public void PlaySfx(Sfx type)
    {
        sfxrCursor = (sfxrCursor + 1) % sfxPlayers.Length;

        switch(type)
        {
            case Sfx.LevelUp:
                sfxPlayers[sfxrCursor].clip = sfxClips[Random.Range(0, 3)];
                break;
            case Sfx.Next:
                sfxPlayers[sfxrCursor].clip = sfxClips[3];
                break;
            case Sfx.Attach:
                sfxPlayers[sfxrCursor].clip = sfxClips[4];
                break;
            case Sfx.Button:
                sfxPlayers[sfxrCursor].clip = sfxClips[5];
                break;
            case Sfx.Over:
                sfxPlayers[sfxrCursor].clip = sfxClips[6];
                break;
        }
        sfxPlayers[sfxrCursor].Play();
    }
    private void LateUpdate()
    {
        scoreText.text = score.ToString();
    }
}

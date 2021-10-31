using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextStage : MonoBehaviour
{
    // Start is called before the first frame update
    int currentSceneNumber;

    void Start()
    {
        currentSceneNumber = SceneManager.GetActiveScene().buildIndex;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextSences()
    {
        Debug.Log("1");
        SceneManager.LoadScene(currentSceneNumber + 1);
    }

}

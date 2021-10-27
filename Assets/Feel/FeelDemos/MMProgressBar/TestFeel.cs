using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;
using MoreMountains.Tools;

public class TestFeel : MonoBehaviour
{
    public MMProgressBar mProgressBar;
    // Start is called before the first frame update
    void Start()
    {
        //   mProgressBar = FindObjectOfType<MMProgressBar>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Debug.Log("0");
            mProgressBar.UpdateBar01(0.1f);
        }
    }
}

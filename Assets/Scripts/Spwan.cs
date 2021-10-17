using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spwan : MonoBehaviour
{
    

    public Transform spwanPos, spwanPos1;
    public GameObject prefabsBall;
    void Start()
    {
        InvokeRepeating("Spawn", 1, 0.3f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Spawn()
    {
        Instantiate(prefabsBall, spwanPos.position, Quaternion.identity);
    }
}

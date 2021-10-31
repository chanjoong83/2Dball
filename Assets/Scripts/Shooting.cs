using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject bullet;
    void Start()
    {
        InvokeRepeating("Shoot", 0, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Shoot()
    {
        Instantiate(bullet,transform.position,Quaternion.identity);
    }
}

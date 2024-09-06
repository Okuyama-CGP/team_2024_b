using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuguKieru : MonoBehaviour
{
    [SerializeField] float lifeTime = 0.2f;

    float startTime;

    void Start()
    {
        startTime = Time.time;
    }

    void Update()
    {
        if(Time.time >= startTime + lifeTime){
            Destroy(gameObject);
        }
    }
}

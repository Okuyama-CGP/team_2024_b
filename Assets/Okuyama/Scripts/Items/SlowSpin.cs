using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowSpin : MonoBehaviour
{
    [SerializeField] float spinSpeed = 100f;
    [SerializeField] float updownSpeed = 1f;
    [SerializeField] float centerY = 1f;
    [SerializeField] float offsetY = 1f;
    [SerializeField] GameObject target;

    float timeStart; //位相
    float timeElapsed { get { return Time.time + timeStart; } } //経過時間

    void Start()
    {
        timeStart = Time.time;
    }

    void Update()
    {
        //回転
        target.transform.Rotate(spinSpeed * Time.deltaTime, 0, 0);

        //sin波で上下移動
        float addY = Mathf.Sin(timeElapsed * updownSpeed) * offsetY;
        target.transform.position = new Vector3(target.transform.position.x, centerY + addY, target.transform.position.z);
    }
}

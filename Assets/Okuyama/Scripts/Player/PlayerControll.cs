using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤー操作関連。
/// 移動方式：Rigidbody.velocity直接代入。慣性あり。
/// </summary>
public class PlayerControll : MonoBehaviour
{
    [SerializeField] float speed = 5.0f;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //水平入力を取得
        Vector3 inputAxis = new Vector3(Input.GetAxis("Horizontal"),0, Input.GetAxis("Vertical"));
        if(inputAxis.magnitude > 1) inputAxis.Normalize(); //斜め移動の正規化

        rb.velocity = inputAxis * speed;
        

        //TODO:ジャンプ機能 要検討
    }
}

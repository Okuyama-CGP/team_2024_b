using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤー操作関連。
/// 移動方式：Rigidbody.velocity直接代入。慣性なし。
/// </summary>
public class PlayerMoveController : MonoBehaviour
{
    [SerializeField] float speed = 4.0f;

    Rigidbody rb;
    PlayerCore playerCore;

    // Start is called before the first frame update
    void Start()
    {
        playerCore = MainGameManager.instance.playerCore;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //水平入力
        Vector3 inputRaw = new Vector3(Input.GetAxisRaw("Horizontal"),0, Input.GetAxisRaw("Vertical"));
        if(inputRaw.magnitude > 1) inputRaw.Normalize(); //斜め移動の正則化
        rb.velocity = inputRaw * speed; //移動
        playerCore.isMoving = inputRaw.magnitude > 0;   //coreに記録

        //移動中
        if(inputRaw.magnitude > 0){

            //向き
            Quaternion targetRotation = Quaternion.Slerp(playerCore.model.transform.rotation, Quaternion.LookRotation(inputRaw), Time.deltaTime * 10.0f);
            playerCore.model.transform.rotation = targetRotation;
            
            //アニメーション制御
            playerCore.animator.SetBool("isMoving", true);
        }else{
            //アニメーション制御
            playerCore.animator.SetBool("isMoving", false);
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤー操作関連。
/// 移動方式：Rigidbody.velocity直接代入。慣性なし。
/// </summary>
public class PlayerMoveController : MonoBehaviour {
    Rigidbody rb;
    PlayerCore playerCore;

    //playerの移動速度を参照
    float speed { get { return playerCore.moveSpeed * 4f; } }

    //モデル向き固定中(攻撃中など)かどうか
    public bool isFocused { get; private set; } = false;
    //モデル向き固定時の向き
    Quaternion focusRotation;

    // ゲームオーバー時のカメラ向き固定フラグ
    bool gameoverFocus = false;


    void Start() {
        playerCore = MainGameManager.instance.playerCore;
        rb = GetComponent<Rigidbody>();
    }

    void Update() {
        switch (MainGameManager.instance.gameState) {
            case GameState.Playing:
                UpdateOnPlaying();
                break;
            case GameState.GameOver:
                UpdateOnGameOver();
                break;
        }
    }

    void UpdateOnPlaying() {
        //水平入力
        Vector3 inputRaw = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        if (inputRaw.magnitude > 1) inputRaw.Normalize(); //斜め移動の正則化
        rb.velocity = inputRaw * speed; //移動
        playerCore.isMoving = inputRaw.magnitude > 0;   //coreに記録

        //モデル向き制御
        if (isFocused) {
            //向き固定中
            Quaternion rotation = Quaternion.Slerp(playerCore.model.transform.rotation, focusRotation, Time.deltaTime * 10.0f);
            playerCore.model.transform.rotation = rotation;
        } else {
            //向き固定中でない
            if (playerCore.isMoving) { //移動中はその向きに
                Quaternion rotation = Quaternion.Slerp(playerCore.model.transform.rotation, Quaternion.LookRotation(inputRaw), Time.deltaTime * 10.0f);
                playerCore.model.transform.rotation = rotation;
            }
        }

        //アニメーション制御
        if (playerCore.isMoving) {
            playerCore.animator.SetBool("isMoving", true);
        } else {
            playerCore.animator.SetBool("isMoving", false);
        }
    }
    void UpdateOnGameOver() {
        if(gameoverFocus) {
            //ゆっくりカメラ目線
            Quaternion targetRot = Quaternion.Euler(0, -230, 0);
            playerCore.model.transform.rotation = Quaternion.Slerp(playerCore.model.transform.rotation, targetRot, Time.deltaTime * 5f);
        }
    }

    /// <summary>
    /// モデル向きを、一時的に特定の方向に固定する
    /// </summary>
    public void FocusRotation(Vector3 direction, float durationSec) {
        StartCoroutine(FocusRotationCoroutine(direction, durationSec));
    }
    IEnumerator FocusRotationCoroutine(Vector3 direction, float durationSec) {
        isFocused = true;
        focusRotation = Quaternion.LookRotation(direction);

        yield return new WaitForSeconds(durationSec);

        isFocused = false;
    }

    /// <summary>
    /// rigidbodyの影響を受けなくする
    /// </summary>
    public void StopRB() {
        rb.isKinematic = true;
    }

    /// <summary>
    /// ゲームオーバーカメラ目線を開始
    /// </summary>
    public void StartGameoverFocus() {
        gameoverFocus = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// フィールドに落ちているアイテムの基底クラス
/// Colliderを付けてtriggerにしておくこと。
/// </summary>
public abstract class BaseItem : MonoBehaviour {
    [SerializeField] protected bool isSuckable = false; //吸引されるか
    static float suckSpeed = 10.0f; //吸引速度

    [SerializeField] AudioClip pickUpSE; //拾われた時のSE

    protected PlayerCore playerCore { get { return MainGameManager.instance.playerCore; } }

    void Update() {
        //吸引される処理
        if (isSuckable) {
            Vector3 direction = playerCore.position - transform.position;
            if (direction.magnitude <= playerCore.suckDistance) {
                transform.position += direction.normalized * Time.deltaTime * suckSpeed;
            }
        }
    }

    //拾われた処理。Playerで衝突判定して呼び出される
    public void PickUp() {
        if (MainGameManager.instance.isPlaying) {
            //サウンド
            if (pickUpSE != null) {
                MainGameManager.instance.grobalSoundManager.PlayOneShot(pickUpSE);
            }
            OnPickedUp();
        }
        Destroy(gameObject);
    }

    /// <summary>
    /// アイテムごとの拾われた時の処理、効果を記述する。
    /// </summary>
    abstract protected void OnPickedUp();

}

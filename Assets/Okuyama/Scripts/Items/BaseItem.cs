using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// フィールドに落ちているアイテムの基底クラス
/// Colliderを付けてtriggerにしておくこと。
/// </summary>
public abstract class BaseItem : MonoBehaviour
{
    [SerializeField] protected bool isSuckable = false; //吸引されるか

    public GameObject Player{get; set;}
    public PlayerCore playerCore{get; set;}

    //拾われた処理。Playerで衝突判定して呼び出される
    public void PickUp(){
        OnPickedUp();
        Destroy(gameObject);    //TODO:消滅演出など
    }

    /// <summary>
    /// アイテムごとの拾われた時の処理、効果を記述する。
    /// </summary>
    abstract protected void OnPickedUp();

    //TODO:吸引される処理
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Playerの現在の状態とかを管理する。
/// Observerパターンにしたかったけど統一性を考慮して妥協
/// </summary>
public class PlayerCore : MonoBehaviour, IDamageable
{
    [SerializeField] CursolObject cursolObject;

    /// <summary>
    /// プレイヤーのモデルオブジェクト inspectorで設定
    /// </summary>
    public GameObject model;

    /// <summary>
    /// プレイヤーが移動中(移動入力がある)かどうか
    /// </summary>
    public bool isMoving;

    /// <summary>
    /// プレイヤーが攻撃した通知
    /// 攻撃開始フレームのみtrue
    /// </summary>
    public bool attackTrigger;

    /// <summary>
    /// カーソルのワールド座標
    /// </summary>
    public Vector3 cursolPosition { get { return cursolObject.cursolPosition; } }
    /// <summary>
    /// カーソルのプレイヤーからの相対位置(方向)
    /// </summary>
    public Vector3 cursolVector { get { return cursolPosition - model.transform.position; } }
    /// <summary>
    /// カーソルのプレイヤーからの距離
    /// </summary>
    public Vector3 cursolDirection { get { return cursolVector.normalized; } }

    /// <summary>
    /// 最大HP
    /// </summary>
    public float MaxHP = 100;

    /// <summary>
    /// 現在のHP
    /// </summary>
    public float HP { get; private set; }

    /// <summary>
    /// 現在の経験値
    /// </summary>
    public float EXP { get; private set; }

    /// <summary>
    /// アイテム吸引距離
    /// </summary>
    public float suckDistance { get; private set; } = 2.0f;


    void Start()
    {
        HP = MaxHP;
        EXP = 0;
    }

    void Update()
    {
        
    }

    //接触系
    void OnTriggerEnter(Collider other)
    {
        //アイテム拾った
        if(other.gameObject.TryGetComponent(out BaseItem item)){
            item.PickUp();
        }
    }

    /// <summary>
    /// ダメージを受ける処理
    /// </summary>
    public void ApplyDamage(Damage damage)
    {
        if(damage.canDamagePlayer){
            HP -= damage.damage;
            if(HP <= 0){
                Die();
            }
        }
    }

    public void Die()
    {
        //TODO:死亡時(ゲームオーバー)処理
        Debug.Log("プレイヤー死亡！！");
    }

    /// <summary>
    /// 経験値を加算する
    /// </summary>
    public void AddEXP(float expAmount)
    {
        EXP += expAmount;
        //TODO:レベルアップ処理
    }
}

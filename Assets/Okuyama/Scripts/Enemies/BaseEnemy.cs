using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// 敵の基底クラス。<br>
/// 継承先で<c>Start()</c>をオーバーライドするときは、必ず<c>base.Start()</c>を呼び出して。
/// <c>Update()</c>についても同じく。
/// </summary>
public abstract class BaseEnemy : MonoBehaviour, IDamageable
{
    [SerializeField] protected float maxHP = 5.0f;
    protected float currentHP;

    public GameObject Player{get; set;}   //召喚時にsetする
    public PlayerCore playerCore{get; set;} //


    [SerializeField] TextMeshPro hpTMP; //TODO 仮置き

    protected virtual void Start()
    {
        currentHP = maxHP;

        hpTMP.text = currentHP.ToString(); //仮置き
    }

    protected virtual void Update()
    {
        
    }

    //ダメージを受ける処理
    public void ApplyDamage(Damage damage)
    {
        //canDamageEnemyなダメージソースからのダメージのみ受ける
        if (damage.canDamageEnemy){
            currentHP -= damage.damage;

            hpTMP.text = currentHP.ToString(); //仮置き

            if (currentHP <= 0){
                Die();
            }
        }
    }

    //死ぬ
    public void Die()
    {
        Destroy(gameObject);
        //TODO:死亡演出、ドロップアイテムなど
    }

}

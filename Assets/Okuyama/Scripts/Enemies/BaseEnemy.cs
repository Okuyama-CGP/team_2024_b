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

    [SerializeField, Tooltip("BaseItemを乗せたPrefab 落とさないならnullでもOK")] 
    protected GameObject dropItem; //落とさないならnullでもいい

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
        //アイテムドロップ
        if (dropItem != null){
            GameObject item = Instantiate(dropItem, transform.position, Quaternion.identity);
            BaseItem baseItem = item.GetComponent<BaseItem>();
            baseItem.Player = Player;
            baseItem.playerCore = playerCore;
        }

        //TODO:死亡演出

        Destroy(gameObject);
    }

}

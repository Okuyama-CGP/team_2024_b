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

    /// <summary>
    /// 現在のHP
    /// </summary>
    public float currentHP{get; protected set;}

    [SerializeField, Tooltip("BaseItemを乗せたPrefab 落とさないならnullでもOK")] 
    protected GameObject dropItem; //死亡時にドロップするアイテム　経験値などのPrefab

    protected PlayerCore playerCore{get{return MainGameManager.instance.playerCore;}}


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
    public bool ApplyDamage(Damage damage)
    {
        //canDamageEnemyなダメージソースからのダメージのみ受ける
        if (damage.canDamageEnemy){
            currentHP -= damage.damageValue;

            hpTMP.text = currentHP.ToString(); //仮置き

            if (currentHP <= 0){
                Die();
            }
            return true;
        }else{
            return false;
        }
    }

    //死ぬ
    public void Die()
    {
        //アイテムドロップ
        if (dropItem != null){
            GameObject item = Instantiate(dropItem, transform.position, Quaternion.identity);
            BaseItem baseItem = item.GetComponent<BaseItem>();
        }

        //TODO:死亡演出

        Destroy(gameObject);
    }

}

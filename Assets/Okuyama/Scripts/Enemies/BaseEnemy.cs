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
    [SerializeField] protected float knockbackMultiplier = 1.0f; 

    /// <summary>
    /// 現在のHP
    /// </summary>
    public float currentHP{get; protected set;}

    [SerializeField, Tooltip("BaseItemを乗せたPrefab 落とさないならnullでもOK")] 
    protected GameObject dropItem; //死亡時にドロップするアイテム　経験値などのPrefab

    protected PlayerCore playerCore{get{return MainGameManager.instance.playerCore;}}

    protected Rigidbody rb;

    /// <summary>
    /// ノックバック中かどうか
    /// </summary>
    protected bool isKnockbacking = false;

    [SerializeField] TextMeshPro hpTMP; //FIXME 仮置き

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
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
            StartCoroutine(KnockbackCoroutine(damage.direction, damage.knockback));

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


    //ノックバックコルーチン
    protected IEnumerator KnockbackCoroutine(Vector3 direction, float amount)
    {
        amount = amount * knockbackMultiplier;

        //ノックバック開始
        isKnockbacking = true;
        Vector3 velocity = direction * amount * 2f;
        rb.velocity = velocity;

        yield return new WaitForSeconds(amount * 0.3f);

        //終了
        isKnockbacking = false;
    }

}

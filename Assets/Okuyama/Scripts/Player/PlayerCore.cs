using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

/// <summary>
/// Playerの現在の状態とかを管理する。
/// Observerパターンにしたかったけど妥協
/// </summary>
public class PlayerCore : MonoBehaviour, IDamageable {
    
    [SerializeField] AudioClip LevelUpSE;

    /// <summary>
    /// プレイヤーのモデルオブジェクト inspectorで設定
    /// </summary>
    public GameObject model;

    /// <summary>
    /// プレイヤーのGameObject
    /// </summary>
    public GameObject playerObject { get { return gameObject; } }

    /// <summary>
    /// プレイヤーのTransform.position
    /// </summary>
    public Vector3 position { get { return transform.position; } }

    /// <summary>
    /// プレイヤーの移動処理クラス
    /// </summary>
    public PlayerMoveController moveController;

    /// <summary>
    /// プレイヤーのupgradeManagerクラス
    /// </summary>
    public UpgradeManager upgradeManager;

    /// <summary>
    /// プレイヤーのAnimator
    /// </summary>
    public Animator animator;

    //-----------------------------------------------------------

    /// <summary>
    /// プレイヤーが移動中(移動入力がある)かどうか 
    /// </summary>
    public bool isMoving;

    /// <summary>
    /// 攻撃中かどうか
    /// (!isAttacking)で攻撃可能かを取得
    /// </summary>
    public bool isAttacking = false;

    /// <summary>
    /// 攻撃開始イベント
    /// </summary>
    public event Action OnAttackStart;

    /// <summary>
    /// 攻撃クールダウン終了イベント
    /// </summary>
    public event Action OnAttackEnded;

    //-----------------------------------------------------------

    /// <summary>
    /// カーソルのワールド座標
    /// </summary>
    public Vector3 cursolPosition { get { return MainGameManager.instance.cursolObject.cursolPosition; } }
    /// <summary>
    /// カーソルのプレイヤーからの相対位置(方向)
    /// </summary>
    public Vector3 cursolVector { get { return cursolPosition - transform.position; } }
    /// <summary>
    /// カーソルのプレイヤーからの距離
    /// </summary>
    public Vector3 cursolDirection { get { return cursolVector.normalized; } }

    //-----------------------------------------------------------

    /// <summary>
    /// 最大HP
    /// </summary>
    public float maxHP { get; private set; } = 100;

    /// <summary>
    /// 現在のHP
    /// </summary>
    public float hp { get; private set; }

    /// <summary>
    /// 現在のレベル
    /// </summary>
    public int level { get; private set; } = 1;

    /// <summary>
    /// 現在の経験値
    /// </summary>
    public float exp { get; private set; }

    /// <summary>
    /// 次のレベルアップに必要な経験値
    /// </summary>
    public float nextLevelEXP { get; private set; } = 10;

    /// <summary>
    /// 経験値獲得倍率
    /// </summary>
    public float expBoost { get; private set; } = 1;

    /// <summary>
    /// 攻撃力
    /// </summary>
    public float attackPower { get; private set; } = 1;

    /// <summary>
    /// 攻撃速度
    /// 現状、攻撃クールダウンを短縮。攻撃自体の速さは変わらない
    /// </summary>
    public float attackSpeed { get; private set; } = 1;

    /// <summary>
    /// 攻撃速度の逆数。クールダウンの計算などに使おう
    /// </summary>
    public float attackSpeedReciprocal { get { return 1 / attackSpeed; } }

    /// <summary>
    /// 攻撃範囲
    /// デフォルト1、倍率
    /// </summary>
    public float attackRange { get; private set; } = 1.0f;
    //TODO: 攻撃範囲の実装

    /// <summary>
    /// アイテム吸引距離
    /// </summary>
    public float suckDistance { get; private set; } = 2.0f;

    /// <summary>
    /// 防御力(ダメージカット率)
    /// 0~1の範囲。0.1なら10%のダメージカット
    /// </summary>
    public float defencePower { get; private set; } = 0;

    /// <summary>
    /// 基本移動速度
    /// </summary>
    public float baseMoveSpeed { get; private set; } = 4.0f;

    /// <summary>
    /// 移動速度ペナルティ
    /// 武器などで減速したときに使う
    /// </summary>
    public float moveSpeedPenalty { get; private set; } = 0;

    /// <summary>
    /// 実際の移動速度 移動処理などにはこれを使う
    /// どれだけペナルティが掛かっても、最低0.3は保証
    /// </summary>
    public float moveSpeed { 
        get { return Mathf.Max(0.3f,baseMoveSpeed * (1-moveSpeedPenalty)); } 
    }



    public delegate void OnHitDelegate(GameObject target, Damage damage);  //イベントの型定義
    /// <summary>
    /// プレイヤーが敵に攻撃したときのイベント<br/>
    /// 武器などで攻撃した際は、<c>OnHitEvent?.Invoke(target, damage)</c>を呼び出す
    /// </summary>
    public OnHitDelegate OnHitEvent;

    void Start() {
        upgradeManager = GetComponent<UpgradeManager>();
        hp = maxHP;
        exp = 0;
    }

    void Update() {

    }

    //接触系
    void OnTriggerEnter(Collider other) {
        //アイテム拾った
        if (other.gameObject.TryGetComponent(out BaseItem item)) {
            item.PickUp();
        }
    }

    /// <summary>
    /// ダメージを受ける処理
    /// </summary>
    public bool ApplyDamage(Damage damage) {
        if (damage.canDamagePlayer) {
            hp -= damage.damageValue * (1 - defencePower);
            if (hp <= 0) {
                Die();
            }
            return true;
        } else {
            return false;
        }
    }
    void Die() {
        //TODO:死亡時(ゲームオーバー)処理
        Debug.Log("プレイヤー死亡！！");
    }


    /// <summary>
    /// 攻撃可能か確かめ、可能なら攻撃クールダウン開始。
    /// if(TryAttack(5f,0.5f)){攻撃処理}のように使う
    /// </summary>
    public bool TryAttack(float attackDuration, float cooldownDuration, float moveSpeedPenalty) {
        if (!isAttacking) //攻撃中でないなら
        {
            StartCoroutine(AttackCooldownCoroutine(attackDuration, cooldownDuration, moveSpeedPenalty));
            return true; //攻撃成功
        }else{
            return false; //他の攻撃実行中→失敗
        }
    }

    // コルーチンでクールダウンを処理
    private IEnumerator AttackCooldownCoroutine(float attackDuration,float cooldownDuration, float moveSpeedPenalty)
    {
        //攻撃開始
        isAttacking = true;
        this.moveSpeedPenalty += moveSpeedPenalty;
        OnAttackStart?.Invoke();

        // 攻撃が終わるまで待つ
        yield return new WaitForSeconds(attackDuration);

        this.moveSpeedPenalty -= moveSpeedPenalty;

        //クールダウンが終わるまで待つ
        yield return new WaitForSeconds(cooldownDuration);

        isAttacking = false;
        OnAttackEnded?.Invoke();
    }




    /// <summary>
    /// 経験値を加算する
    /// </summary>
    public void AddEXP(float expAmount) {
        exp += expAmount * expBoost;
        if (exp >= nextLevelEXP) {
            exp -= nextLevelEXP;
            level++;
            //TODO 必要経験値のカーブ考えよう
            LevelUp();
        }
    }
    void LevelUp() {
        MainGameManager.instance.PlayOneShot(LevelUpSE);
        upgradeManager.SelectUpgrade();
    }

    /// <summary>
    /// 最大HPを増やす
    /// </summary>
    public void IncreaseMaxHP(float amount) {
        maxHP += amount;
        hp += amount;
    }

    /// <summary>
    /// 経験値獲得倍率を増やす
    /// </summary>
    public void IncreaseEXPBoost(float amount) {
        expBoost += amount;
    }

    /// <summary>
    /// 攻撃力を増やす
    /// </summary>
    public void IncreaseAttackPower(float amount) {
        attackPower += amount;
    }

    /// <summary>
    /// 攻撃速度を増やす
    /// </summary>
    public void IncreaseAttackSpeed(float amount) {
        attackSpeed += amount;
    }

    /// <summary>
    /// 攻撃範囲を増やす
    /// </summary>
    public void IncreaseAttackRange(float amount) {
        attackRange += amount;
    }

    /// <summary>
    /// アイテム吸引距離を増やす
    /// </summary>
    public void IncreaseSuckDistance(float amount) {
        suckDistance += amount;
    }

    /// <summary>
    /// 防御力を増やす 0~1の範囲にクランプ
    /// </summary>
    public void IncreaseDefencePower(float amount) {
        defencePower += amount;
        if(defencePower > 1) defencePower = 1;
    }

    /// <summary>
    /// 移動速度を増やす
    /// </summary>
    public void IncreaseMoveSpeed(float amount) {
        baseMoveSpeed += amount;
    }
}

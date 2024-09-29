using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RacketBullet : MonoBehaviour {
    [SerializeField] SphereCollider sphereCollider;
    [SerializeField] MeshRenderer meshRenderer;
    [SerializeField] AudioClip hitSE;

    //ダメージ反映率、プレイヤー攻撃力に掛ける
    const float DamageRatio = 1.0f;

    // ダメージソースのプレイヤー
    PlayerCore playerCore;
    Damage damage;

    //既に当たったもの (重複ダメージ防止)
    List<IDamageable> alreadyHit = new List<IDamageable>();

    /// <summary>
    /// 初期化
    /// </summary>
    public void Initialize(PlayerCore playerCore, Vector3 direction) {
        this.playerCore = playerCore;

        //オブジェクトサイズ、向き
        transform.localScale *= playerCore.attackRange;
        transform.rotation = Quaternion.LookRotation(direction,Vector3.up);

        //ダメージ設定
        damage = new Damage {
            canDamagePlayer = false,
            canDamageEnemy = true,
            damageValue = playerCore.attackPower * DamageRatio,
            direction = direction,
            knockback = playerCore.knockbackRatio * 1.0f
        };
    }
    void Start() {
        StartCoroutine(BulletStateCoroutine());
    }

    // 時間を掛けた状態推移、消滅
    private IEnumerator BulletStateCoroutine() {
        yield return new WaitForSeconds(0.2f);
        sphereCollider.enabled = true;
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
    }

    // 当たり判定
    private void OnTriggerEnter(Collider other) {
        if (other.TryGetComponent(out IDamageable damageable)) {
            if (alreadyHit.Contains(damageable)) return; //既に当たっているなら無視
            //ダメージを与える
            if (damageable.ApplyDamage(damage)) {
                //成功ならリストに追加
                alreadyHit.Add(damageable);
                playerCore.OnHitEvent?.Invoke(other.gameObject, damage); //イベント発火
                MainGameManager.instance.grobalSoundManager.PlayOneShot(hitSE,0.1f); //ヒット音
            }
        }
    }
}
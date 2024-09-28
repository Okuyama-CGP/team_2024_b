using UnityEngine;
using System.Collections;

public class RacketBullet : MonoBehaviour {
    [SerializeField] SphereCollider sphereCollider;
    [SerializeField] MeshRenderer meshRenderer;

    //ダメージ反映率、プレイヤー攻撃力に掛ける
    const float DamageRatio = 5.0f;

    //遅延時間いろいろ
    const float lifeTime = 0.2f;


    // ダメージソースのプレイヤー
    PlayerCore playerCore;

    Damage damage;

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
            knockback = 0.0f
        };
    }
    void Start() {
        StartCoroutine(BulletStateCoroutine());
    }

    // 時間を掛けた状態推移、消滅
    private IEnumerator BulletStateCoroutine() {
        yield return new WaitForSeconds(0.2f);
        sphereCollider.enabled = true;
        yield return new WaitForSeconds(0.4f);
        Destroy(gameObject);
    }

    // 当たり判定
    private void OnTriggerEnter(Collider other) {
        if (other.TryGetComponent(out IDamageable damageable)) {
            //ダメージを与える
            if (damageable.ApplyDamage(damage)) {
                //成功ならOnHitEventを呼び出す
                playerCore.OnHitEvent?.Invoke(other.gameObject, damage);
            }
        }
    }
}
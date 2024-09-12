using UnityEngine;

public class RacketBullet : MonoBehaviour
{
    //ダメージ反映率、プレイヤー攻撃力に掛ける
    const float DamageRatio = 5.0f;

    //持続時間
    const float lifeTime = 0.2f;

    //生成時刻
    float startTime;

    Damage damage = new Damage
    {
        canDamagePlayer = false,
        canDamageEnemy = true,
        damageValue = 0,
        direction = Vector3.zero, //TODO 攻撃の方向にノックバック
        knockback = 0.0f
    };

    private void Start() {
        startTime = Time.time;
        //プレイヤーの攻撃力を反映して、この攻撃のダメージ値を設定
        damage.damageValue = MainGameManager.instance.playerCore.AttackPower * DamageRatio;
    }

    void Update()
    {
        //一定時間後消滅の機能を統合した
        if(Time.time >= startTime + lifeTime){
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDamageable damageable))
        {
            //ダメージを与える
            if(damageable.ApplyDamage(damage)){
                //成功ならOnHitEventを呼び出す
                MainGameManager.instance.playerCore.OnHitEvent?.Invoke(damageable, damage);
            }
        }
    }
}
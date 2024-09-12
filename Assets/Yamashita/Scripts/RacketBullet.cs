using UnityEngine;

public class RacketBullet : MonoBehaviour
{
    //ダメージ反映率、プレイヤー攻撃力に掛ける
    const float DamageRatio = 5.0f;

    Damage damage = new Damage
    {
        canDamagePlayer = false,
        canDamageEnemy = true,
        damageValue = 0,
        direction = Vector3.zero, //TODO 攻撃の方向にノックバック
        knockback = 0.0f
    };

    private void Start() {
        //プレイヤーの攻撃力を反映して、この攻撃のダメージ値を設定
        damage.damageValue = MainGameManager.instance.playerCore.AttackPower * DamageRatio;
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
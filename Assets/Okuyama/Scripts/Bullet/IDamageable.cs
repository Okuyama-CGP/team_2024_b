using UnityEngine;

/// <summary>
/// ダメージを受けることができる、という性質。
/// ダメージソースなどは、struct Damageの中身を見て判断。
/// </summary>
public interface IDamageable
{
    /// <summary>
    /// ダメージを受ける処理 <br/>
    /// 成功ならtrueを返せ。
    /// </summary>
    bool ApplyDamage(Damage damage);
}

public struct Damage
{
    public bool canDamagePlayer;
    public bool canDamageEnemy;

    public float damageValue;
    public Vector3 direction;
    public float knockback;
}

using UnityEngine;

/// <summary>
/// ダメージを受けることができる、という性質。
/// ダメージソースなどは、struct Damageの中身を見て判断。
/// </summary>
interface IDamageable
{
    void ApplyDamage(Damage damage);
}

public struct Damage
{
    public bool canDamagePlayer;
    public bool canDamageEnemy;

    public float damage;
    public Vector3 direction;
    public float knockback;
}

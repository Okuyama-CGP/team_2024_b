using UnityEngine;

public class RacketBullet : MonoBehaviour
{
    public bool canDamagePlayer = false;
    public bool canDamageEnemy = true;

    public float damageAmount = 5.0f;
    public Vector3 knockbackDirection = Vector3.zero;
    public float knockbackAmount = 0.0f;

    private void OnTriggerEnter(Collider other)
    {
        IDamageable damageable = other.GetComponent<IDamageable>();
        if (damageable != null)
        {
            Damage damage = new Damage
            {
                canDamagePlayer = canDamagePlayer,
                canDamageEnemy = canDamageEnemy,
                damage = damageAmount,
                direction = knockbackDirection,
                knockback = knockbackAmount
            };

            damageable.ApplyDamage(damage);
        }
    }
}
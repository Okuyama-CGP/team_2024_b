using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprayBullet : MonoBehaviour
{
    public bool canDamagePlayer = false;
    public bool canDamageEnemy = true;
    public float damageAmount = 1.0f;
    public Vector3 knockbackDirection = Vector3.zero;
    public float knockbackAmount = 0.0f;

    public float damageInterval = 0.1f;
    private float nextDamageTime = 0f;

    private void OnTriggerStay(Collider other)
    {
        IDamageable damageable = other.GetComponent<IDamageable>();
        if (damageable != null)
        {
            if (Time.time >= nextDamageTime)
            {
                Damage damage = new Damage
                {
                    canDamagePlayer = canDamagePlayer,
                    canDamageEnemy = canDamageEnemy,
                    damageValue = damageAmount,
                    direction = knockbackDirection,
                    knockback = knockbackAmount
                };

                damageable.ApplyDamage(damage);

                nextDamageTime = Time.time + damageInterval;
            }
        }
    }
}

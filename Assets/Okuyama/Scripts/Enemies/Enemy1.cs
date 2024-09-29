using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : BaseEnemy {
    [SerializeField] float speed = 1.0f;
    [SerializeField] GameObject Model;

    protected override void Start() {
        base.Start();

    }

    protected override void Update() {
        base.Update();

        if (!isKnockbacking) {
            //プレイヤーに向かって移動
            Vector3 direction = (playerCore.position - transform.position).normalized;
            rb.velocity = direction * speed;
            //向き
            Model.transform.LookAt(playerCore.position);
        }
    }

    //接触ダメージ
    //TODO ダメージ量など設定できるようにしたい
    Damage contactDamage = new Damage {
        canDamagePlayer = true,
        canDamageEnemy = false,
        damageValue = 0.1f,
        direction = Vector3.zero,
        knockback = 0f
    };
    void OnCollisionStay(Collision collision) {
        //単一対象ならGameobject照合が負荷的にいいらしい。
        if (collision.gameObject == playerCore.gameObject) {
            playerCore.ApplyDamage(contactDamage);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : BaseEnemy {
    [SerializeField] float speed = 1.0f;
    [SerializeField] GameObject Model;
    [SerializeField] float contactDamageValue = 0.1f;

    private float initialPositionY;
    private float startTime;

    protected override void Start() {
        base.Start();

        contactDamage = new Damage {
            canDamagePlayer = true,
            canDamageEnemy = false,
            damageValue = contactDamageValue * MainGameManager.instance.enemyStatusMultiplier,
            direction = Vector3.zero,
            knockback = 0f
        };

        // オブジェクトの最初のY位置を保存
        initialPositionY = Model.transform.position.y;
        startTime = Time.time;
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

        // 時間に基づいてサイン波でY座標を計算 (-1~1 の値を取得し、それを高さ範囲に拡大)
        float offsetY = Mathf.Sin(Time.time - startTime) * 0.4f;
        // 現在の位置を取得し、Y軸のみを変更
        Vector3 currentPosition = transform.position;
        currentPosition.y = initialPositionY + offsetY;
        // 新しい位置を適用
        Model.transform.position = currentPosition;
    }

    //接触ダメージ
    Damage contactDamage;
    void OnCollisionStay(Collision collision) {
        //単一対象ならGameobject照合が負荷的にいいらしい。
        if (collision.gameObject == playerCore.gameObject) {
            playerCore.ApplyDamage(contactDamage);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponUser : MonoBehaviour {
    [SerializeField] GameObject weaponHolder;
    [SerializeField] GameObject weaponPrefab; //TODO:暫定措置

    PlayerCore playerCore;
    GameObject weaponInstance;
    BaseWeapon holdingWeapon;

    protected float lastUseTime = 0.0f;

    void Start() {
        playerCore = MainGameManager.instance.playerCore;

        weaponInstance = Instantiate(weaponPrefab, weaponHolder.transform);
        holdingWeapon = weaponInstance.GetComponent<BaseWeapon>();

        //イベントサブスクライブ
        playerCore.OnAttackEnded += OnPlayerAttackEnded;
    }


    void Update() {
        //マウスクリック
        if (Input.GetMouseButtonDown(0)) {
            Attack();
        }
    }

    //プレイヤーが攻撃処理終了したタイミング
    void OnPlayerAttackEnded() {
        //マウス長押し中
        if (Input.GetMouseButton(0)) {
            Attack();
        }
    }

    void Attack() {
        //攻撃クールダウン確認しつつ攻撃
        //TODO: 攻撃速度の影響
        if (playerCore.TryAttack(holdingWeapon.useDuration, holdingWeapon.useCoolTime, holdingWeapon.moveSpeedPenalty)) {
            holdingWeapon.UseWeapon(playerCore);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 攻撃範囲を増やすアップグレード
/// </summary>
[CreateAssetMenu(fileName = "UpgradeBulletRange", menuName = "Upgrades/UpgradeBulletRange")]
public class UpgradeBulletRange : BaseUpgrade {

    [Space]
    [SerializeField] float bulletRangeBonus = 0.5f;
    [SerializeField] float bulletRangeBonusPerStack = 0.25f;

    public override void OnAdded(PlayerCore player){
        player.IncreaseAttackRange(bulletRangeBonus);
    }

    public override void OnStacked(PlayerCore player){
        player.IncreaseAttackRange(bulletRangeBonusPerStack);
    }
}
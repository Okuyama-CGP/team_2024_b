using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 攻撃力を増やすアップグレード
/// </summary>
[CreateAssetMenu(fileName = "UpgradeAttackPower", menuName = "Upgrades/UpgradeAttackPower")]
public class UpgradeAttackPower : BaseUpgrade {
    
    [Space]
    [SerializeField] float attackPowerBonus = 1f;
    [SerializeField] float attackPowerBonusPerStack = 0.5f;

    public override void OnAdded(PlayerCore player) {
        player.IncreaseAttackPower(attackPowerBonus);
    }

    public override void OnStacked(PlayerCore player) {
        player.IncreaseAttackPower(attackPowerBonusPerStack);
    }
}

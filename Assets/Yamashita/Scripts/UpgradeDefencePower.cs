using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 防御力を増やすアップグレード
/// </summary>
[CreateAssetMenu(fileName = "UpgradeDefencePower", menuName = "Upgrades/UpgradeDefencePower")]
public class UpgradeDefencePower : BaseUpgrade {
    
    [Space]
    [SerializeField] float defencePowerBonus = 0.2f;
    [SerializeField] float defencePowerBonusPerStack = 0.1f;

    public override void OnAdded(PlayerCore player) {
        player.IncreaseDefencePower(defencePowerBonus);
    }

    public override void OnStacked(PlayerCore player) {
        player.IncreaseDefencePower(defencePowerBonusPerStack);
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 獲得経験値倍率を増やすアップグレード
/// </summary>
[CreateAssetMenu(fileName = "UpgradeExpBoost", menuName = "Upgrades/UpgradeExpBoost")]
public class UpgradeExpBoost : BaseUpgrade {
    
    [Space]
    [SerializeField] float ExpBoostBonus = 0.2f;
    [SerializeField] float ExpBoostBonusPerStack = 0.1f;

    public override void OnAdded(PlayerCore player) {
        player.IncreaseEXPBoost(ExpBoostBonus);
    }

    public override void OnStacked(PlayerCore player) {
        player.IncreaseEXPBoost(ExpBoostBonusPerStack);
    }
}

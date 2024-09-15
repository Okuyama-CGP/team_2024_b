using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// MaxHPを増やすアップグレード
/// </summary>
[CreateAssetMenu(fileName = "UpgradeHealth", menuName = "Upgrades/UpgradeHealth")]
public class UpgradeHealth : BaseUpgrade {

    [Space]
    [SerializeField] float healthBonus = 10f;
    [SerializeField] float healthBonusPerStack = 5f;

    public override void OnAdded(PlayerCore player) {
        player.IncreaseMaxHP(healthBonus);
    }

    public override void OnStacked(PlayerCore player) {
        player.IncreaseMaxHP(healthBonusPerStack);
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// アイテムの吸引範囲を増やすアップグレード
/// </summary>
[CreateAssetMenu(fileName = "UpgradeMagnet", menuName = "Upgrades/UpgradeMagnet")]
public class UpgradeMagnet : BaseUpgrade {
    
    [Space]
    [SerializeField] float magnetRangeBonus = 1f;
    [SerializeField] float magnetRangeBonusPerStack = 1f;

    public override void OnAdded(PlayerCore player) {
        player.IncreaseSuckDistance(magnetRangeBonus);
    }

    public override void OnStacked(PlayerCore player) {
        player.IncreaseSuckDistance(magnetRangeBonusPerStack);
    }
}


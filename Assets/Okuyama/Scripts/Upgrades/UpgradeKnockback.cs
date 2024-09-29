using UnityEngine;

/// <summary>
/// 攻撃速度を増やすアップグレード
/// </summary>
[CreateAssetMenu(fileName = "UpgradeKnockback", menuName = "Upgrades/UpgradeKnockback")]
public class UpgradeKnockback : BaseUpgrade {
    
    [Space]
    [SerializeField] float KnockbackBonus = 0.2f;
    [SerializeField] float KnockbackBonusPerStack = 0.2f;

    public override void OnAdded(PlayerCore player) {
        player.IncreaseKnockbackRatio(KnockbackBonus);
    }

    public override void OnStacked(PlayerCore player) {
        player.IncreaseKnockbackRatio(KnockbackBonusPerStack);
    }
}

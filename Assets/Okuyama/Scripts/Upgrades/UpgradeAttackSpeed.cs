using UnityEngine;

/// <summary>
/// 攻撃速度を増やすアップグレード
/// </summary>
[CreateAssetMenu(fileName = "UpgradeAttackSpeed", menuName = "Upgrades/UpgradeAttackSpeed")]
public class UpgradeAttackSpeed : BaseUpgrade {
    
    [Space]
    [SerializeField] float AttackSpeedBonus = 0.15f;
    [SerializeField] float AttackSpeedBonusPerStack = 0.15f;

    public override void OnAdded(PlayerCore player) {
        player.IncreaseAttackSpeed(AttackSpeedBonus);
    }

    public override void OnStacked(PlayerCore player) {
        player.IncreaseAttackSpeed(AttackSpeedBonusPerStack);
    }
}

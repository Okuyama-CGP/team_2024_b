using UnityEngine;

/// <summary>
/// 自動回復を増やすアップグレード
/// </summary>
[CreateAssetMenu(fileName = "UpgradeRegeneration", menuName = "Upgrades/UpgradeRegeneration")]
public class UpgradeRegeneration : BaseUpgrade {
    
    [Space]
    [SerializeField] float regenerationBunus = 0.3f;
    [SerializeField] float regenerationBonusPerStack = 0.3f;

    public override void OnAdded(PlayerCore player) {
        player.IncreaseRegenerationSpeed(regenerationBunus);
    }

    public override void OnStacked(PlayerCore player) {
        player.IncreaseRegenerationSpeed(regenerationBonusPerStack);
    }
}

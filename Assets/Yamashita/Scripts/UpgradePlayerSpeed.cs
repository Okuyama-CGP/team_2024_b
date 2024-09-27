using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 移動速度を上げるアップグレード
/// </summary>
[CreateAssetMenu(fileName = "UpgradePlayerSpeed", menuName = "Upgrades/UpgradePlayerSpeed")]
public class UpgradePlayerSpeed : BaseUpgrade{

    [Space]
    [SerializeField] float playerSpeedBonus = 1f;
    [SerializeField] float playerSpeedBonusPerStack = 0.5f;

    public override void OnAdded(PlayerCore player){
        player.IncreaseMoveSpeed(playerSpeedBonus);
    }

    public override void OnStacked(PlayerCore player){
        player.IncreaseMoveSpeed(playerSpeedBonusPerStack);
    }
}

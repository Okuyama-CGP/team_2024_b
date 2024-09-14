using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 利用可能なアップグレードを取りつくしたときの、ダミーアップグレード
/// 現状効果なし。
/// </summary>
[CreateAssetMenu(fileName = "UpgradeDummy", menuName = "Upgrades/UpgradeDummy")]
public class UpgradeDummy : BaseUpgrade {
    
    public override void OnAdded(PlayerCore player) {

    }

    public override void OnStacked(PlayerCore player) {

    }
}


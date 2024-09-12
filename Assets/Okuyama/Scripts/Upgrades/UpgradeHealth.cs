using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeHealth : BaseUpgrade
{
    const float healthBonus = 10f;
    const float healthBonusPerStack = 5f;

    public override void OnAdded(PlayerCore player)
    {
        player.IncreaseMaxHP(healthBonus);
    }

    public override void OnStacked(PlayerCore player)
    {
        player.IncreaseMaxHP(healthBonusPerStack);
    }
}


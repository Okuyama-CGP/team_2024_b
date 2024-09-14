using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeOnhitTest : BaseUpgrade
{
    public override void OnAdded(PlayerCore player)
    {
        player.OnHitEvent += OnHitEffect;
        Debug.Log("OnHit追加");
    }

    public override void OnStacked(PlayerCore player)
    {
        Debug.Log("OnHitスタック : " + stackCount);
    }

    //OnHit処理の内容
    public void OnHitEffect(IDamageable target, Damage damage)
    {
        Debug.Log("OnHit : " + stackCount);
    }
}

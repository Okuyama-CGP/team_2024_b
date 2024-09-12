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

    public override void OnRemoved(PlayerCore player)
    {
        player.OnHitEvent -= OnHitEffect;
        Debug.Log("OnHit削除");
    }

    public void OnHitEffect(IDamageable target, Damage damage)
    {
        Debug.Log("OnHit : " + damage.damageValue);
    }
}

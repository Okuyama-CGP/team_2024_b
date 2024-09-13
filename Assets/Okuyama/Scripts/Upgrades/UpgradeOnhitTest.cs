using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeOnhitTest", menuName = "Upgrades/UpgradeOnhitTest")]
public class UpgradeOnhitTest : BaseUpgrade
{
    [Space]
    [SerializeField] GameObject particle;

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
    public void OnHitEffect(GameObject target, Damage damage)
    {
        //パーティクル生成
        Vector3 pos = target.transform.position + new Vector3(0, 1, 0);
        Instantiate(particle, pos, Quaternion.identity);
    }
}

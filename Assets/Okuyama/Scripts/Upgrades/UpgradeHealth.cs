using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeHealth : BaseUpgrade
{
    public override void OnAdded(PlayerCore player)
    {
        Debug.Log("最大ヘルス増加");//TODO ヘルス増加処理
    }

    public override void OnRemoved(PlayerCore player)
    {
        Debug.Log("最大ヘルス減少");
    }
}


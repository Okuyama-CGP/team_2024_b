using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    PlayerCore playerCore;

    /// <summary>
    /// 所持しているアップグレードのリスト
    /// </summary>
    private List<BaseUpgrade> upgradesList = new List<BaseUpgrade>();

    void Start()
    {
        playerCore = MainGameManager.instance.playerCore;
    }

    /// <summary>
    /// アップグレードを追加する
    /// </summary>
    public void AddUpgrade(BaseUpgrade upgrade)
    {
        upgradesList.Add(upgrade);
        upgrade.OnAdded(playerCore);
    }

    /// <summary>
    /// アップグレードを削除する
    /// </summary>
    public void RemoveUpgrade(BaseUpgrade upgrade)
    {
        upgradesList.Remove(upgrade);
        upgrade.OnRemoved(playerCore);
    }
}

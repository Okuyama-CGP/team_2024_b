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
        //TODO upgradeIDとかで判断したい
        BaseUpgrade sameUpgrade = upgradesList.Find(u => u.GetType() == upgrade.GetType());
        if(sameUpgrade != null){
            //既に所持：スタック
            sameUpgrade.stackCount++;
            sameUpgrade.OnStacked(playerCore);
        }else{
            //未所持：追加
            upgradesList.Add(upgrade);
            upgrade.OnAdded(playerCore);
        }

        //TODO maxStack制限
    }
}

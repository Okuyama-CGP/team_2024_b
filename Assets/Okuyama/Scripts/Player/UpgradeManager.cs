using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour {
    /// <summary>
    /// アップグレード全種類の参照、インスペクタで設定。
    /// </summary>
    [SerializeField] List<BaseUpgrade> allUpgrades;

    /// <summary>
    /// 所持しているアップグレードのリスト
    /// </summary>
    private List<BaseUpgrade> upgradesList = new List<BaseUpgrade>();

    /// <summary>
    /// 既に最大スタックに達したアップグレードのフラグ
    /// </summary>
    public bool[] maxStackedUpgrades;

    //privateプロパティ
    PlayerCore playerCore;

    void Start() {
        playerCore = MainGameManager.instance.playerCore;
        //trueで初期化
        maxStackedUpgrades = new bool[allUpgrades.Count];
    }

    void Update() {
        //TODO デバッグ用
        if (Input.GetKeyDown(KeyCode.I)) {
            AddUpgrade(0);
        }
        if (Input.GetKeyDown(KeyCode.O)) {
            AddUpgrade(1);
        }
    }

    /// <summary>
    /// アップグレードを追加する
    /// </summary>
    public void AddUpgrade(int upgradeID) {

        BaseUpgrade upgrade = allUpgrades[upgradeID];
        if (upgradesList.Contains(upgrade)) { //既にこのUpgradeを所持

            if (upgrade.stackCount < upgrade.maxStack) { //スタック可能

                upgrade.stackCount++;
                upgrade.OnStacked(playerCore);

                //所持上限に到達
                if (upgrade.stackCount == upgrade.maxStack) maxStackedUpgrades[upgradeID] = true;

            } else {                                     //既に所持上限
                Debug.LogError("アプグレ所持上限");
            }
        } else {
            //未所持：追加
            upgradesList.Add(upgrade);
            upgrade.stackCount = 1;
            upgrade.OnAdded(playerCore);

        }
    }
}

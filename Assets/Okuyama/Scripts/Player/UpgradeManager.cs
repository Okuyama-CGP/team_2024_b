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
    /// 全て取得し終わった後の、ダミーアップグレード
    /// </summary>
    [SerializeField] BaseUpgrade dummyUpgrade;

    /// <summary>
    /// 所持しているアップグレードのリスト
    /// </summary>
    private List<BaseUpgrade> upgradesList = new List<BaseUpgrade>();

    /// <summary>
    /// 獲得可能なアップグレードのリスト
    /// </summary>
    private List<BaseUpgrade> obtainableUpgrades {
        get {
            List<BaseUpgrade> result = new List<BaseUpgrade>();
            foreach (var upgrade in allUpgrades) {
                if (upgrade.isObtainable) {
                    result.Add(upgrade);
                }
            }
            return result;
        }
    }


    //privateプロパティ
    PlayerCore playerCore;
    UImanager uImanager;

    void Start() {
        playerCore = MainGameManager.instance.playerCore;
        uImanager = MainGameManager.instance.uImanager;
    }

    void Update() {
        //TODO デバッグ用
        if (Input.GetKeyDown(KeyCode.I)) {
            AddUpgrade(allUpgrades[0]);
        }
        if (Input.GetKeyDown(KeyCode.O)) {
            AddUpgrade(allUpgrades[1]);
        }
    }

    /// <summary>
    /// アップグレードを追加する
    /// </summary>
    public void AddUpgrade(BaseUpgrade upgrade) {

        if (upgradesList.Contains(upgrade)) { //既にこのUpgradeを所持

            if (upgrade.isObtainable) {       //獲得可能

                upgrade.stackCount++;
                upgrade.OnStacked(playerCore);

            } else {                           //既に所持上限
                Debug.LogError("アプグレ所持上限");
            }
        } else {
            //未所持：追加
            upgradesList.Add(upgrade);
            upgrade.stackCount = 1;
            upgrade.OnAdded(playerCore);

        }
    }

    public void LevelUp() {
        //ランダムにアップグレードを選出
        List<BaseUpgrade> upgrades = GetRandomUpgrade(3);
        //UI表示
        uImanager.ActivateLevelUpUI(upgrades);
    }

    /// <summary>
    /// num種類の獲得可能アップグレードをランダムに選出
    /// appearanceRateに応じて重み付けされる
    /// </summary>
    public List<BaseUpgrade> GetRandomUpgrade(int num) {
        List<BaseUpgrade> result = new List<BaseUpgrade>();
        List<BaseUpgrade> obtainable = obtainableUpgrades;

        //全体の出現頻度の合計算出
        float totalRate = 0;
        foreach (var upgrade in obtainable) {
            totalRate += upgrade.appearanceRate;
        }

        for (int i = 0; i < num; i++) { //N回

            if (obtainable.Count == 0) { //獲得可能なアップグレードがない → ダミー
                result.Add(dummyUpgrade);
            } else {
                //ランダムに選択
                float randomValue = UnityEngine.Random.Range(0, totalRate);
                float sumRate = 0;
                foreach (var upgrade in obtainable) {
                    sumRate += upgrade.appearanceRate;
                    if (sumRate >= randomValue) {  //選択
                        result.Add(upgrade);
                        obtainable.Remove(upgrade);
                        totalRate -= upgrade.appearanceRate;
                        break;
                    }
                }
            }
        }

        return result;
    }
}

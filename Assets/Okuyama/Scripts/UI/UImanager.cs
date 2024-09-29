using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UImanager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI hpText;
    [SerializeField] TextMeshProUGUI expText;
    [SerializeField] TextMeshProUGUI upgradesText;

    /// <summary>
    /// レベルアップ時のupgrade選択用UI
    /// </summary>
    [SerializeField] LevelUpUI levelUpUI;

    /// <summary>
    /// ゲームオーバー時のUI
    /// </summary>
    [SerializeField] GameOverUI gameOverUI;

    PlayerCore playerCore;

    void Start()
    {
        playerCore = MainGameManager.instance.playerCore;
        levelUpUI.gameObject.SetActive(false);

        playerCore.upgradeManager.OnUpgradesChanged += UpdateUpgradesText;
    }

    void Update()
    {
        //HP表示(仮)
        hpText.text = "HP: " + (int)playerCore.hp + " / " + playerCore.maxHP;

        //EXP表示(仮)
        expText.text = "Level: " + playerCore.level + "  EXP: " + (int)playerCore.exp;
    }


    //ゲーム実行中---------------
    /// <summary>
    /// 取得中のupgradeを表示 TODO:アプグレアイコンのせる
    /// </summary>
    public void UpdateUpgradesText(List<BaseUpgrade> upgrades){
        string text = "";
        foreach (var upgrade in upgrades){
            text += upgrade.upgradeName + "   ×" + upgrade.stackCount + "\n";
        }
        upgradesText.text = text;
    }


    //レベルアップ関係---------------
    /// <summary>
    /// レベルアップUIを表示
    /// ・ 表示内容を初期化、時間停止
    /// </summary>
    public void ActivateLevelUpUI(List<BaseUpgrade> upgrades){
        levelUpUI.gameObject.SetActive(true);
        levelUpUI.InitializePanel(upgrades);
        Time.timeScale = 0;
    }
    /// <summary>
    /// レベルアップUIの任務完了
    /// ・ UI非表示、upgradeを適用、時間再開
    /// </summary>
    public void CompleteLevelUpUI(BaseUpgrade upgrade){
        levelUpUI.gameObject.SetActive(false);
        playerCore.upgradeManager.AddUpgrade(upgrade);
        Time.timeScale = 1;
    }

    //ゲームオーバー関係---------------
    /// <summary>
    /// ゲームオーバーUIを表示
    /// ・ 表示内容を初期化、時間停止
    /// </summary>
    public void ActivateGameOverUI(){
        gameOverUI.gameObject.SetActive(true);
        gameOverUI.InitializePanel();
    }
}

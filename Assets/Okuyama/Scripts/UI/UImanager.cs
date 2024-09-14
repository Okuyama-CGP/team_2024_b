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

    PlayerCore playerCore;

    // Start is called before the first frame update
    void Start()
    {
        playerCore = MainGameManager.instance.playerCore;
        levelUpUI.gameObject.SetActive(false);

        playerCore.upgradeManager.OnUpgradesChanged += UpdateUpgradesText;
    }

    // Update is called once per frame
    void Update()
    {
        //HP表示(仮)
        hpText.text = "HP: " + (int)playerCore.HP + " / " + playerCore.MaxHP;

        //EXP表示(仮)
        expText.text = "Level: " + playerCore.Level + "  EXP: " + (int)playerCore.EXP;
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


    public void UpdateUpgradesText(List<BaseUpgrade> upgrades){
        string text = "";
        foreach (var upgrade in upgrades){
            text += upgrade.upgradeName + "   ×" + upgrade.stackCount + "\n";
        }
        upgradesText.text = text;
    }
}

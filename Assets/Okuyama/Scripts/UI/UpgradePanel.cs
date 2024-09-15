using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradePanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI upgradeName;
    [SerializeField] TextMeshProUGUI upgradeDescription;
    [SerializeField] Image upgradeIcon;

    //親であるレベルアップ時のUI
    LevelUpUI levelUpUI;
    //表示中のupgrade
    BaseUpgrade upgrade;

    /// <summary>
    /// 初期化 一回のみ
    /// </summary>
    public void Initialize(LevelUpUI levelUpUI){
        this.levelUpUI = levelUpUI;
    }

    /// <summary>
    /// upgrade表示内容の更新(初期化)
    /// </summary>
    public void InitializeUpgrade(BaseUpgrade upgrade){
        this.upgrade = upgrade;

        upgradeName.text = upgrade.upgradeName;
        upgradeDescription.text = upgrade.description;
        upgradeIcon.sprite = upgrade.icon;
    }

    /// <summary>
    /// パネルクリック時の処理
    /// ButtonのOnClickイベントで呼び出す
    /// </summary>
    public void OnClick(){
        levelUpUI.OnSelected(upgrade);
    }
}

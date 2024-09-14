using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpUI : MonoBehaviour {
    [SerializeField] List<UpgradePanel> upgradePanels;


    void Start() {
        //子に参照を伝える
        foreach (var panel in upgradePanels) {
            panel.Initialize(this);
        }
    }

    /// <summary>
    /// 表示内容初期化 LevelUpUIがactiveにされてから呼び出される
    /// </summary>
    public void InitializePanel(List<BaseUpgrade> upgrades) {
        for (int i = 0; i < upgradePanels.Count; i++) {
            upgradePanels[i].InitializeUpgrade(upgrades[i]);
        }
    }

    /// <summary>
    /// いずれかのUpgradePanelが選択された時の処理
    /// upgradeを適用して、このUIを閉じる
    /// </summary>
    public void OnSelected(BaseUpgrade upgrade) {
        MainGameManager.instance.uImanager.CompleteLevelUpUI(upgrade);
    }
}

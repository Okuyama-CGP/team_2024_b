using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeBox : MonoBehaviour {
    [SerializeField] GameObject UpgradeIconPrefab;

    [SerializeField] Vector2 initialPos; //一個目のアイコンの位置
    [SerializeField] Vector2 intervalPos; //アイコン間の間隔

    

    PlayerCore playerCore { get { return MainGameManager.instance.playerCore; } }

    private void Start() {
        //全削除
        foreach (Transform child in transform) {
            Destroy(child.gameObject);
        }
        playerCore.upgradeManager.OnUpgradesChanged += UpdateUpgradeBox;
    }

    /// <summary>
    /// upgradeに並んだアイコンを更新
    /// </summary>
    public void UpdateUpgradeBox(List<BaseUpgrade> upgrades) {
        //全削除
        foreach (Transform child in transform) {
            Destroy(child.gameObject);
        }


        for (int i = 0; i < upgrades.Count; i++) {
            var upgrade = upgrades[i];
            var icon = Instantiate(UpgradeIconPrefab, transform);

            //位置設定
            RectTransform rect = icon.GetComponent<RectTransform>();
            rect.anchoredPosition = new Vector2(initialPos.x + intervalPos.x * i, 0); //TODO 二列表示

            //内容設定
            UpgradeIcon upgradeIcon = icon.GetComponent<UpgradeIcon>();
            upgradeIcon.SetIcon(upgrade.icon);
            upgradeIcon.SetUpgradeCount(upgrade.stackCount);
        }
    }
}

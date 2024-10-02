using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeIcon : MonoBehaviour {
    [SerializeField] Image iconImage;
    [SerializeField] private TextMeshProUGUI upgradeCountTMP;

    public void SetIcon(Sprite icon) {
        iconImage.sprite = icon;
    }

    public void SetUpgradeCount(int count) {
        if (count > 1) {
            upgradeCountTMP.text = "×" + count.ToString();
        } else {
            upgradeCountTMP.text = "";
        }
    }
}


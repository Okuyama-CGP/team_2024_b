using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI upgradesText;
    [SerializeField] TextMeshProUGUI timerText;

    private float survivalTime = 0f;
    private bool isSurviving = true;

    PlayerCore playerCore;

    // Start is called before the first frame update
    void Start()
    {
        playerCore = MainGameManager.instance.playerCore;
        playerCore.upgradeManager.OnUpgradesChanged += UpdateUpgradesText;
    }

    void Update()
    {
        if (isSurviving)
        {
            survivalTime += Time.deltaTime;
            //Debug.Log("時間" + survivalTime);
        }
    }

    public void EndSurvival()
    {
        isSurviving = false;
        timerText.text = "生存時間: " + Mathf.FloorToInt(survivalTime) + "秒";
    }

    public void UpdateUpgradesText(List<BaseUpgrade> upgrades){
        string text = "";
        foreach (var upgrade in upgrades){
            text += upgrade.upgradeName + "   ×" + upgrade.stackCount + "\n";
        }
        upgradesText.text = text;
    }
}

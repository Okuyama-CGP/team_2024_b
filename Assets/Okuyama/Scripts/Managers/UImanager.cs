using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UImanager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI hpText;
    [SerializeField] TextMeshProUGUI expText;

    PlayerCore playerCore;

    // Start is called before the first frame update
    void Start()
    {
        playerCore = MainGameManager.instance.playerCore;
    }

    // Update is called once per frame
    void Update()
    {
        //HP表示(仮)
        hpText.text = "HP: " + (int)playerCore.HP + " / " + playerCore.MaxHP;

        //EXP表示(仮)
        expText.text = "Level: " + playerCore.Level + "  EXP: " + (int)playerCore.EXP;
    }
}

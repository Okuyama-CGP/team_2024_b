using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UImanager : MonoBehaviour
{
    [SerializeField] PlayerCore playerCore;
    [SerializeField] TextMeshProUGUI hpText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //HP表示(仮)
        hpText.text = "HP: " + (int)playerCore.HP + " / " + playerCore.MaxHP;
    }
}

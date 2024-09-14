using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class hpbar : MonoBehaviour
{
  PlayerCore playerCore;
    private Image _image;
    // Start is called before the first frame update
    void Start()
    {
        _image = this.GetComponent<Image>();
        playerCore = MainGameManager.instance.playerCore;
    }

    // Update is called once per frame
    void Update()
    {

       _image.fillAmount = playerCore.HP / playerCore.MaxHP;
    }
    //画像を長方形に変えたい
    //背景に枠を追加する
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameManager : MonoBehaviour
{
    /// <summary>
    /// PlayerCore : Playerの状態などを管理している。
    /// </summary>
    [SerializeField]public PlayerCore playerCore;

    /// <summary>
    /// UImanager : UIの表示、更新を管理している
    /// </summary>
    [SerializeField]public UImanager uImanager;

    //シングルトンパターン
    public static MainGameManager instance;
    private void Awake()
    {
        if (instance == null){
            instance = this;
            //DontDestroyOnLoad(gameObject); シーン跨ぎ。要検討
        }else{
            Destroy(gameObject);
        }
    }
}

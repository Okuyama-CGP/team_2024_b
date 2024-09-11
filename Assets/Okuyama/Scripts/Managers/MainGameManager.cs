using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameManager : MonoBehaviour
{
    /// <summary>
    /// PlayerCoreスクリプト Playerの状態などを管理している。
    /// </summary>
    [SerializeField]public PlayerCore playerCore;

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

    void Start()
    {
        
    }
}

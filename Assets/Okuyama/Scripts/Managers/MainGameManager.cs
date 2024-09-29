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

    /// <summary>
    /// グローバル空間に置かれるカーソル
    /// </summary>
    [SerializeField]public CursolObject cursolObject;

    /// <summary>
    /// どこでも聞こえる音を鳴らすためのAudioSource
    /// </summary>
    [SerializeField] AudioSource audioSourceGrobal;
    /// <summary>
    /// SEのOneShot再生
    /// 可聴範囲無しの2D音声。
    /// </summary>
    public void PlayOneShot(AudioClip clip){
        audioSourceGrobal.PlayOneShot(clip);
    }


    /// <summary>
    /// 生存時間
    /// </summary>
    public float survivedTime {get; private set;} = 0;

    void Start()
    {
        survivedTime = 0;
        playerCore.OnDeath += OnGameOver;
    }

    void Update()
    {
        survivedTime += Time.deltaTime;
    }

    /// <summary>
    /// ゲームオーバー時の処理
    /// </summary>
    void OnGameOver(){
        audioSourceGrobal.Stop();
        uImanager.ActivateGameOverUI();
    }


    //シングルトンパターン
    public static MainGameManager instance;
    private void Awake()
    {
        if (instance == null){
            instance = this;
            //DontDestroyOnLoad(gameObject); //シーン跨ぎ、要検討 
        }else{
            Destroy(gameObject);
        }
    }
}

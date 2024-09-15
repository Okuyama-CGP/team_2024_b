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
    /// OneShot再生用のAudioSource
    /// </summary>
    [SerializeField] AudioSource audioSourceOneShot;
    /// <summary>
    /// SEのOneShot再生
    /// 可聴範囲無しの2D音声。
    /// </summary>
    public void PlayOneShot(AudioClip clip){
        audioSourceOneShot.PlayOneShot(clip);
    }

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

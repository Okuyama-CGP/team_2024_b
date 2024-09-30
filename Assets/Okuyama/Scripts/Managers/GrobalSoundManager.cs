using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrobalSoundManager : MonoBehaviour {
    /// <summary>
    /// どこでも聞こえる音を鳴らすためのAudioSource
    /// </summary>
    [SerializeField] AudioSource audioSourceGrobal;
    [SerializeField] AudioSource mainBGMsource; //メインBGM用
    [Space]
    [SerializeField] AudioClip deathSE;
    [SerializeField] AudioClip gameOverSE;
    [SerializeField] AudioClip levelUpSE;


    /// <summary>
    /// SEのOneShot再生
    /// 可聴範囲無しの2D音声。
    /// </summary>
    public void PlayOneShot(AudioClip clip, float volume = 1.0f) {
        audioSourceGrobal.PlayOneShot(clip,volume);
    }

    public void PlayDeathSE() {
        PlayOneShot(deathSE,0.7f);
    }

    public void PlayGameOverSE() {
        PlayOneShot(gameOverSE,0.7f);
    }

    public void PlayLevelUpSE() {
        PlayOneShot(levelUpSE);
    }

    /// <summary>
    /// メインBGMの停止
    /// </summary>
    public void StopMainBGM() {
        mainBGMsource.Stop();
    }
}

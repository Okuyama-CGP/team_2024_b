using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class WaveManager : MonoBehaviour
{
    [System.Serializable]
    public struct WaveObject
    {
        public GameObject waveObject;
        public float waveDuration;
    }

    [SerializeField] List<WaveObject> waveObjects = new List<WaveObject>();   
    //各waveScript,終了時刻

    [SerializeField] float allWaveEndedMultiplier = 2.0f; //全てのwaveが終了した際の敵のステータス倍率

    int CurrentWaveIndex = 0; //現在のwaveのインデックス

    float ElapsedTime = 0; //ゲーム開始からの経過時間
    float CurrentWaveEndTime = 0; //現在のwaveの終了時刻
    BaseWave CurrentWaveScript;


    void Start()
    {
        CurrentWaveScript = waveObjects[0].waveObject.GetComponent<BaseWave>();
        if(CurrentWaveScript == null){
            Debug.LogError("WaveObjectにBaseWaveを継承したスクリプトがアタッチされていません");
        }else{
            CurrentWaveScript.OnStartWave(); //第一ウェーブ開始処理
            CurrentWaveEndTime = waveObjects[0].waveDuration;
        }
    }

    void Update()
    {
        ElapsedTime += Time.deltaTime;

        if (ElapsedTime >= CurrentWaveEndTime) //ウェーブ終了時刻到達時、次のウェーブへ
        {
            CurrentWaveScript.OnEndWave(); //現在のウェーブの終了時処理
            CurrentWaveIndex++;

            if(CurrentWaveIndex == waveObjects.Count){
                CurrentWaveIndex = 0;
                MainGameManager.instance.enemyStatusMultiplier *= allWaveEndedMultiplier;
            }
            CurrentWaveScript = waveObjects[CurrentWaveIndex].waveObject.GetComponent<BaseWave>();
            CurrentWaveScript.OnStartWave(); //次のウェーブの開始処理
            CurrentWaveEndTime += waveObjects[CurrentWaveIndex].waveDuration;

        }
        else
        {
            
            CurrentWaveScript.OnWaveUpdate(); //ウェーブ実行中の処理
        }
    }
}

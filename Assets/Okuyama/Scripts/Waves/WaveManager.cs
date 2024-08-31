using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class WaveManager : MonoBehaviour
{
    [System.Serializable]
    public struct WaveObject
    {
        public GameObject waveObject;
        public float endTimeSec;
    }

    [SerializeField] List<WaveObject> waveObjects = new List<WaveObject>();   
    //各waveScript,終了時刻

    int CurrentWaveIndex = 0; //現在のwaveのインデックス

    float ElapsedTime = 0; //ゲーム開始からの経過時間
    float CurrentWaveEndTime = 0; //現在のwaveの終了時刻
    BaseWave CurrentWaveScript;

    bool isEnd = false;

    void Start()
    {
        CurrentWaveScript = waveObjects[0].waveObject.GetComponent<BaseWave>();
        if(CurrentWaveScript == null){
            Debug.LogError("WaveObjectにBaseWaveを継承したスクリプトがアタッチされていません");
        }else{
            CurrentWaveScript.OnStartWave(); //第一ウェーブ開始処理
            CurrentWaveEndTime = waveObjects[0].endTimeSec;
        }
    }

    void Update()
    {
        ElapsedTime += Time.deltaTime;

        if (ElapsedTime >= CurrentWaveEndTime) //ウェーブ終了時刻到達時、次のウェーブへ
        {
            CurrentWaveScript.OnEndWave(); //現在のウェーブの終了時処理
            CurrentWaveIndex++;

            if (CurrentWaveIndex < waveObjects.Count){
                CurrentWaveScript = waveObjects[CurrentWaveIndex].waveObject.GetComponent<BaseWave>();
                CurrentWaveScript.OnStartWave(); //次のウェーブの開始処理
                CurrentWaveEndTime = waveObjects[CurrentWaveIndex].endTimeSec;
            }else{
                CurrentWaveEndTime = float.PositiveInfinity;

                //TODO:全ウェーブ終了時(ゲーム終了時?)の処理
                Debug.Log("終了！！！aaa");
                isEnd = true;

            }
        }
        else
        {
            if (!isEnd){
                CurrentWaveScript.OnWaveUpdate(); //ウェーブ実行中の処理
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class WaveManager : MonoBehaviour
{
    [System.Serializable]
    public struct WaveData
    {
        public IWave iwaveaa;
        public GameObject waveObject;
        public float endTimeSec;
    }

    [SerializeField] List<WaveData> waveDatas = new List<WaveData>();   //各waveScript,終了時刻

    int CurrentWaveIndex = 0; //現在のwaveのインデックス

    float ElapsedTime = 0; //ゲーム開始からの経過時間
    float CurrentWaveEndTime = 0; //現在のwaveの終了時刻
    IWave CurrentIWave;

    bool isEnd = false;

    void Start()
    {
        CurrentIWave = waveDatas[0].waveObject.GetComponent<IWave>();
        CurrentIWave.OnStartWave(); //第一ウェーブ開始処理
        CurrentWaveEndTime = waveDatas[0].endTimeSec;
    }

    void Update()
    {
        ElapsedTime += Time.deltaTime;

        if (ElapsedTime >= CurrentWaveEndTime) //ウェーブ終了時刻到達時、次のウェーブへ
        {
            CurrentIWave.OnEndWave(); //現在のウェーブの終了時処理
            CurrentWaveIndex++;

            if (CurrentWaveIndex < waveDatas.Count){
                CurrentIWave = waveDatas[CurrentWaveIndex].waveObject.GetComponent<IWave>();
                CurrentIWave.OnStartWave(); //次のウェーブの開始処理
                CurrentWaveEndTime = waveDatas[CurrentWaveIndex].endTimeSec;
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
                CurrentIWave.OnWaveUpdate(); //ウェーブ実行中の処理
            }
        }
    }
}

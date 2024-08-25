using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ウェーブを定義するときの基底。
/// 特殊ウェーブとか作るときはこれを継承してね<br/>
/// <c>OnStartWave()</c> : ウェーブ開始時の処<br/>
/// <c>OnEndWave()</c> : ウェーブ終了時の処理<br/>
/// <c>OnWaveUpdate()</c> : ウェーブ実行中、毎フレーム
/// </summary>
public interface IWave
{
    void OnStartWave();
    void OnEndWave();
    void OnWaveUpdate();
}

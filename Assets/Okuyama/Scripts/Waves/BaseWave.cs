using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 各ウェーブを表現するコンポーネントの基底クラス<br/>
/// <c>OnStartWave() </c> : ウェーブ開始時の処理<br/>
/// <c>OnEndWave() </c>   : ウェーブ終了時の処理<br/>
/// <c>OnWaveUpdate() </c> : ウェーブ中の処理
/// </summary>
public abstract class BaseWave : MonoBehaviour
{
    public abstract void OnStartWave();
    public abstract void OnEndWave();
    public abstract void OnWaveUpdate();
}

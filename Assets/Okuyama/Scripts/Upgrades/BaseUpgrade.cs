
using System;
using UnityEngine;

public abstract class BaseUpgrade : ScriptableObject {
    
    [Header("---------- BaseUpgradeInfo ----------")]
    public string upgradeName = "NoName";
    [TextArea(3, 10)]
    public string description = "Upgradeの説明テキストテキストテキストテキスト";
    public Sprite icon;
    public int maxStack = 99;
    [Tooltip("upgradeの出現頻度。ノーマル(HP増加など)で1.0。レアなほど小さく。")]
    public float appearanceRate = 1.0f;

    /// <summary>
    /// 取得してスタックされた数
    /// </summary>
    [NonSerialized] public int stackCount = 0;

    /// <summary>
    /// まだ獲得可能であるか(スタック数が上限に達していないか)
    /// </summary>
    public bool isObtainable { get { return stackCount < maxStack; } }

    /// <summary>
    /// アップグレードが追加されたときの処理
    /// ステータスを増やす、イベントの追加などを記述する。
    /// </summary>
    public abstract void OnAdded(PlayerCore player);

    /// <summary>
    /// アップグレードがスタックされたときの処理
    /// </summary>
    public abstract void OnStacked(PlayerCore player);
}

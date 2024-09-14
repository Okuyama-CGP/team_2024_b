
using UnityEngine;

public abstract class BaseUpgrade : ScriptableObject {
    
    [Header("---------- BaseUpgradeInfo ----------")]
    public string upgradeName = "NoName";
    [TextArea(3, 10)]
    public string description = "Upgradeの説明テキストテキストテキストテキスト";
    public Sprite icon;
    public int maxStack = 99;
    [Tooltip("アプグレ出現率の重み。ノーマル(HP増加など)で1.0。レアなほど小さく。")]
    public float appearanceRate = 1.0f;

    public int stackCount { get; set; } = 1;

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

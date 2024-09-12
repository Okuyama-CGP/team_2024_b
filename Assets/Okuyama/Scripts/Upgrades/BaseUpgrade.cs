
public abstract class BaseUpgrade
{
    public int stackCount { get; set; } = 1;

    public int maxStack { get; protected set; } = 10;

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

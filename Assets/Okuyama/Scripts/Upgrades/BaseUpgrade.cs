
public abstract class BaseUpgrade
{
    public int StackCount { get; protected set; } = 1;

    public int MaxStack = 10;

    /// <summary>
    /// アップグレードが追加されたときの処理
    /// ステータスを増やす、イベントの追加などを記述する。
    /// </summary>
    public abstract void OnAdded(PlayerCore player);

    /// <summary>
    /// アップグレードが削除されたときの処理
    /// </summary>
    public abstract void OnRemoved(PlayerCore player);
}

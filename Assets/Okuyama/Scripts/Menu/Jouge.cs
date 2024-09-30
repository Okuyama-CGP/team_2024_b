using UnityEngine;

public class Jouge : MonoBehaviour
{
    public float movementSpeed = 1.0f; // 移動速度
    public float heightRange = 1.0f;   // 上下移動の範囲

    private float initialPositionY;

    void Start()
    {
        // オブジェクトの最初のY位置を保存
        initialPositionY = transform.position.y;
    }

    void Update()
    {
        // 時間に基づいてサイン波でY座標を計算 (-1~1 の値を取得し、それを高さ範囲に拡大)
        float offsetY = Mathf.Sin(Time.time * movementSpeed) * heightRange;

        // 現在の位置を取得し、Y軸のみを変更
        Vector3 currentPosition = transform.position;
        currentPosition.y = initialPositionY + offsetY;

        // 新しい位置を適用
        transform.position = currentPosition;
    }
}

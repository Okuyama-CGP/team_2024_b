using UnityEngine;

public class Yureru : MonoBehaviour
{
    public float rotationSpeed = 1.0f; // 回転速度
    public float angleRange = 3.0f;    // 回転角度の範囲 (例: -3度から3度)

    private float initialRotationX;

    void Start()
    {
        // オブジェクトの最初のx軸の回転値を保存
        initialRotationX = transform.rotation.eulerAngles.x;
    }

    void Update()
    {
        // 時間に基づいてサイン波で角度を計算 (-1~1 の値を取得し、それを角度範囲に拡大)
        float angle = Mathf.Sin(Time.time * rotationSpeed) * angleRange;

        // 回転角度を新しい角度に設定
        Vector3 currentRotation = transform.rotation.eulerAngles;
        currentRotation.x = initialRotationX + angle;

        // 新しい回転を適用
        transform.rotation = Quaternion.Euler(currentRotation);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    private Vector3 initialPosition; // 初期のカメラ位置

    [SerializeField] GameObject focusGameoverPos; // ゲームオーバー時のカメラ位置
    private Vector3 focusGameoverPosition;
    private Quaternion focusGameoverRotation;
    bool isGameoverFocus = false;

    void Start() {
        //初期位置
        initialPosition = transform.localPosition;
    }

    void Update() {
        if (isGameoverFocus) FocusGameover();
    }

    public void CameraShake(float duration = 0.5f, float magnitude = 0.1f) {
        StartCoroutine(Shake(duration, magnitude));
    }
    IEnumerator Shake(float duration, float magnitude) {
        float elapsed = 0.0f;

        while (elapsed < duration) {
            // ランダムな位置を生成
            Vector3 randomPoint = initialPosition + Random.insideUnitSphere * magnitude;

            // カメラの位置を変更
            transform.localPosition = randomPoint;
            // 時間経過を更新
            elapsed += Time.unscaledDeltaTime;

            // 次のフレームまで待つ
            yield return null;
        }

        // 振動後に元の位置に戻す
        transform.localPosition = initialPosition;
    }

    // ゲームオーバー時のカメラ位置に移動開始
    public void StartGameoverFocus() {
        isGameoverFocus = true;
        focusGameoverPosition = focusGameoverPos.transform.position;
        focusGameoverRotation = focusGameoverPos.transform.rotation;
    }
    void FocusGameover() {
        transform.position = Vector3.Slerp(transform.position, focusGameoverPosition, Time.deltaTime * 2);
        transform.rotation = Quaternion.Slerp(transform.rotation, focusGameoverRotation, Time.deltaTime * 2);
    }

}

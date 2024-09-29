using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHPbar : MonoBehaviour {
    [SerializeField] Slider hpSlider;
    [SerializeField] float hideDelay = 2.0f;    // ダメージを受けてからHPバーが消えるまでの時間
    [SerializeField] float fadeDuration = 0.5f; // フェードアウトにかかる時間
    private Coroutine hideCoroutine;
    private CanvasGroup canvasGroup;


    void Start() {
        // CanvasGroup を取得または追加
        canvasGroup = GetComponent<CanvasGroup>();

        // 初期状態でHPバーを非表示に
        canvasGroup.alpha = 0f;
    }

    // ダメージを受けたときに呼ばれる関数
    public void ActivateHPbar(float value) {
        // HPバーを更新,表示
        hpSlider.value = value;
        canvasGroup.alpha = 1f;

        // もし以前の非表示コルーチンが実行中なら、停止する
        if (hideCoroutine != null) {
            StopCoroutine(hideCoroutine);
        }

        // 一定時間後にHPバーをフェードアウトして非表示にするコルーチンを開始
        hideCoroutine = StartCoroutine(HideHealthBarAfterDelay());
    }
    IEnumerator HideHealthBarAfterDelay() {

        yield return new WaitForSeconds(hideDelay);

        // フェードアウト
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration) {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            yield return null;
        }
        canvasGroup.alpha = 0f;
    }
}

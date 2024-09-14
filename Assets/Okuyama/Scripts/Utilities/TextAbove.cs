using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// オブジェクトの頭上にテキストを表示する簡易的なやつ
/// デバックとかで使ってね。
/// </summary>
public class TextAbove : MonoBehaviour
{
    [SerializeField, TextArea] string text = "textAbove";
    [SerializeField] int fontSize = 10;
    [SerializeField] float height = 1.0f;

    TextMeshPro tmp;


    void Start()
    {
        GameObject tmpObj = new GameObject("TMP Text");
        tmpObj.transform.SetParent(this.transform);

        MeshRenderer meshRenderer = tmpObj.AddComponent<MeshRenderer>();

        tmp = tmpObj.AddComponent<TextMeshPro>();
        tmpObj.transform.localPosition = new Vector3(0, height, 0);
        tmp.text = text;
        tmp.fontSize = fontSize;
        tmp.alignment = TextAlignmentOptions.Center;
    }

    void Update()
    {

    }

    /// <summary>
    /// 頭上に表示するテキストを変更する
    /// </summary>
    public void SetText(string text)
    {
        tmp.text = text;
    }
}

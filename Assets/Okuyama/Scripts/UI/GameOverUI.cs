using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI survivedTimeText; //生存時間

    /// <summary>
    /// 表示内容を初期化
    /// </summary>
    public void InitializePanel()
    {
        survivedTimeText.text = "生存時間 : " + MainGameManager.instance.survivedTime.ToString("F2") + " s";
    }


    /// <summary>
    /// リスタートボタンの処理
    /// </summary>
    public void ButtonRestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /// <summary>
    /// タイトルに戻るボタンの処理
    /// </summary>
    public void ButtonQuitGame()
    {
        SceneManager.LoadScene("Title");
    }
}

using System;
using System.Collections;
using UnityEngine;

public enum GameState {
    Playing,
    GameOver
}

public class MainGameManager : MonoBehaviour {
    /// <summary>
    /// PlayerCore : Playerの状態などを管理している。
    /// </summary>
    [SerializeField] public PlayerCore playerCore;

    /// <summary>
    /// UImanager : UIの表示、更新を管理している
    /// </summary>
    [SerializeField] public UImanager uImanager;

    /// <summary>
    /// グローバル空間で鳴らす、BGMなどを管理
    /// </summary>
    [SerializeField] public GrobalSoundManager grobalSoundManager;

    /// <summary>
    /// グローバル空間に置かれるカーソル
    /// </summary>
    [SerializeField] public CursolObject cursolObject;

    /// <summary>
    /// ゲームの状態(InGame内のみ)
    /// </summary>
    public GameState gameState { get; private set; } = GameState.Playing;

    /// <summary>
    /// ゲーム中かどうかを簡単に取得
    /// </summary>
    public bool isPlaying { get { return gameState == GameState.Playing; } }

    /// <summary>
    /// ゲームオーバーイベント
    /// </summary>
    public event Action GameOverEvent;

    /// <summary>
    /// 生存時間
    /// </summary>
    public float survivedTime { get; private set; } = 0;

    void Start() {
        gameState = GameState.Playing;
        survivedTime = 0;
    }

    void Update() {
        switch (gameState) {
            case GameState.Playing:
                UpdateOnPlaying();
                break;
            case GameState.GameOver:
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// ゲーム中の更新処理
    /// </summary>
    void UpdateOnPlaying() {
        survivedTime += Time.deltaTime;
    }



    /// <summary>
    /// ゲームオーバー処理
    /// </summary>
    public void GameOver() {
        gameState = GameState.GameOver;
        GameOverEvent?.Invoke();
        StartCoroutine(GameOverCoroutine());
    }
    // ゲームオーバー演出
    public IEnumerator GameOverCoroutine() {
        Time.timeScale = 0.2f; //スローモーション化
        //カメラ振動
        //TODO エフェクト
        grobalSoundManager.StopMainBGM(); //BGM停止
        grobalSoundManager.PlayDeathSE(); //死亡SE再生

        yield return new WaitForSeconds(0.2f);

        Time.timeScale = 1f; //スローモーション解除
        playerCore.moveController.StopRB(); //プレイヤーの押し出し停止
        playerCore.animator.SetTrigger("death"); //アニメーション
        playerCore.moveController.gameoverFocus = true;
        //カメラズーム
        grobalSoundManager.PlayGameOverSE(); //ゲームオーバーSE再生
        
        yield return new WaitForSeconds(4f);

        uImanager.ActivateGameOverUI(); //ゲームオーバーUI表示
    }


    //シングルトンパターン
    public static MainGameManager instance;
    private void Awake() {
        if (instance == null) {
            instance = this;
            //DontDestroyOnLoad(gameObject); //シーン跨ぎ、要検討 
        } else {
            Destroy(gameObject);
        }
    }
}

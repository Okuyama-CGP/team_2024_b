using System;
using System.Collections;
using UnityEngine;
using unityroom.Api;

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
    /// ゲームエリアのサイズ
    /// </summary>
    [SerializeField] private Vector2 GameAreaSize = new Vector2(100, 100);
    [SerializeField] private Vector2 GameAreaCenter = Vector2.zero;

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

    /// <summary>
    /// 敵のステータス倍率
    /// </summary>
    public float enemyStatusMultiplier = 1.0f;

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
        SendUnityroomSurvivalTime(); //生存時間送信
        GameOverEvent?.Invoke();
        StartCoroutine(GameOverCoroutine());
    }
    // ゲームオーバー演出
    public IEnumerator GameOverCoroutine() {
        Time.timeScale = 0.4f; //スローモーション化
        playerCore.cameraController.CameraShake(0.2f, 0.1f); //カメラ振動
        grobalSoundManager.StopMainBGM(); //BGM停止
        grobalSoundManager.PlayDeathSE(); //死亡SE再生

        yield return new WaitForSecondsRealtime(1f);

        Time.timeScale = 1f; //スローモーション解除
        playerCore.moveController.StopRB(); //プレイヤーの押し出し停止
        playerCore.animator.SetTrigger("death"); //アニメーション
        playerCore.moveController.StartGameoverFocus(); //カメラ目線
        playerCore.cameraController.StartGameoverFocus(); //ズーム
        grobalSoundManager.PlayGameOverSE(); //ゲームオーバーSE再生

        yield return new WaitForSeconds(3f);

        uImanager.ActivateGameOverUI(); //ゲームオーバーUI表示
    }


    /// <summary>
    /// UnityRoomに生存時間を送信する関数
    /// 参考： https://github.com/naichilab/unityroom-client-library?tab=readme-ov-file
    /// </summary>
    public void SendUnityroomSurvivalTime() {
        UnityroomApiClient.Instance.SendScore(1,survivedTime,ScoreboardWriteMode.HighScoreDesc);
    }


    /// <summary>
    /// 指定地点がゲームエリア内かどうかを判定する関数
    /// </summary>
    public bool IsInGameArea(Vector3 pos) {
        return Mathf.Abs(pos.x - GameAreaCenter.x) < GameAreaSize.x / 2 && Mathf.Abs(pos.z - GameAreaCenter.y) < GameAreaSize.y / 2;
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

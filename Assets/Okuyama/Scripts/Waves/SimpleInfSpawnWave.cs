using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleInfSpawnWave : BaseWave
{
    [SerializeField] float spawnInterval = 1.0f;
    [SerializeField] GameObject enemyPrefab;

    float elapsedTime = 0;
    PlayerCore playerCore;

    public override void OnStartWave()
    {
        Debug.Log(name + " Start");
        playerCore = MainGameManager.instance.playerCore;
    }

    public override void OnWaveUpdate()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= spawnInterval) //Intervalごとに敵を生成
        {
            //距離20の円周上にランダムに敵を生成
            //FIXME マップ外スポーン対策
            float dist = 20;
            float deg = UnityEngine.Random.Range(0, Mathf.PI * 2);

            Vector3 spawnPos = new Vector3(Mathf.Cos(deg), 0, Mathf.Sin(deg)) * dist;   //極座標変換
            spawnPos += playerCore.position;
            GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
            elapsedTime -= spawnInterval;
        }
    }

    public override void OnEndWave()
    {
        Debug.Log(name + "End");
    }
}

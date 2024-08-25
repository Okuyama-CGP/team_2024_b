using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleInfSpawn : MonoBehaviour, IWave
{
    [SerializeField] float spawnInterval = 1.0f;
    [SerializeField] GameObject enemyPrefab;

    float elapsedTime = 0;
    GameObject Player;

    public void OnStartWave()
    {
        Debug.Log(name + " Start");
        Player = GameObject.Find("Player");
    }

    public void OnWaveUpdate()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= spawnInterval) //Intervalごとに敵を生成
        {
            //距離20の円周上にランダムに敵を生成
            //TODO:マップ外スポーン対策
            float dist = 20;
            float deg = UnityEngine.Random.Range(0, Mathf.PI * 2);

            Vector3 spawnPos = new Vector3(Mathf.Cos(deg), 0, Mathf.Sin(deg)) * dist;   //極座標変換
            spawnPos += Player.transform.position;
            Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
            elapsedTime -= spawnInterval;
        }
    }

    public void OnEndWave()
    {
        Debug.Log(name + "End");
    }
}

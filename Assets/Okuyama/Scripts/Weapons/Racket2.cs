using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Racket2 : BaseWeapon
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] AudioClip swingSE;
    
    static float distance = 2.0f; //どれくらい遠くに出現させるか

    public override void UseWeapon(PlayerCore usePlayer)
    {
        Vector3 cursolDirection = usePlayer.cursolDirection;
        cursolDirection.Normalize();

        //Bullet生成
        Vector3 spawnPos = usePlayer.position + cursolDirection * distance;
        spawnPos.y = 1f;
        Instantiate(bulletPrefab, spawnPos, Quaternion.identity);

        //サウンド
        MainGameManager.instance.PlayOneShot(swingSE);
    }
}

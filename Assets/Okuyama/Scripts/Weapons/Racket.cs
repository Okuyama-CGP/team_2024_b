using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Racket : BaseWeapon
{
    [SerializeField] GameObject bulletPrefab;
    
    float range = 2.0f; //どれくらい遠くに出現させるか

    public override void UseWeapon(PlayerCore usePlayer)
    {
        Transform playerModelTF = usePlayer.model.transform;
        Vector3 spawnPos = playerModelTF.position + playerModelTF.forward * range;
        Instantiate(bulletPrefab, spawnPos, playerModelTF.rotation);

        //TODO:カーソル方向にする？
    }
}

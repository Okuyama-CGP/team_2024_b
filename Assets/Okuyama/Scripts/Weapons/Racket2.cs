using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Racket2 : BaseWeapon
{
    [SerializeField] GameObject bulletPrefab;
    
    float range = 2.0f; //どれくらい遠くに出現させるか

    public override void UseWeapon(PlayerCore usePlayer)
    {
        Transform playerModelTF = usePlayer.model.transform;

        Vector3 cursolDirection = usePlayer.cursolDirection;
        cursolDirection.Normalize();

        Vector3 spawnPos = playerModelTF.position + cursolDirection * range;
        Instantiate(bulletPrefab, spawnPos, Quaternion.identity);
    }
}

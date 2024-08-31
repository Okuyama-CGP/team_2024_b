using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseWeapon : MonoBehaviour, IUseable
{
    /// <summary>
    /// 武器の使用クールタイム
    /// </summary>
    public float useCoolTime = 1.0f;

    /// <summary>
    /// 実際の武器使用処理。overrideしてね
    /// </summary>
    public abstract void UseWeapon(PlayerCore usePlayer);

    protected float lastUseTime = 0.0f;

    public bool TryUse(PlayerCore usePlayer){
        if(Time.time >= lastUseTime + useCoolTime){
            UseWeapon(usePlayer);
            lastUseTime = Time.time;
            return true;
        }else{
            return false;
        }
    }

    
}

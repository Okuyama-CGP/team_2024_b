using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseWeapon : MonoBehaviour
{
    /// <summary>
    /// 武器の使用にかかる時間 = 移動速度ペナルティの時間
    /// </summary>
    [SerializeField] public float useDuration = 0.5f;

    /// <summary>
    /// 武器の使用クールタイム モーションと合わせるのが望ましい
    /// </summary>
    [SerializeField] public float useCoolTime = 1.0f;

    /// <summary>
    /// 攻撃中の移動速度ペナルティ
    /// </summary>
    [SerializeField] public float moveSpeedPenalty = 0.3f;

    /// <summary>
    /// 実際の武器使用処理。overrideしてね
    /// </summary>
    public abstract void UseWeapon(PlayerCore usePlayer);
    
}

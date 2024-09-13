using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// カーソル位置をワールド空間に表示するオブジェクト
/// </summary>
public class CursolObject : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    [SerializeField] float y = 0.1f; //カーソルのY座標固定値

    /// <summary>
    /// カーソルのワールド座標 (Y=0)
    /// </summary>
    public Vector3 cursolPosition { get {
        return new Vector3(transform.position.x, 0, transform.position.z); 
    } }

    void Start()
    {
        
    }

    void Update()
    {
        //カーソル位置に移動
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        float distance = (y - ray.origin.y) / ray.direction.y; //指定Y平面までの距離
        transform.position =  ray.origin + ray.direction * distance;
        
        //TODO 通常のマウスカーソル消してもいいかもね
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : BaseEnemy
{
    GameObject Player;
    [SerializeField] float speed = 1.0f;
    Rigidbody rb;

    protected override void Start()
    {
        base.Start();
        Player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody>();
    }

    protected override void Update()
    {
        base.Update();
        Vector3 direction = (Player.transform.position - transform.position).normalized;
        rb.velocity = direction * speed;
    }
}

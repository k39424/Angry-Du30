﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoLifeTime : MonoBehaviour {

    public float lifeTime;

    public GameObject ammo;
    public SpringJoint2D spring;
    public Rigidbody2D rigid;
   
    private void Awake()
    {
        spring = GetComponent<SpringJoint2D>();
        
        rigid = GetComponent<Rigidbody2D>();

        if (lifeTime <= 0)
            lifeTime = 3.0f;
    }

    private void Update()
    {
        if (spring == null && ammo.GetComponent<Rigidbody2D>().velocity.magnitude <= 0.3f)
        {
            Destroy(ammo, lifeTime);
        }

        else
        {

        }
    }
}

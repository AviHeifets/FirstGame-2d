using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Player : Charchter
{
    private Slider FearBar;
    Vector2 direction;

    int lastDirection;

    private void Awake()
    {
        speed = 6.0f;
        FearBar = GameObject.Find("FearBar").GetComponent<Slider>();
        RigidBody = GetComponent<Rigidbody2D>();

    }

    private void Update()
    {
        Movment();
    }

    void FixedUpdate()
    {
        RigidBody.MovePosition(RigidBody.position + speed * Time.fixedDeltaTime * direction);
        PreventSpin();
    }

    private void PreventSpin()
    {
        RigidBody.angularVelocity = 0;

        if (RigidBody.transform.rotation.z != 0)
            RigidBody.transform.rotation = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);
    }


    private void Movment()
    {
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.y = Input.GetAxisRaw("Vertical");
        if (direction.x == 0 && direction.y == 0)
            lastDirection = 0;


        if (Math.Abs(direction.x) > Math.Abs(direction.y))
        {
            direction.y = 0;
            lastDirection = 1;

        }
        else if (Math.Abs(direction.x) < Math.Abs(direction.y))
        {
            direction.x = 0;
            lastDirection = -1;
        }
        else
        {
            if (lastDirection == 1)
                direction.x = 0;
            if (lastDirection == -1)
                direction.y = 0;
        }

        Animation.SetFloat("Horizontal", direction.x);
        Animation.SetFloat("Vertical", direction.y);
        Animation.SetFloat("Speed", direction.sqrMagnitude);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Enemy>(out _))
        {
            Die();
        }
    }


}

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

    private List<Image> BatteryIcons;
    private int batteryCount;
    private Sprite FullBattery;

    private void Start()
    {
        BatteryIcons = new List<Image>();
        speed = 6.0f;
        FearBar = GameObject.Find("FearBar").GetComponent<Slider>();
        RigidBody = GetComponent<Rigidbody2D>();

        FullBattery = Resources.Load<Sprite>("Fullbattery");
        BatteryIcons.Add(GameObject.Find("Battery1").GetComponent<Image>());
        BatteryIcons.Add(GameObject.Find("Battery2").GetComponent<Image>());
        BatteryIcons.Add(GameObject.Find("Battery3").GetComponent<Image>());
    } 

    private void Update()
    {
        Movment();
    }

    void FixedUpdate()
    {
        RigidBody.MovePosition(RigidBody.position + speed * Time.fixedDeltaTime * direction);
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
        if (collision.gameObject.TryGetComponent<EnemyAi>(out _))
        {
            Die();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Battery")
        {
            if (batteryCount < 3)
            {
                batteryCount++;
                collision.gameObject.SetActive(false);
                BatteryIcons[batteryCount - 1].sprite = FullBattery;
            }
        }
            
            
    }


}

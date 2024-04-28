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

    private Item Battery;
    private Item Bravery;


    private void Start()
    {
        speed = 6.0f;
        FearBar = GameObject.Find("FearBar").GetComponent<Slider>();
        RigidBody = GetComponent<Rigidbody2D>();

        LoadIcons();
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

    private void LoadIcons()
    {
        Battery.Icons = new List<Image>();
        Bravery.Icons = new List<Image>();

        Battery.FullSprite = Resources.Load<Sprite>("Fullbattery");
        Bravery.FullSprite = Resources.Load<Sprite>("Fullbravery");

        Battery.Icons.Add(GameObject.Find("Battery1").GetComponent<Image>());
        Battery.Icons.Add(GameObject.Find("Battery2").GetComponent<Image>());
        Battery.Icons.Add(GameObject.Find("Battery3").GetComponent<Image>());

        Bravery.Icons.Add(GameObject.Find("Bravery1").GetComponent<Image>());
        Bravery.Icons.Add(GameObject.Find("Bravery2").GetComponent<Image>());
        Bravery.Icons.Add(GameObject.Find("Bravery3").GetComponent<Image>());
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
        if (collision.CompareTag("Battery"))
        {
            if (Battery.Count < 3)
            {
                Battery.Count++;
                collision.gameObject.SetActive(false);
                GameObject.Destroy(collision.gameObject);
                Battery.Icons[Battery.Count - 1].sprite = Battery.FullSprite;
                MainLogicScript.Instance.BatteryCount--;
            }
        }
        if (collision.CompareTag("Bravery"))
        {
            if (Bravery.Count < 3)
            {
                Bravery.Count++;
                collision.gameObject.SetActive(false);
                GameObject.Destroy(collision.gameObject);
                Bravery.Icons[Bravery.Count - 1].sprite = Bravery.FullSprite;
                MainLogicScript.Instance.BraveryCount--;
            }
        }
    }

}

struct Item
{
    public List<Image> Icons;
    public int Count;
    public Sprite FullSprite;

    public Item(List<Image> icons, Sprite fullSprite)
    {
        Icons = icons;
        Count = 0;
        FullSprite = fullSprite;
    }
}

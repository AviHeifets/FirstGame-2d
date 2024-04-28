using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MainLogicScript : MonoBehaviour
{
    public static MainLogicScript Instance { get; private set; }
    public int BatteryCount { get; set; }
    public int BraveryCount { get; set; }

    private Tilemap groundTiles;
    private List<Vector3Int> groundTilePositions = new List<Vector3Int>();


    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;

        groundTiles = GameObject.Find("Ground").GetComponent<Tilemap>();
        GetGroundTilePositions();
    }

    // Update is called once per frame
    void Update()
    {
        if (BatteryCount < 10)
        {
            MainLogicScript.Instance.SpawnItem("Battery");
            BatteryCount++;
        }
        //if (BraveryCount < 10)
        //{
        //    MainLogicScript.Instance.SpawnItem("Bravery");
        //    BraveryCount++;
        //}
    }

    public void SpawnItem(string Tag)
    {
        Vector3Int randomPosition = groundTilePositions[Random.Range(0, groundTilePositions.Count)];
        Vector3 spawnPosition = groundTiles.CellToWorld(randomPosition) + new Vector3(0.5f, 0.5f, 0);
        spawnPosition.z = 0f;

        GameObject item = new GameObject();
        item.tag = Tag;

        item.AddComponent<BoxCollider2D>();
        item.AddComponent<SpriteRenderer>();

        item.GetComponent<BoxCollider2D>().isTrigger = true;
        item.GetComponent<BoxCollider2D>().size = new Vector2(0.44f, 0.68f);
        item.GetComponent<Transform>().localScale = new Vector3(0.6f, 0.6f, 0.6f);
        item.GetComponent<Transform>().position = spawnPosition;

        switch(Tag)
        {
            case "Battery":
                item.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Fullbattery");
                return;
            case "FullBravery":
                item.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("FullBravery");
                return;
            default:
                Debug.Log("Error loading sprite");
                return;
        }
    }

    private void GetGroundTilePositions()
    {
        foreach (Vector3Int pos in groundTiles.cellBounds.allPositionsWithin)
        {
            if (groundTiles.HasTile(pos))
            {
                groundTilePositions.Add(pos);
            }
        }
    }
}

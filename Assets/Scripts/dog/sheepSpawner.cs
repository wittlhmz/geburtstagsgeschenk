using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sheepSpawner : MonoBehaviour
{
    public GameObject sheepPrefab;
    public int sheepAmount = 4;

    public float minX = -10f;
    public float maxX = 17f;
    public float minY = -10f;
    public float maxY = 0f;

    void Start()
    {
        SpawnSheep();
    }

    void SpawnSheep()
    {
        for (int i = 0; i < sheepAmount; i++)
        {
            float x = Random.Range(minX, maxX);
            float y = Random.Range(minY, maxY);

            Vector2 spawnPos = new Vector2(x, y);

            Instantiate(sheepPrefab, spawnPos, Quaternion.identity);
        }
    }
}

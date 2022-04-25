using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    private const int SEGMENT_SIZE = 8;
    public int ZOFFSET;
    public GameObject startPlatform;
    public GameObject[] levelSegments;
    public GameObject teleporter;
    public int vertical;
    public int horizontal;

    void Start()
    {
        GenerateLevel();
    }

    void GenerateLevel()
    {
        Instantiate(startPlatform, this.transform.position, Quaternion.identity);
        for (int i = 0; i < vertical; i++)
        {
            Instantiate(teleporter, this.transform.position + new Vector3(SEGMENT_SIZE * horizontal + SEGMENT_SIZE, 0, ZOFFSET * i), Quaternion.identity);
        }
        for (int i = 0; i < horizontal; i++)
        {
            for (int j = 0; j < vertical; j++)
            {
                Instantiate(levelSegments[Random.Range(0, levelSegments.Length - 1)], this.transform.position + new Vector3(SEGMENT_SIZE * i + SEGMENT_SIZE, 0, ZOFFSET * j), Quaternion.identity);
            }
        }
    }
}

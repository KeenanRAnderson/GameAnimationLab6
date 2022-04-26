using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Written by Keenan Anderson
public class LevelGenerator : MonoBehaviour
{
    private const int SEGMENT_SIZE = 8;
    public int ZOFFSET;
    public GameObject startPlatform;
    public GameObject winPlatform;
    public GameObject[] levelSegments;
    public GameObject startTeleporter;
    public GameObject endTeleporter;
    public CameraFollow mainCam;
    public int vertical;
    public int horizontal;

    void Start()
    {
        GenerateLevel();
    }

    void GenerateLevel()
    {
        GameObject start;
        GameObject end;
        Instantiate(startPlatform, this.transform.position, Quaternion.identity);
        Instantiate(winPlatform, this.transform.position + new Vector3(SEGMENT_SIZE * horizontal + SEGMENT_SIZE, 0, ZOFFSET * (vertical - 1)), Quaternion.identity);
        for (int i = 0; i < vertical - 1; i++)
        {
            start = Instantiate(startTeleporter, this.transform.position + new Vector3(0, 0, ZOFFSET * i + ZOFFSET), Quaternion.identity);
            end = Instantiate(endTeleporter, this.transform.position + new Vector3(SEGMENT_SIZE * horizontal + SEGMENT_SIZE, 0, ZOFFSET * i), Quaternion.identity);
            start.GetComponentInChildren<Teleporter>().SetTeleporterAndCameraFollow(end.GetComponentInChildren<Teleporter>(), mainCam);
            end.GetComponentInChildren<Teleporter>().SetTeleporterAndCameraFollow(start.GetComponentInChildren<Teleporter>(), mainCam);
        }
        for (int i = 0; i < horizontal; i++)
        {
            for (int j = 0; j < vertical; j++)
            {
                Instantiate(levelSegments[Random.Range(0, levelSegments.Length)], this.transform.position + new Vector3(SEGMENT_SIZE * i + SEGMENT_SIZE, 0, ZOFFSET * j), Quaternion.identity);
            }
        }
    }
}

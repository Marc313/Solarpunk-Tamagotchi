using System.Collections.Generic;
using UnityEngine;

public class Spawner : Singleton<Spawner>
{
    public float floorOffset;
    public GameObject floorPrefab;
    public float floorCount = 1;
    public Transform firstFloorReference;

    [Header("Obstacles")]
    public GameObject obstaclePrefab;
    public GameObject slopePrefab;

    private Vector3 FirstFloorPosition;
    private List<GameObject> floorList = new List<GameObject>();
    private int floorListIndex = 0;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        FirstFloorPosition = firstFloorReference.transform.position;
        floorList.Add(firstFloorReference.gameObject);
        SpawnFloor();
        SpawnFloor();
        SpawnFloor();
    }

    public void SpawnFloor()
    {
        Vector3 newPosition = FirstFloorPosition + Vector3.forward * ((floorOffset * floorCount));
        GameObject newFloor = Instantiate(floorPrefab, newPosition, Quaternion.identity, transform);
        floorList.Add(newFloor);
        if (floorListIndex - 3 > 0)
            floorList[floorListIndex - 3].SetActive(false);
        floorListIndex++;
        floorCount++;

        int obstacleCount = 0;

        for (int i = 0; i < 3; i++)
        {
            float rand = Random.Range(0f, 1f);
            if (rand < 0.66f)
            {
                SpawnObstacle(i, newPosition);
                obstacleCount++;
            }
        }

        if (obstacleCount == 3)
        {
            SpawnSlope(Random.Range(0, 3), newPosition);
        }
    }

    private void SpawnObstacle(int index, Vector3 newPosition)
    {
        float xPos = -3 + 3 * index;
        Vector3 pos = new Vector3(xPos, obstaclePrefab.transform.position.y, newPosition.z);
        Instantiate(obstaclePrefab, pos, obstaclePrefab.transform.rotation, transform);
    }

    private void SpawnSlope(int index, Vector3 newPosition)
    {
        float xPos = -3 + 3 * index;
        Vector3 pos = new Vector3(xPos, slopePrefab.transform.position.y, newPosition.z - 5);
        Instantiate(slopePrefab, pos, slopePrefab.transform.rotation, transform);
    }
}

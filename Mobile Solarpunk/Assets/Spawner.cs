using UnityEngine;

public class Spawner : Singleton<Spawner>
{
    public float floorOffset;
    public GameObject floorPrefab;
    public float floorCount = 1;

    private Vector3 startPosition;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        startPosition= transform.position;
        SpawnFloor();
    }

    public void SpawnFloor()
    {
        Vector3 newPosition = startPosition + Vector3.forward * ((floorOffset * floorCount));
        GameObject newFloor = Instantiate(floorPrefab, newPosition, Quaternion.identity, transform);
        floorCount++;
    }
}

using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    [Header("Forward Force")]
    [SerializeField] private float speed;

    private bool isActive = true;
    private Rigidbody rigidBody;

    private float lastSpawnZPos;
    private float spawnInterval;

    // Touch detection
    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        lastSpawnZPos = transform.position.z;
        spawnInterval = Spawner.Instance.floorOffset;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isActive) return;

        // Constant force
        ApplyForwardForce();

        // Detect Swipe
        DetectSwipeOrArrow();

        // Check for Floor Spawn
        CheckForFloorSpawn();
    }

    private void CheckForFloorSpawn()
    {
        if (transform.position.z > lastSpawnZPos + spawnInterval)
        {
            lastSpawnZPos += spawnInterval;
            Spawner.Instance.SpawnFloor();
        }
    }

    private void DetectSwipeOrArrow()
    {
        // Swipe Input
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            startTouchPosition = Input.GetTouch(0).position;

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            endTouchPosition = Input.GetTouch(0).position;

            if (endTouchPosition.x < startTouchPosition.x) MoveToLeft();
            else if (endTouchPosition.x > startTouchPosition.x) MoveToRight();
        }

        // Arrow Key Input
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveToLeft();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveToRight();
        }
    }

    private void MoveToRight()
    {
        Vector3 offset = LaneManager.Instance.ToRight();
        transform.position += offset;
    }

    private void MoveToLeft()
    {
        Vector3 offset = LaneManager.Instance.ToLeft();
        transform.position += offset;
    }

    private void ApplyForwardForce()
    {
        Debug.Log("Whooshh");
        rigidBody.AddForce(speed * Vector3.forward * Time.deltaTime, ForceMode.Force);
    }

    public void OnHit()
    {
        GetComponentInChildren<Animator>().enabled = false;
        isActive = false;
        RunnerGameManager.Instance.OnLose();
    }
}

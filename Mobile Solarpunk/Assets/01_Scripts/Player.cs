using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    [Header("Forward Force")]
    [SerializeField] private float startSpeed;
    [SerializeField] private float speedIncreasePerSecond;
    [SerializeField] private float gravityStrenght;

    private bool isActive = true;
    private Rigidbody rigidBody;
    private Animator anim;

    private float lastSpawnZPos;
    private float spawnInterval;
    private bool gravityEnabled;

    // Touch detection
    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
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

        startSpeed += speedIncreasePerSecond * Time.deltaTime;
        anim.speed = 0.8f * (startSpeed / 10000);

        // Constant force
        ApplyForwardForce();

        // Detect Swipe
        DetectSwipeOrArrow();

        // Check for Floor Spawn
        CheckForFloorSpawn();

        if (gravityEnabled)
        {
            ApplyGravity();
        }
    }

    public void EnableGravity()
    {
        gravityEnabled = true;
        Invoke(nameof(DisableGravity), .9f);
    }

    public void DisableGravity()
    {
        gravityEnabled= false;

        Vector3 velocity = rigidBody.velocity;
        velocity.y = 0;
        rigidBody.velocity = velocity;
    }

    private void ApplyGravity()
    {
        Vector3 velocity = rigidBody.velocity;
        velocity.y -= gravityStrenght * Time.deltaTime;
        rigidBody.velocity = velocity;
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
        rigidBody.AddForce(startSpeed * Vector3.forward * Time.deltaTime, ForceMode.Force);
    }

    public void OnHit()
    {
        GetComponentInChildren<Animator>().enabled = false;
        isActive = false;
        RunnerGameManager.Instance.OnLose();
    }
}

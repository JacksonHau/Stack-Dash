using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Speeds")]
    public float forwardSpeed = 5f;
    public float laneSwitchSpeed = 5f;


    [Header("Lane Settings")]
    [Tooltip("Total number of lanes (odd number gives a true center).")]
    public int numLanes = 3;
    [Tooltip("World‑space distance between adjacent lanes.")]
    public float laneOffset = 2f;

    private CharacterController cc;
    private int currentLane;   // 0 .. numLanes-1
    private float gravity = -9.81f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cc = GetComponent<CharacterController>();
        // start in center lane
        currentLane = numLanes / 2;
    }

    // Update is called once per frame
    void Update()
    {
        // Read discrete left/right input
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            currentLane = Mathf.Min(currentLane + 1, numLanes - 1);
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            currentLane = Mathf.Max(currentLane - 1, 0);

        // Compute target X based on lane index
        float centerIndex = (numLanes - 1) / 2f;
        float targetX = (currentLane - centerIndex) * laneOffset;

        // Build movement vector
        Vector3 move = Vector3.forward * forwardSpeed;

        // Smoothly interpolate our X‑velocity toward the target lane
        float deltaX = targetX - transform.position.x;
        float horizontalVel = deltaX * laneSwitchSpeed;
        move += Vector3.right * horizontalVel;

        // Gravity
        move.y += gravity * Time.deltaTime;

        // Apply character controller movement
        cc.Move(move * Time.deltaTime);
    }
}

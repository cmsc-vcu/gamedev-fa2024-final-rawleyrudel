using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingLine : MonoBehaviour
{
    public float dropSpeed = 5f;
    public float returnSpeed = 3f;
    public float maxDepth = -5f;
    public Transform reelPosition; // Position where the line should start (e.g., the rod tip)
    public LineRenderer lineRenderer; // Reference to the Line Renderer
    [HideInInspector] public bool isReturning = false;
    private bool isDropping = false;
    private Vector3 initialPosition; // Store the original position of the line

    void Start()
    {
        // Ensure lineRenderer is assigned and initialized
        if (lineRenderer == null)
        {
            lineRenderer = GetComponent<LineRenderer>();
        }
        lineRenderer.positionCount = 2; // We only need two points: start and end

        // Store the initial position
        initialPosition = transform.position;
    }

    void Update()
    {
        HandleInput();
        MoveLine();
        UpdateLineRenderer();
    }

    void HandleInput()
    {
        // Toggle between dropping and returning when space bar is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isDropping)
            {
                // If dropping, interrupt and start returning
                isDropping = false;
                isReturning = true;
            }
            else if (!isReturning)
            {
                // If not already returning, start dropping
                isDropping = true;
            }
        }

        // Automatically start returning if max depth is reached
        if (transform.position.y <= maxDepth && isDropping)
        {
            isDropping = false;
            isReturning = true;
        }
    }

    void MoveLine()
    {
        if (isDropping)
        {
            // Move straight down
            transform.position += Vector3.down * dropSpeed * Time.deltaTime;
        }
        else if (isReturning)
        {
            // Move back towards the initial position
            transform.position = Vector3.MoveTowards(transform.position, initialPosition, returnSpeed * Time.deltaTime);

            // Allow horizontal movement with arrow keys while moving up
            float horizontal = Input.GetAxis("Horizontal");
            transform.position += Vector3.right * horizontal * returnSpeed * Time.deltaTime;

            // Stop returning once it reaches the initial position
            if (transform.position == initialPosition)
            {
                isReturning = false;
            }
        }
    }

    void UpdateLineRenderer()
    {
        // Set the start position of the line at the reel (rod tip)
        lineRenderer.SetPosition(0, reelPosition.position);

        // Set the end position of the line at the hook's current position
        lineRenderer.SetPosition(1, transform.position);
    }
}

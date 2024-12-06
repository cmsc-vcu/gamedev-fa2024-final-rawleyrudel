using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingLine : MonoBehaviour
{
    public float dropSpeed = 5f; // Speed of the line when dropping
    public float returnSpeed = 3f; // Speed of the line when returning
    public float horizontalSpeed = 2f; // Configurable speed for left and right movement
    public float maxDepth = -5f; // Maximum depth the line can reach
    public Transform reelPosition; // Position where the line starts (e.g., rod tip)
    public LineRenderer lineRenderer; // Reference to the Line Renderer
    private Vector3 initialPosition; // Store the original position of the line

    void Start()
    {
        // Ensure the lineRenderer is assigned and initialized
        if (lineRenderer == null)
        {
            lineRenderer = GetComponent<LineRenderer>();
        }
        lineRenderer.positionCount = 2; // Line Renderer uses two points (start and end)

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
        // No specific input handling is required here since movement is driven by key states
    }

    void MoveLine()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            // Move straight down while holding the space bar
            if (transform.position.y > maxDepth)
            {
                transform.position += Vector3.down * dropSpeed * Time.deltaTime;
            }
        }
        else
        {
            // Move back towards the initial position when the space bar is released
            transform.position = Vector3.MoveTowards(transform.position, initialPosition, returnSpeed * Time.deltaTime);

            // Allow configurable horizontal movement with arrow keys while moving up
            float horizontal = Input.GetAxis("Horizontal");
            transform.position += Vector3.right * horizontal * horizontalSpeed * Time.deltaTime;

            // Release the fish if the line has returned to the initial position
            if (transform.position == initialPosition)
            {
                Hook hook = GetComponentInChildren<Hook>();
                if (hook != null)
                {
                    hook.ReleaseFish(); // Ensure the fish is removed when the line reaches the surface
                }
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

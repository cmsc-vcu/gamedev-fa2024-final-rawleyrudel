using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishAI : MonoBehaviour
{
    public float speed = 2f; // Speed of fish movement
    public float moveDistance = 3f; // Distance fish moves before turning around
    private Vector3 startPosition;
    private bool movingRight = true; // Fish starts by moving right

    void Start()
    {
        // Store the initial position of the fish
        startPosition = transform.position;
    }

    void Update()
    {
        MoveFish();
    }

    void MoveFish()
    {
        // Calculate the target position based on movement direction
        float targetX = movingRight ? startPosition.x + moveDistance : startPosition.x - moveDistance;

        // Move the fish in the current direction
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(targetX, transform.position.y, transform.position.z), speed * Time.deltaTime);

        // If the fish reaches the target position, change direction
        if (transform.position.x == targetX)
        {
            movingRight = !movingRight; // Flip the direction
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z); // Flip the sprite
        }
    }
}

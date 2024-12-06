using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    private FishingLine fishingLine;
    private GameObject caughtFish; // Reference to the caught fish
    private FishTallyManager fishTallyManager; // Reference to the FishTallyManager

    void Start()
    {
        // Reference to the FishingLine script on the parent object
        fishingLine = transform.parent.GetComponent<FishingLine>();

        // Find and reference the FishTallyManager in the scene
        fishTallyManager = FindObjectOfType<FishTallyManager>();
    }

    void Update()
    {
        // If a fish is caught, make the fish follow the hook's position
        if (caughtFish != null)
        {
            caughtFish.transform.position = transform.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the hook is ascending (space bar is released) and if it collides with a fish
        if (!Input.GetKey(KeyCode.Space) && collision.CompareTag("Fish") && caughtFish == null)
        {
            // Attach the fish to the hook
            caughtFish = collision.gameObject;

            // Stop the fish's AI behavior
            if (caughtFish.TryGetComponent<FishAI>(out FishAI fishAI))
            {
                Destroy(fishAI); // Destroy the FishAI component to stop its movement
            }

            // Notify the FishTallyManager to update the tally
            if (fishTallyManager != null)
            {
                fishTallyManager.AddFish();
            }

            Debug.Log("Fish collected!");
        }
    }

    public void ReleaseFish()
    {
        // Release the caught fish (if any)
        if (caughtFish != null)
        {
            // Destroy the caught fish GameObject
            Destroy(caughtFish);

            // Clear the reference
            caughtFish = null;
        }
    }

    // Method to check if a fish is caught
    public bool HasCaughtFish()
    {
        return caughtFish != null;
    }
}

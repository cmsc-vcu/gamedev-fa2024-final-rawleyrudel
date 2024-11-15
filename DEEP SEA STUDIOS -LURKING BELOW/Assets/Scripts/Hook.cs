using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    private FishingLine fishingLine;
    private GameObject caughtFish; // Reference to the caught fish

    void Start()
    {
        // Reference to the FishingLine script on the parent object
        fishingLine = transform.parent.GetComponent<FishingLine>();
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
        // Check if the hook is in "collecting" mode (i.e., moving up) and if it collides with a fish
        if (fishingLine.isReturning && collision.CompareTag("Fish") && caughtFish == null)
        {
            // Attach the fish to the hook
            caughtFish = collision.gameObject;
            Debug.Log("Fish collected!");
        }
    }

    public void ReleaseFish()
    {
        // Release the caught fish (if any)
        if (caughtFish != null)
        {
            // Destroy the caught fish from the scene
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    private FishingLine fishingLine;

    void Start()
    {
        // Reference to the FishingLine script on the parent object
        fishingLine = transform.parent.GetComponent<FishingLine>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the hook is in "collecting" mode (i.e., moving up) and if it collides with a fish
        if (fishingLine.isReturning && collision.CompareTag("Fish"))
        {
            // Collect the fish (e.g., disable it, increase score)
            collision.gameObject.SetActive(false);
            Debug.Log("Fish collected!");
        }
    }
}

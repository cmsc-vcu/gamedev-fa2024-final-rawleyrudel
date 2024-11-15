using UnityEngine;
using UnityEngine.UI;

public class FishTallyManager : MonoBehaviour
{
    public Text fishTallyText; // Reference to the UI Text component showing the fish tally
    private int fishCount = 0;

    private void Start()
    {
        UpdateFishTallyUI();
    }

    // Method to add a fish to the tally
    public void AddFish()
    {
        fishCount++;
        UpdateFishTallyUI();
    }

    // Method to reset the fish tally
    public void ResetFishTally()
    {
        fishCount = 0;
        UpdateFishTallyUI();
    }

    // Updates the fish tally on the UI
    private void UpdateFishTallyUI()
    {
        fishTallyText.text = "Fish Caught: " + fishCount.ToString();
    }
}

using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float levelDuration = 60f; // Duration of the level in seconds
    public Text timerText; // Reference to the UI Text to display the timer

    private float timeRemaining;
    private bool isTimerRunning = true;

    void Start()
    {
        timeRemaining = levelDuration;
        UpdateTimerUI();
    }

    void Update()
    {
        if (isTimerRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                UpdateTimerUI();
            }
            else
            {
                timeRemaining = 0;
                isTimerRunning = false;
                EndGame(); // Stop the game when the timer hits 0
            }
        }
    }

    void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        timerText.text = $"{minutes:00}:{seconds:00}";
    }

    void EndGame()
    {
        // Stop the game and trigger GameOver
        Debug.Log("Time's up! Game Over!");
        GameManager.Instance.TriggerGameOver();

        // Stop all fish activity
        FishSpawner spawner = FindObjectOfType<FishSpawner>();
        if (spawner != null)
        {
            spawner.StopSpawning();
        }

        FishAI[] fishes = FindObjectsOfType<FishAI>();
        foreach (FishAI fish in fishes)
        {
            fish.Deactivate(); // Stop fish movement
        }
    }
}

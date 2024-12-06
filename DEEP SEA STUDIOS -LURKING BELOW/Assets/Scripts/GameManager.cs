using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Singleton instance

    public GameObject gameOverCanvas; // Reference to the Game Over Canvas

    private void Awake()
    {
        // Ensure there is only one instance of GameManager
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void TriggerGameOver()
    {
        // Activate the Game Over Canvas
        if (gameOverCanvas != null)
        {
            gameOverCanvas.SetActive(true);
            Time.timeScale = 0; // Pause the game
        }
        else
        {
            Debug.LogWarning("GameOverCanvas not assigned in GameManager.");
        }
    }

}

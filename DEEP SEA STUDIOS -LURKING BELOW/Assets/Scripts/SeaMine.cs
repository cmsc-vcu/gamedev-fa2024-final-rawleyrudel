using UnityEngine;
using UnityEngine.SceneManagement; // For resetting the scene

public class SeaMine : MonoBehaviour
{
    public float swaySpeed = 1f; // Speed of the swaying motion
    public float swayRange = 1f; // Range of the swaying motion
    public GameObject gameOverCanvas; // Reference to the Game Over Canvas

    private float startingX;
    private bool gameOverTriggered = false;

    void Start()
    {
        startingX = transform.position.x;
    }

    void Update()
    {
        if (!gameOverTriggered)
        {
            float newX = startingX + Mathf.Sin(Time.time * swaySpeed) * swayRange;
            transform.position = new Vector3(newX, transform.position.y, transform.position.z);
        }
        else
        {
            CheckForReset(); // Check for "Space" press to reset the game
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hook") && !gameOverTriggered)
        {
            Debug.Log("Game Over! Sea mine hit the hook.");
            gameOverTriggered = true;
            TriggerGameOver(collision.transform);
        }
    }

    void TriggerGameOver(Transform hookTransform)
    {
        if (gameOverCanvas != null)
        {
            gameOverCanvas.transform.position = hookTransform.position + new Vector3(0, 1, 0); // Offset above the hook
            gameOverCanvas.transform.rotation = Quaternion.identity; // Reset rotation to upright
            gameOverCanvas.SetActive(true);
            Debug.Log("GameOverCanvas activated at position: " + gameOverCanvas.transform.position);
        }
        else
        {
            Debug.LogError("GameOverCanvas is not assigned in the Inspector!");
        }

        Time.timeScale = 0f; // Pause the game
    }

    void CheckForReset()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ResetGame();
        }
    }

    void ResetGame()
    {
        Time.timeScale = 1f; // Resume the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the current scene
    }
}

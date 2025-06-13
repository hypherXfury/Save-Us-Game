using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverPanel;
    public static GameManager Instance { get; private set; }
    public TextMeshProUGUI scoreText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {

        // Initialize the game state
        Time.timeScale = 1f; // Ensure the game is running
        gameOverPanel.SetActive(false); // Hide the game over panel at the start
    }
    void Update()
    {
        if (gameOverPanel == null || scoreText == null)
        {
            // Log an error if gameOverPanel or scoreText is not assigned
            Debug.LogError("GameManager: gameOverPanel or scoreText is not assigned.");
            return; // Exit if either is not assigned
        }
        // Update the score text
        scoreText.text = "Score: " + Time.timeSinceLevelLoad.ToString("F2"); // Display score as time since level load
    }
    public void GameOver()
    {
        Time.timeScale = 0f; // Pause the game
        gameOverPanel.SetActive(true); // Show the game over panel
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f; // Ensure the game is running
        gameOverPanel.SetActive(false); // Hide the game over panel at the start
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("GameScene"); // Replace with your main menu scene name
    }
}

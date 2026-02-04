using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public PlayerController player;
    
    [Header("Score UI")]
    public TextMeshProUGUI scoreText;

    [Header("Health UI")]
    public Image heartDisplay;
    public Sprite[] heartSprites;

    [Header("Panels")]
    public GameObject pausePanel;
    public GameObject gameOverPanel;
    public TextMeshProUGUI finalScoreValueText;

    private bool isGameOver = false;

    void Update()
    {
        if (player != null && !isGameOver)
        {
            scoreText.text = Mathf.FloorToInt(player.score).ToString();
            UpdateHeartUI(player.health);
        }
    }

    public void UpdateHeartUI(int currentHealth)
    {
        int index = 3 - currentHealth;
        index = Mathf.Clamp(index, 0, 3);
        heartDisplay.sprite = heartSprites[index];
    }

    public void OpenSettings()
    {
        Time.timeScale = 0f;
        pausePanel.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
    }

    public void TriggerGameOver(float finalScore)
    {
        isGameOver = true;
        UpdateHeartUI(0);
        gameOverPanel.SetActive(true);
        finalScoreValueText.text = Mathf.FloorToInt(finalScore).ToString();
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
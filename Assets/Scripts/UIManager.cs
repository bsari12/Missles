using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverScoreText;
    public TextMeshProUGUI highScoreText;
    public GameObject gameOverPanel;
    public GameObject pausePanel;
    public Image healthImage;
    public Sprite[] healthSprites;
    public SceneFader sceneFader;

    public void UpdateScoreUI(float score)
    {
        if (scoreText != null)
        {
            scoreText.text = Mathf.FloorToInt(score).ToString();
        }
    }

    public void UpdateHealthUI(int currentHealth)
    {
        if (currentHealth >= 0 && currentHealth < healthSprites.Length)
        {
            healthImage.sprite = healthSprites[currentHealth];
        }
    }

    public void TriggerGameOver(float finalScore)
    {
        int scoreToSave = Mathf.FloorToInt(finalScore);
        gameOverScoreText.text = scoreToSave.ToString();
        CheckHighScore(scoreToSave);
        gameOverPanel.SetActive(true);
        gameOverPanel.transform.SetAsLastSibling();
    }

    private void CheckHighScore(int currentScore)
    {
        int savedHighScore = PlayerPrefs.GetInt("HighScore", 0);
        if (currentScore > savedHighScore)
        {
            savedHighScore = currentScore;
            PlayerPrefs.SetInt("HighScore", savedHighScore);
            PlayerPrefs.Save();
        }
        highScoreText.text = "High Score\n\n" + savedHighScore.ToString();
    }

    public void PauseGame()
    {
        pausePanel.SetActive(true);
        pausePanel.transform.SetAsLastSibling();
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        if (sceneFader != null) sceneFader.FadeTo(SceneManager.GetActiveScene().name);
        else SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToMainMenu(string menuSceneName)
    {
        Time.timeScale = 1f;
        if (sceneFader != null) sceneFader.FadeTo(menuSceneName);
        else SceneManager.LoadScene(menuSceneName);
    }
}
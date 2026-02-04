using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public SceneFader sceneFader;
    public string gameSceneName = "GameScene";

    void Start()
    {
        Application.targetFrameRate = 60;
    }
    public void PlayGame()
    {
        sceneFader.FadeTo(gameSceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
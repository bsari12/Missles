using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public PlayerController player;
    public TextMeshProUGUI scoreText;

    void Update()
    {
        if (player != null && scoreText != null)
        {
            scoreText.text = "SCORE: " + Mathf.FloorToInt(player.score).ToString();
        }
    }
}
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverController : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI scoreText;

    void Start()
    {
        scoreText.text = "FINAL SCORE:\n" + GameManager.finalScore;
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("GameScene");
    }
}
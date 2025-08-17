using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject gamePanel;
    
    [Header("Text Elements")]
    public TextMeshProUGUI currentMovieText;
    public TextMeshProUGUI nextMovieText;
    public TextMeshProUGUI scoreText;

    public void ShowQuestionUI(Movie current, Movie next, int score, GameSettings.GameMode mode)
    {
        UpdateGameUI(current, next, score, mode);
        gamePanel.SetActive(true);
    }

    public void HideQuestionUI()
    {
        gamePanel.SetActive(false);
    }

    public void UpdateGameUI(Movie current, Movie next, int score, GameSettings.GameMode mode)
    {
        double currentGross = (mode == GameSettings.GameMode.Worldwide) ? current.worldwideGross : current.domesticGross;
        string modeLabel = (mode == GameSettings.GameMode.Worldwide) ? "Worldwide" : "Domestic";

        currentMovieText.text = $"{current.title} ({current.year})\n{FormatGross(currentGross)}";
        nextMovieText.text = $"{next.title} ({next.year})\n???";
        scoreText.text = $"STREAK: {score} ({modeLabel})";
    }

    public void RevealNextMovie(Movie next, GameSettings.GameMode mode)
    {
        double nextGross = (mode == GameSettings.GameMode.Worldwide) ? next.worldwideGross : next.domesticGross;
        nextMovieText.text = $"{next.title} ({next.year})\n{FormatGross(nextGross)}";
    }

    public string FormatGross(double gross)
    {
        if (gross >= 1_000_000_000)
        {
            return $"${(gross / 1_000_000_000.0):F3} billion";
        }
        if (gross >= 1_000_000)
        {
            return $"${(gross / 1_000_000.0):F3} million";
        }
        return $"${gross:N0}";
    }
}
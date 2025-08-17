using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public GameSettings gameSettings;

    public void StartGame(int mode)
    {
        // Set the selected mode in our persistent GameSettings object
        // 0 = Worldwide, 1 = Domestic
        gameSettings.selectedGameMode = (GameSettings.GameMode)mode;
        SceneManager.LoadScene("GameScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
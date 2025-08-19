using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static int finalScore;
    private enum GameState { Walking, WaitingForInput, Transitioning }
    private GameState currentState;

    [Header("References")]
    public LevelGenerator levelGenerator;
    public PlayerAnimationController playerAnim;
    public UIManager uiManager;
    public GameSettings gameSettings;
    public MovieDatabase movieDB;
    public Camera mainCamera;

    private GameObject currentStairUnit;
    private Movie currentMovie;
    private Movie nextMovie;
    private int score;
    private GameSettings.GameMode currentGameMode;

    [Header("Timer Settings")]
    public Slider timerBar;
    public float initialTime = 10f;
    public float timeDecreasePerPoint = 0.1f;
    public float minimumTime = 2f;
    
    private Coroutine timerCoroutine;

    void Start()
    {
        currentStairUnit = levelGenerator.GetStartingPiece();
        if (currentStairUnit != null)
        {
             transform.position = currentStairUnit.transform.Find("StartPoint").position;
        }

        currentGameMode = gameSettings.selectedGameMode;
        playerAnim.ResetToIdle();
        score = 0;
        currentMovie = movieDB.GetRandomMovie();
        nextMovie = movieDB.GetRandomMovie(currentMovie);

        ChangeState(GameState.Walking);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (currentState == GameState.Walking && other.CompareTag("IntersectionTrigger"))
        {
            ChangeState(GameState.WaitingForInput);
        }
    }

    void ChangeState(GameState newState)
    {
        currentState = newState;
        switch (currentState)
        {
            case GameState.Walking:
                if (timerCoroutine != null) StopCoroutine(timerCoroutine); // Stop timer while walking
                timerBar.gameObject.SetActive(false);
                uiManager.HideQuestionUI();
                Transform intersection = currentStairUnit.transform.Find("IntersectionTrigger");
                StartCoroutine(playerAnim.WalkTo(intersection.position));
                break;

            case GameState.WaitingForInput:
                if (timerCoroutine != null) StopCoroutine(timerCoroutine); // Stop any old timer
                timerBar.gameObject.SetActive(true);
                timerCoroutine = StartCoroutine(StartTimer());
                playerAnim.ResetToIdle();
                uiManager.ShowQuestionUI(currentMovie, nextMovie, score, currentGameMode);
                break;
        }
    }
    
    public void PlayerGuessHigher() => PlayerGuess(true);
    public void PlayerGuessLower() => PlayerGuess(false);

    private void PlayerGuess(bool guessedHigher)
    {
        if (currentState != GameState.WaitingForInput) return;
        if (timerCoroutine != null) StopCoroutine(timerCoroutine);
        currentState = GameState.Transitioning;
        uiManager.RevealNextMovie(nextMovie, currentGameMode);
        bool wasCorrect = CheckIfGuessWasCorrect(guessedHigher);
        StartCoroutine(HandleRoundEnd(wasCorrect, guessedHigher));
    }
    
    private IEnumerator HandleRoundEnd(bool wasCorrect, bool guessedHigher)
    {
        if (wasCorrect)
        {
            Transform startFrom = guessedHigher
                ? currentStairUnit.transform.Find("UpEndPoint")
                : currentStairUnit.transform.Find("DownEndPoint");

            GameObject nextStairUnit = levelGenerator.GenerateNextPiece(startFrom);
            Transform nextStartPoint = nextStairUnit.transform.Find("StartPoint");

            playerAnim.AnimateWalk();
            yield return playerAnim.MovePlayerTo(nextStartPoint.position);

            mainCamera.transform.position = new Vector3(nextStartPoint.position.x, nextStartPoint.position.y, mainCamera.transform.position.z);
            Destroy(currentStairUnit);
            currentStairUnit = nextStairUnit;

            score++;
            currentMovie = nextMovie;
            nextMovie = movieDB.GetRandomMovie(currentMovie);
            ChangeState(GameState.Walking);
        }
        else
        {
            playerAnim.PlayDeath();
            yield return new WaitForSeconds(1.5f);
            finalScore = score;
            SceneManager.LoadScene("GameOverScene");
        }
    }
    
    private bool CheckIfGuessWasCorrect(bool guessedHigher)
    {
        double currentGross = (currentGameMode == GameSettings.GameMode.Worldwide) ? currentMovie.worldwideGross : currentMovie.domesticGross;
        double nextGross = (currentGameMode == GameSettings.GameMode.Worldwide) ? nextMovie.worldwideGross : nextMovie.domesticGross;
        return (guessedHigher && nextGross > currentGross) || (!guessedHigher && nextGross < currentGross);
    }

    private IEnumerator StartTimer()
    {
        float timeForThisRound = Mathf.Max(minimumTime, initialTime - (score * timeDecreasePerPoint));
        float timeRemaining = timeForThisRound;

        while (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            timerBar.value = timeRemaining / timeForThisRound;
            yield return null;
        }

        // If the timer runs out, the player loses
        StartCoroutine(HandleRoundEnd(false, false));
    }
}
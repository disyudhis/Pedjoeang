using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private Image timerImage;
    [SerializeField] private float gameTime;
    private float sliderCurrentFillAmount = 1f;

    [Header("Score Components")]
    [SerializeField] private TextMeshProUGUI scoreText;

    [Header("Game Over Components")]
    [SerializeField] private GameObject gameOverScreen;

    [Header("High Score Components")]
    [SerializeField] private TextMeshProUGUI highScoreText;

    private int highScore;

    [Header("Gameplay audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] gameplayAudio;

    private int playerScore;

    public enum GameState
    {
        waiting,
        Playing,
        GameOver
    }

    public static GameState currentGameStatus;

    private void Awake()
    {
        currentGameStatus = GameState.waiting;
        if (PlayerPrefs.HasKey("HighScore"))
        {
            highScoreText.text = PlayerPrefs.GetInt("HighScore").ToString();
        }
    }

    private void Update()
    {
        if (currentGameStatus == GameState.Playing)
            AdjustTimer();
    }

    private void AdjustTimer()
    {
        timerImage.fillAmount = sliderCurrentFillAmount - (Time.deltaTime / gameTime);

        sliderCurrentFillAmount = timerImage.fillAmount;

        if (sliderCurrentFillAmount <= 0f)
        {
            GameOver();
        }
    }

    public void UpdatePlayerScore(int enemyHitPoints)
    {
        if (currentGameStatus != GameState.Playing)
            return;

        playerScore += enemyHitPoints;
        scoreText.text = playerScore.ToString();
    }

    public void StartGame()
    {
        currentGameStatus = GameState.Playing;
        PlayGameAudio(gameplayAudio[1], true);
    }

    private void GameOver()
    {
        currentGameStatus = GameState.GameOver;

        //show the game over screen
        gameOverScreen.SetActive(true);

        //check the high score ...
        if (playerScore > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", playerScore);
            highScoreText.text = playerScore.ToString();
        }

        // change the audio
        PlayGameAudio(gameplayAudio[2], false);
    }

    public void ResetGame()
    {
        currentGameStatus = GameState.waiting;

        // put timer to 1
        sliderCurrentFillAmount = 1f;
        timerImage.fillAmount = 1f;

        //reset the score
        playerScore = 0;
        scoreText.text = "0";

        //play intro music
        PlayGameAudio(gameplayAudio[0], true);
    }

    private void PlayGameAudio(AudioClip clipToPlay, bool shouldLoop)
    {
        audioSource.clip = clipToPlay;
        audioSource.loop = shouldLoop;
        audioSource.Play();
    }
}
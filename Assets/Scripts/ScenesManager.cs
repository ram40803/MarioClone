using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public GameObject gameOverPanel;
    public GameObject gamePosePanel;
    public GameObject gameCompletePanel;

    public static bool isGameOver;
    public static bool isGameComplete;

    private int currentSceneIndex;

    private AudioSource audioSource;
    public AudioClip gameOverSound;
    public AudioClip gameCompleteSound;

    private void Start()
    {
        ScenesManager.isGameOver = false;
        ScenesManager.isGameComplete = false;

        audioSource = GetComponent<AudioSource>();

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }

        if (gamePosePanel != null)
        {
            gamePosePanel.SetActive(false);
        }

        if (gameCompletePanel != null)
        {
            gameCompletePanel.SetActive(false);
        }

        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void GameOver()
    {
        if (isGameOver || isGameComplete) return;
        audioSource.PlayOneShot(gameOverSound);
        ScenesManager.isGameOver = true;
        gameOverPanel.SetActive(true);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void PoseGame()
    {
        if (isGameOver || isGameComplete) return;
        gamePosePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        gamePosePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void GameComplete()
    {
        if (isGameOver || isGameComplete) return;
        audioSource.PlayOneShot(gameCompleteSound);
        ScenesManager.isGameComplete = true;
        gameCompletePanel.SetActive(true);
    }


}

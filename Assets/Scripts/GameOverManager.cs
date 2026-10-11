using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverPanel;

    [Header("Game Over Sound")]
    public AudioSource gameOverSound;

    [Header("Try Again Click Sound")]
    public AudioSource tryAgainClickSound;

    [Header("Home Click Sound")]
    public AudioSource homeClickSound;

    private bool isLoading = false;

    void Start()
    {
        gameOverPanel.SetActive(false);
    }

    public void ShowGameOver()
    {
        gameOverPanel.SetActive(true);

        // Hide mobile controls
        GameObject mobileControls = GameObject.Find("MobileControlsCanvas");

        if (mobileControls != null)
            mobileControls.SetActive(false);

        // Play Game Over sound
        if (gameOverSound != null && gameOverSound.clip != null)
            gameOverSound.PlayOneShot(gameOverSound.clip);

        // Pause the game
        Time.timeScale = 0f;
    }

    public void TryAgain()
    {
        if (isLoading)
            return;

        isLoading = true;

        StartCoroutine(TryAgainRoutine());
    }

    IEnumerator TryAgainRoutine()
    {
        // Resume time
        Time.timeScale = 1f;

        // Play Try Again sound
        if (tryAgainClickSound != null && tryAgainClickSound.clip != null)
        {
            tryAgainClickSound.PlayOneShot(tryAgainClickSound.clip);

            yield return new WaitForSecondsRealtime(
                tryAgainClickSound.clip.length
            );
        }

        // Reload current scene AFTER sound finishes
        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }

    public void Home()
    {
        if (isLoading)
            return;

        isLoading = true;

        StartCoroutine(HomeRoutine());
    }

    IEnumerator HomeRoutine()
    {
        // Resume time
        Time.timeScale = 1f;

        // Play Home sound
        if (homeClickSound != null && homeClickSound.clip != null)
        {
            homeClickSound.PlayOneShot(homeClickSound.clip);

            yield return new WaitForSecondsRealtime(
                homeClickSound.clip.length
            );
        }

        // Load Home scene AFTER sound finishes
        SceneManager.LoadScene("Home");
    }
}
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int maxLives = 3;

    private int currentLives;

    [Header("Life UI")]
    public GameObject life1;
    public GameObject life2;
    public GameObject life3;

    [Header("Game Over")]
    public GameOverManager gameOverManager;

    [Header("Hit Sound")]
    public AudioSource playerHitSound;

    [Header("Invincibility")]
    public float invincibilityDuration = 5f;

    private bool gameOver = false;
    private bool isInvincible = false;

    void Start()
    {
        currentLives = maxLives;
    }

    public void TakeDamage()
    {
        if (gameOver)
            return;

        // Ignore damage while invincible
        if (isInvincible)
            return;

        currentLives--;

        // Play hit sound ONLY if the player still has lives remaining
        if (currentLives > 0)
        {
            if (playerHitSound != null && playerHitSound.clip != null)
                playerHitSound.PlayOneShot(playerHitSound.clip);

            // Start 5 second invincibility
            StartCoroutine(InvincibilityTimer());
        }

        // Remove lives from RIGHT to LEFT
        if (currentLives == 2)
            life3.SetActive(false);

        else if (currentLives == 1)
            life2.SetActive(false);

        else if (currentLives <= 0)
        {
            life1.SetActive(false);
            GameOver();
        }
    }

    IEnumerator InvincibilityTimer()
    {
        isInvincible = true;

        yield return new WaitForSeconds(invincibilityDuration);

        isInvincible = false;
    }

    void GameOver()
    {
        if (gameOver)
            return;

        gameOver = true;

        if (gameOverManager != null)
            gameOverManager.ShowGameOver();
    }
}
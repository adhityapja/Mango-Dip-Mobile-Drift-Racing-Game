
using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance;

    [Header("Coin Counter UI")]
    public TextMeshProUGUI coinText; // Assign CoinNumber here

    public GameObject winCanvas;

    [Header("Coin Sound")]
    public AudioSource coinSound;

    public int totalCoins = 10;

    private int collectedCoins = 0;

    void Awake()
    {
        Instance = this;
        UpdateUI();

        if (winCanvas != null)
            winCanvas.SetActive(false);
    }

    public void CollectCoin()
    {
        collectedCoins++;

        if (coinSound != null && coinSound.clip != null)
            coinSound.PlayOneShot(coinSound.clip);

        UpdateUI();

        if (collectedCoins >= totalCoins)
            WinGame();
    }

    void UpdateUI()
    {
        if (coinText != null)
            coinText.text = collectedCoins.ToString();
    }

    void WinGame()
    {
        if (winCanvas != null)
        {
            winCanvas.SetActive(true);

            GameObject lifeCanvas = GameObject.Find("LifeCanvas");
            if (lifeCanvas != null)
                lifeCanvas.SetActive(false);

            GameObject coinCanvas = GameObject.Find("CoinCanvas");
            if (coinCanvas != null)
                coinCanvas.SetActive(false);

            Time.timeScale = 0f;
        }
    }
}

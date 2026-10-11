using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeManager : MonoBehaviour
{
    [Header("Play Button Sound")]
    public AudioSource playClickSound;

    private AsyncOperation sceneLoadOperation;

    void Start()
    {
        // Start loading lvl5 in the background
        sceneLoadOperation = SceneManager.LoadSceneAsync("lvl5");

        // Do NOT switch scenes yet
        sceneLoadOperation.allowSceneActivation = false;
    }

    public void PlayGame()
    {
        StartCoroutine(PlayGameRoutine());
    }

    IEnumerator PlayGameRoutine()
    {
        // Play Play button SFX
        if (playClickSound != null && playClickSound.clip != null)
        {
            playClickSound.PlayOneShot(playClickSound.clip);

            // Wait for the sound to finish
            yield return new WaitForSecondsRealtime(
                playClickSound.clip.length
            );
        }

        Time.timeScale = 1f;

        // Activate the already-loaded scene
        sceneLoadOperation.allowSceneActivation = true;
    }
}
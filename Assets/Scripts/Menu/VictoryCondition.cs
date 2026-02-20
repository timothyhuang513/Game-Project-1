using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public GameObject victoryMenuUI;
    public int enemiesDefeated = 0;
    public int enemiesToWin = 20;

    private bool isGameOver = false;

    void Update()
    {
        if (!isGameOver && enemiesDefeated >= enemiesToWin)
        {
            Victory();
        }

        if (isGameOver && Keyboard.current.qKey.wasPressedThisFrame)
        {
            QuitGame();
        }

        if (isGameOver && Keyboard.current.rKey.wasPressedThisFrame)
        {
            RestartGame();
        }
    }

    public void EnemyDefeated()
    {
        enemiesDefeated++;
    }

    void Victory()
    {
        isGameOver = true;
        victoryMenuUI.SetActive(true);
        Time.timeScale = 0f;
    }

    void RestartGame()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
        );
    }

    void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
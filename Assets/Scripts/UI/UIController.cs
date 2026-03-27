using UnityEngine;

public class UIController : MonoBehaviour
{
    public Player player;
    public GameObject searchingPanel;
    public GameObject cameraPanel;
    public GameObject pausePanel;
    public GameObject gameOverPanel;

    public static UIController instance;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        searchingPanel.SetActive(true);
        cameraPanel.SetActive(false);
        pausePanel.SetActive(false);
        gameOverPanel.SetActive(false);
    }

    public void ToggleMode()
    {
        if(player.isSearching)
        {
            searchingPanel.SetActive(true);
            cameraPanel.SetActive(false);
        }
        else if(player.cameraMode)
        {
            searchingPanel.SetActive(false);
            cameraPanel.SetActive(true);
        }
    }

    public void TogglePause()
    {
        if(GameManager.instance.currentState == GameManager.GameState.paused)
        {
            pausePanel.SetActive(true);
        }
        else if(GameManager.instance.currentState == GameManager.GameState.playing)
        {
            pausePanel.SetActive(false);
        }
    }

    public void GameOver()
    {
        if(GameManager.instance.currentState == GameManager.GameState.gameOver)
        {
            searchingPanel.SetActive(false);
            cameraPanel.SetActive(false);
            GameOverController.instance.GameOverText();
            gameOverPanel.SetActive(true);
        }
    }
}

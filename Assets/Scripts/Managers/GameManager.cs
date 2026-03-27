using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public enum GameState
    {
        paused,
        playing,
        menu,
        gameOver,
        gameWin
    }
    public Player player;

    public static GameManager instance;

    public GameState currentState = GameState.menu;

void OnEnable()
{
    SceneManager.sceneLoaded += OnSceneLoaded;
}

void OnDisable()
{
    SceneManager.sceneLoaded -= OnSceneLoaded;
}

void OnSceneLoaded(Scene scene, LoadSceneMode mode)
{
    if(scene.name == "Forest")
    {
        // Assign references
        player = FindObjectOfType<Player>();
        UIController.instance = FindObjectOfType<UIController>();
        CameraLook.instance = FindObjectOfType<CameraLook>();

        // Reinitialize player/gameplay values
        player.isSearching = true;
        player.cameraMode = false;

        Physics. gravity = new Vector3(0f, -20f, 0f); 

        // Reinitialize camera/film values
        var photoCam = FindObjectOfType<PhotoCamera>();
        if(photoCam != null)
        {
            photoCam.filmCount = photoCam.maxFilmCount = 5; // reset for level start
            photoCam.cryptidPictures = 0;
        }

        // Set state to playing
        currentState = GameState.playing;
        Time.timeScale = 1f;
    }
}

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    } 

    public void Pause()
    {
        Time.timeScale = 0;
        currentState = GameState.paused;
        CameraLook.instance.SetLookEnable(false);
    }

    public void Resume()
    {
        Time.timeScale = 1;
        currentState = GameState.playing;
        CameraLook.instance.SetLookEnable(true);
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1;
        currentState = GameState.menu;
        SceneManager.LoadScene("MainMenu");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void GameOver()
    {
        currentState = GameState.gameOver;
        UIController.instance.GameOver();
        CameraLook.instance.SetLookEnable(false);
    }
}

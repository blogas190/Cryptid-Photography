using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void OnPlay()
    {
        SceneManager.LoadScene("Forest");
        GameManager.instance.currentState = GameManager.GameState.playing;
    }

    public void OnQuit()
    {
        Application.Quit();
    }
}

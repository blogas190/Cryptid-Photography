using UnityEngine;

public class PauseUIController : MonoBehaviour
{

    public void OnResume()
    {
        GameManager.instance.Resume();
        UIController.instance.TogglePause();
    }

    public void OnMainMenu()
    {
        GameManager.instance.LoadMainMenu();
    }

    public void OnQuit()
    {
        GameManager.instance.Quit();
    }
}

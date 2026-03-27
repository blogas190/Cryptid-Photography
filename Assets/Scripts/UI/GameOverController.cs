using TMPro;
using UnityEngine;

public class GameOverController : MonoBehaviour
{
    public static GameOverController instance;

    public TextMeshProUGUI gameOverScoreText;

    public PhotoCamera photoCamera;

    void Awake()
    {
        instance = this;
    }

    public void GameOverText()
    {
        gameOverScoreText.SetText("You took " + photoCamera.cryptidPictures + " pictures of the Cryptid!");
    }
}

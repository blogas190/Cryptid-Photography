using TMPro;
using UnityEngine;

public class CameraUIController : MonoBehaviour
{
    public TextMeshProUGUI filmCountText;
    public PhotoCamera cam;

    private int oldCount;

    void Start()
    {
        filmCountText.SetText(cam.filmCount + "/" + cam.maxFilmCount);
        oldCount = cam.filmCount;
    }

    public void Update()
    {
        if(oldCount > cam.filmCount)
        {
            oldCount = cam.filmCount;
            filmCountText.SetText(cam.filmCount + "/" + cam.maxFilmCount);
        }
        else
        {
            return;
        }
    }
}

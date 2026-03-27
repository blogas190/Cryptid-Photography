using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    private Player player;
    private PhotoCamera photoCamera;

    void Awake()
    {
        player = GetComponent<Player>();
        photoCamera = GetComponentInChildren<PhotoCamera>(); // ensures child objects are found
    }
    public void Move(InputAction.CallbackContext context)
    {
        if(GameManager.instance.currentState == GameManager.GameState.playing)
        {
            Vector2 input = context.ReadValue<Vector2>();

            player.direction = new Vector3(input.x, 0f, input.y);
            player.isMoving = input.sqrMagnitude > 0.01f;   
        }
    }

    public void ToggleCamera(InputAction.CallbackContext context)
    {
        if(context.performed && player.isSearching && GameManager.instance.currentState == GameManager.GameState.playing)
        {
            player.isSearching = false;
            player.cameraMode = true;
            UIController.instance.ToggleMode();
        }
        else if(context.performed && player.cameraMode && GameManager.instance.currentState == GameManager.GameState.playing)
        {
            player.isSearching = true;
            player.cameraMode = false;
            UIController.instance.ToggleMode();
        }
    }

    public void TakePhoto(InputAction.CallbackContext context)
    {
        if(context.performed && player.cameraMode && GameManager.instance.currentState == GameManager.GameState.playing)
        {
            photoCamera.ShootPhoto();
        }
    }

    public void TogglePause(InputAction.CallbackContext context)
    {
        if(context.performed && GameManager.instance.currentState != GameManager.GameState.menu)
        {
            if(GameManager.instance.currentState == GameManager.GameState.playing)
            {
                GameManager.instance.Pause();
                UIController.instance.TogglePause();
            }
            else if(GameManager.instance.currentState == GameManager.GameState.paused)
            {
                GameManager.instance.Resume();
                UIController.instance.TogglePause();
            }
        }
    }
}

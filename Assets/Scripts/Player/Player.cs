using UnityEngine;

public class Player : MonoBehaviour
{
    [HideInInspector] public Vector3 direction;
    [HideInInspector] public bool isMoving = false;
    [HideInInspector] public bool cameraMode = false;
    [HideInInspector] public bool isSearching = true;
    public Camera mainCamera;
}

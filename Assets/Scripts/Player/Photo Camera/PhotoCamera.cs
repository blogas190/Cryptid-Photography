using UnityEngine;

public class PhotoCamera : MonoBehaviour
{
    public float photoRange = 30f;
    public float photoFOV = 40f;
    public float shotCooldown = 200f;
    public LayerMask cryptidLayer;
    public LayerMask obstacleLayer;
    public AudioSource photoSound;

    public int filmCount = 5;
    [HideInInspector]public int maxFilmCount;

    public int cryptidPictures;

    float lastShotTime;

    private Player player;
    private Camera cam;

    void Awake()
    {
        player = GetComponentInParent<Player>(); // use parent to ensure it finds Player
        cam = player.mainCamera;
    }

    public void ShootPhoto()
    {
        if(Time.time - lastShotTime < shotCooldown)
        {
            return;
        }
        Collider[] hits = Physics.OverlapSphere(cam.transform.position, photoRange, cryptidLayer);
        lastShotTime = Time.time;
        photoSound.time = 0.5f;
        photoSound.Play();
        if(filmCount <= 1)
        {
            GameManager.instance.GameOver();
        }
        filmCount -= 1;

        foreach(Collider hit in hits)
        {
            if(!hit.CompareTag("Cryptid"))
            continue;

            Vector3 dir = (hit.transform.position - cam.transform.position).normalized;
            float angle = Vector3.Angle(cam.transform.forward, dir);

            if(angle > photoFOV * 0.5f)
            {
                continue;
            }

            if(Physics.Raycast(cam.transform.position, dir, out RaycastHit rayHit, photoRange, obstacleLayer | cryptidLayer))
            {
                if(rayHit.collider != hit)
                {
                    continue;
                }
            }

            cryptidPictures++;
        }
    }


}

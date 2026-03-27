using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    private Player player;
    private Rigidbody p_rb;
    private Transform cam;

    public AudioSource walkSounds;

    [Header("Variables")]
    public float speed = 15;
    void Start()
    {
        player = GetComponent<Player>();
        p_rb = GetComponent<Rigidbody>();
        cam = Camera.main.transform;

        p_rb.freezeRotation = true;
    }

    void FixedUpdate()
    {
        //Walking logic
        //------------------------------------
        Vector3 camForward = cam.forward;
        Vector3 camRight = cam.right;

        // Flatten (ignore vertical tilt)
        camForward.y = 0f;
        camRight.y = 0f;

        camForward.Normalize();
        camRight.Normalize();

        // Build movement direction
        Vector3 moveDir = camForward * player.direction.z + camRight * player.direction.x;

        Vector3 velocity = p_rb.linearVelocity;
        velocity.x = moveDir.x * speed;
        velocity.z = moveDir.z *speed;
        if(!walkSounds.isPlaying)
        {
            walkSounds.loop = true;
            walkSounds.Play();
        }

        p_rb.linearVelocity = velocity;
        
        if (!player.isMoving || player.cameraMode) 
        {
            if(walkSounds.isPlaying)
            {
                walkSounds.Stop();
                walkSounds.loop = false;
            }
            Vector3 vel = p_rb.linearVelocity;
            vel.x = 0f;
            vel.z = 0f;
            p_rb.linearVelocity = vel;
        }

        //------------------------------------
    }
}

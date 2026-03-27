using UnityEngine;

public class Animal : MonoBehaviour
{
    public float walkSpeed = 6f;
    public float fleeSpeed = 12f;
    public float activeDistance = 20f;
    public float decisionTime = 4f;
    public float fleeTime = 5f;

    [HideInInspector]public bool isWandering;
    [HideInInspector]public bool isIdle;
    [HideInInspector]public bool isFleeing;

    [HideInInspector]public Transform player;
    [HideInInspector]public Rigidbody rb;

    [HideInInspector]public Vector3 targetDir;
    [HideInInspector]public Vector3 lastPosition;
    [HideInInspector]public float timer;
    public float stuckTimer;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody>();

        rb.freezeRotation = true;

        lastPosition = transform.position;

        StartWandering();

        timer = Random.Range(0f, decisionTime);

    }

    public virtual void FixedUpdate()
    {
        float distToPlayer = Vector3.Distance(transform.position, player.position);

        Vector3 velocity = rb.linearVelocity;

        if(distToPlayer < activeDistance && CanActivate())
        {
            Activate();
        }

        if(isFleeing)
        {
            Vector3 away = (transform.position - player.position).normalized;
            velocity.x = away.x * fleeSpeed;
            velocity.z = away.z * fleeSpeed;

            timer -= Time.fixedDeltaTime;

            if(timer<= 0f)
            {
                StartWandering();
            }
        }
        else if(isWandering)
        {
            timer -= Time.fixedDeltaTime;

            velocity.x = targetDir.x * walkSpeed;
            velocity.z = targetDir.z * walkSpeed;

            if(timer <= 0f)
            {
                StartIdle();
            }
        }
        else if(isIdle)
        {
            timer -= Time.fixedDeltaTime;

            velocity.x = 0f;
            velocity.z = 0f;

            if(timer <= 0f)
            {
                StartWandering();
            }
        }

        rb.linearVelocity = velocity;

        Vector3 lookDir = new Vector3(velocity.x, 0, velocity.z);
        if(lookDir.sqrMagnitude > 0.1f)
        {
            transform.rotation = Quaternion.LookRotation(lookDir);
        }

        float moveDist = Vector3.Distance(transform.position, lastPosition);

        if((isWandering || isFleeing) && moveDist < 0.1f)
        {
            stuckTimer += Time.fixedDeltaTime;

            if(stuckTimer >= 0.5f)
            {
                targetDir = Quaternion.Euler(0, Random.Range(90f, 180f), 0) * targetDir;
                targetDir = targetDir.normalized;
                stuckTimer = 0f;
            }
        }
        else
        {
            stuckTimer = 0f;
        }

        lastPosition = transform.position;
    }

    public void PickNewDirection()
    {
        Vector2 rand = Random.insideUnitCircle.normalized;
        targetDir = new Vector3(rand.x, 0f, rand.y);
    }

    public virtual void StartWandering()
    {
        isIdle = false;
        isWandering = true;
        isFleeing = false;

        PickNewDirection();

        timer = decisionTime;
    }

    public virtual void StartIdle()
    {
        isIdle = true;
        isWandering = false;
        isFleeing = false;

        timer = decisionTime;
    }

    public virtual void Activate()
    {
        isWandering = false;
        isIdle = false;
        isFleeing = true;

        //original is for fleeing
        timer = fleeTime;
    }

    protected virtual bool CanActivate()
    {
        return true;
    }
}

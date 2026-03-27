using System.Collections;
using UnityEngine;

public class TheWatcher : Animal
{
    private bool isWatching = false;
    public float stareTime = 6f;
    public float eyeMaxIntensity = 1.60f;
    public float eyeGlowSpeed = 0.6f;

    public Light[] eyeLights;
    Coroutine routine;
    public override void Activate()
    {
        isWandering = false;
        isIdle = false;
        isFleeing = false;
        isWatching = true;

        timer = stareTime;

        if(routine != null)
        {
            StopCoroutine(routine);
        }

        routine = StartCoroutine(EyeGlow());
    }

    public override void FixedUpdate()
    {
        bool playerInRange = Vector3.Distance(transform.position, player.position) < activeDistance;

        if (isWatching)
        {
            Vector3 dir = player.position - transform.position;
            dir.y = 0f;

            if (dir.sqrMagnitude > 0.01f)
                transform.rotation = Quaternion.LookRotation(dir);

            rb.linearVelocity = Vector3.zero;

            if (playerInRange)
            {
                timer = stareTime; // freeze timer
            }
            else
            {
                timer -= Time.fixedDeltaTime; // countdown
                if (timer <= 0f)
                {
                    isWatching = false;
                    SetEyeIntensity(0f, false);
                    StartWandering();
                }
            }

            return;
        }

        base.FixedUpdate();
    }

    public IEnumerator EyeGlow()
    {

        yield return new WaitForSeconds(2f);   

        while (isWatching && eyeLights[0].intensity < eyeMaxIntensity)
        {
            ChangeEyeIntensity(eyeGlowSpeed * Time.deltaTime);
            yield return null;
        }

        while (isWatching)
        {
            yield return null;
        }

    }

    private void SetEyeIntensity(float value, bool enabled)
    {
        foreach (Light l in eyeLights)
        {
            l.intensity = value;
            l.enabled = enabled;
        }
    }

    private void ChangeEyeIntensity(float delta)
    {
        foreach (Light l in eyeLights)
        {
            l.enabled = true;
            l.intensity = Mathf.Clamp(l.intensity + delta, 0f, eyeMaxIntensity);
        }
    }

    protected override bool CanActivate()
    {
        return !isWatching;
    }
}

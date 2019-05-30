using UnityEngine;

public enum NPCDriverState
{
    Pursue,
    Evade
}

public class DriverStrategicBehaviour : MonoBehaviour
{
    public DriverSituationalAwareness SituationalAwareness;
    public NPCDriverState State = NPCDriverState.Pursue;
    public Transform Target;

    private float EndEvadeTime;

    bool IsBeingTargeted()
    {
        foreach (Transform target in SituationalAwareness.Targets)
        {
            if (target != null)
            {
                Vector3 targetToMe = Vector3.Normalize(transform.position - target.position);
                Vector3 targetForward = transform.forward;

                float angle = Vector3.Dot(targetToMe, targetForward);
                if (angle > 0.8)
                {
                    return true;
                }
            }
        }

        return false;
    }

    void PickTarget()
    {
        float closest = float.MaxValue;
        Transform closestTarget = null;
        foreach (Transform target in SituationalAwareness.Targets)
        {
            // Apparently it is possible that game objects are destroyed between the call that fills Targets and this call.
            if (target != null)
            {
                float distance = Vector3.Distance(target.position, transform.position);
                if (distance < closest)
                {
                    closest = distance;
                    closestTarget = target;
                }
            }
        }

        Target = closestTarget;
    }

	void Update ()
    {
        switch (State)
        {
            case NPCDriverState.Pursue:
                PickTarget();
                /*
                if (IsBeingTargeted())
                {
                    State = NPCDriverState.Evade;
                    EndEvadeTime = Time.time + 0.2f;
                }
                */
                break;

            case NPCDriverState.Evade:
                /*
                if (!IsBeingTargeted() && Time.time >= EndEvadeTime)
                {
                    State = NPCDriverState.Pursue;
                }
                */
                State = NPCDriverState.Pursue;
                break;
        }
    }
}

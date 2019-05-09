using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriverSteeringBehaviour : MonoBehaviour
{
    public DriverSituationalAwareness   SituationalAwareness;
    public DriverStrategicBehaviour     StrategicBehaviour;
    public Vehicle                      Vehicle;
    public AssignedFaction              Faction;

    private System.Random random;

    void Start()
    {
        random = new System.Random();
    }

	bool IsTargetInFront()
    {
        foreach (Transform target in SituationalAwareness.Targets)
        {
            if (target != null)
            {
                Vector3 meToTarget = target.position - transform.position;
                float angle = Vector3.Dot(transform.right, meToTarget);
                if (angle > 0.8)
                {
                    return true;
                }
            }
        }

        return false;
    }

    void Evade()
    {
        if (SituationalAwareness.Targets.Count > 0)
        {
            if (random.Next(9) < 6)
            {
                Transform target = SituationalAwareness.Targets[0];
                if (target)
                {
                    Vector3 targetToMe = transform.position - target.position;
                    float angle = Vector3.SignedAngle(target.right, targetToMe, Vector3.up);
                    if (angle > 0)
                    {
                        Vehicle.SteerRight();
                    }
                    else
                    {
                        Vehicle.SteerLeft();
                    }
                }
            }
            else
            {
                if (random.Next(1) == 0)
                {
                    Vehicle.SteerRight();
                }
                else
                {
                    Vehicle.SteerLeft();
                }
            }
        }            
    }

    void Pursue()
    {
        if (SituationalAwareness.Targets.Count > 0)
        {
            var target = StrategicBehaviour.Target;
            if (target != null)
            {
                Vector3 meToTarget = target.position - transform.position;

                float angle = Vector3.SignedAngle(transform.right, meToTarget, Vector3.up);
                if (angle > 0)
                {
                    Vehicle.SteerRight();
                }
                else
                {
                    Vehicle.SteerLeft();
                }
            }
        }
    }

	void Update ()
    {
        // Always move forward
        Vehicle.PushThrottle();

        // Fire at target
        if (IsTargetInFront())
        {
            Vehicle.DischargeWeapons(Faction.Faction);
        }

        switch (StrategicBehaviour.State)
        {
            case NPCDriverState.Evade:
                Evade();
                break;
            case NPCDriverState.Pursue:
                Pursue();
                break;
        }
    }
}

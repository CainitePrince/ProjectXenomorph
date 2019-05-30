using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DriverSteeringBehaviour : MonoBehaviour
{
    public DriverSituationalAwareness   SituationalAwareness;
    public DriverStrategicBehaviour     StrategicBehaviour;
    public Vehicle                      Vehicle;
    public AssignedFaction              Faction;
    public NavMeshAgent                 Agent;

    private System.Random random;

    void Start()
    {
        random = new System.Random();
        Agent = GetComponent<NavMeshAgent>();

        Agent.destination = Vector3.zero;
    }

	bool IsTargetInFront()
    {
        foreach (Transform target in SituationalAwareness.Targets)
        {
            if (target != null)
            {
                Vector3 meToTarget = Vector3.Normalize(target.position - transform.position);
                float angle = Vector3.Dot(transform.forward, meToTarget);
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
        /*
        float step = Time.deltaTime * Vehicle.RotationSpeed * Mathf.Deg2Rad;
        if (Agent.velocity.magnitude < 0.1f)
        {
            Debug.Log("Set random velocity");

            Vector3 r = Random.onUnitSphere;

            Debug.Log("Gen: " + r);

            r.y = 0;
            r = Vector3.Normalize(r);

            Debug.Log("Nor gen: " + r);
            Agent.velocity = r;


            Debug.Log("Random v:" + Agent.velocity);
        }
        Vector3 v = Quaternion.AngleAxis(Mathf.Sign(Mathf.Sin(Time.time)) * 90, Vector3.up) * Agent.velocity;
        Agent.velocity = Vector3.RotateTowards(Agent.velocity, v, step, 0.0f);
        Debug.Log(Agent.velocity);
        */

        /*
        // Manual control
        Agent.acceleration = 0;
        Vehicle.PushThrottle();

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
        */
    }

    void Pursue()
    {
        if (SituationalAwareness.Targets.Count > 0)
        {
            var target = StrategicBehaviour.Target;
            
            if (target != null)
            {
                Agent.acceleration = Vehicle.Acceleration;
                Agent.destination = target.position;

                Vector3 meToTarget = target.position - transform.position;

                if (meToTarget.magnitude < Agent.stoppingDistance)
                {
                    float angle = Vector3.SignedAngle(transform.forward, meToTarget, Vector3.up);
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
    }

	void Update ()
    {
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

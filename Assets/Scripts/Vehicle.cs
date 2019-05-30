using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
    public float Acceleration = 1;
    public float RotationSpeed = 1;
    public Transform[] HardPoints;
    public Transform[] Turrets;
    public List<Weapon> Weapons = new List<Weapon>();
    public float RaycastYOffset;
    public float RaycastLength;
    public Afterburner Afterburner;

    void OnDrawGizmos()
    {
        Vector3 raycastOrigin = transform.parent.position;
        raycastOrigin.y += RaycastYOffset;
        Gizmos.DrawLine(raycastOrigin, raycastOrigin + transform.forward * RaycastLength);
    }

    public void PushThrottle(bool useAfterburner = false)
    {
        Vector3 raycastOrigin = transform.parent.position;
        raycastOrigin.y += RaycastYOffset;

        float speedRatio = 1.0f;
        if (useAfterburner)
        {
            if (Afterburner && Afterburner.Use())
            {
                speedRatio = Afterburner.SpeedRatio;
            }
        }

        if (!Physics.Raycast(raycastOrigin, transform.forward, RaycastLength))
        {
            transform.parent.position += transform.parent.forward * speedRatio * Acceleration * Time.deltaTime;
        }        
    }

    public void SteerRight()
    {
        transform.parent.Rotate(new Vector3(0, RotationSpeed * Time.deltaTime, 0));
    }

    public void SteerLeft()
    {
        transform.parent.Rotate(new Vector3(0, -RotationSpeed * Time.deltaTime, 0));
    }

    public void DischargeWeapons(Faction faction)
    {
        foreach (Weapon weapon in Weapons)
        {
            weapon.Fire(faction);
        }
    }
}

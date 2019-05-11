using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
    public float Acceleration = 1;
    public float RotationSpeed = 1;
    public Transform[] HardPoints;
    public List<Weapon> Weapons = new List<Weapon>();
    public float RaycastYOffset;
    public float RaycastLength;
    public Afterburner Afterburner;

    //private bool usingAfterburner = false;

    void OnDrawGizmos()
    {
        Vector3 raycastOrigin = transform.parent.position;
        raycastOrigin.y += RaycastYOffset;
        Gizmos.DrawLine(raycastOrigin, raycastOrigin + transform.right * RaycastLength);
    }

    /*
    public void UseAfterburner()
    {
        if (Afterburner != null)
        {
            usingAfterburner = Afterburner.Use();
        }
    }
    */

    public void PushThrottle(bool ua = false)
    {
        Vector3 raycastOrigin = transform.parent.position;
        raycastOrigin.y += RaycastYOffset;

        float speedRatio = 1.0f;
        if (ua)
        {
            if (Afterburner.Use())
            {
                speedRatio = Afterburner.SpeedRatio;
            }
            //usingAfterburner = false;
        }

        //Debug.DrawLine(raycastOrigin, raycastOrigin + transform.right);
        if (!Physics.Raycast(raycastOrigin, transform.right, RaycastLength))
        {
            //Vector3 position = transform.parent.position;
            //Debug.Log(speedRatio);
            transform.parent.position += transform.parent.right * speedRatio * Acceleration * Time.deltaTime;
            //transform.parent.localPosition = position;
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

using UnityEngine;

public class Afterburner : MonoBehaviour
{
    public float SpeedRatio;
    public float PowerConsumption;
    public Generator Generator;
    public Vehicle Vehicle;

    float nextFireTime;

    void Start()
    {
        nextFireTime = Time.time;
    }

    public bool Use()
    {
        if ((PowerConsumption * Time.deltaTime) <= Generator.AvailablePower && Time.time >= nextFireTime)
        {
            Generator.AvailablePower -= PowerConsumption * Time.deltaTime;
            return true;
        }
        else
        {
            nextFireTime = Time.time + 0.1f;
        }
        
        return false;
    }
}

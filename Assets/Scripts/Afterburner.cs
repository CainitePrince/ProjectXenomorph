using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Afterburner : MonoBehaviour
{
    public float SpeedRatio;
    public float PowerConsumption;
    public Generator Generator;
    public Vehicle Vehicle;

    //bool sub = false;

    float nextFireTime;

    void Start()
    {
        nextFireTime = Time.time;
    }

    public bool Use()
    {
        //Debug.Log("Using");
        
        if ((PowerConsumption * Time.deltaTime) <= Generator.AvailablePower && Time.time >= nextFireTime)
        {
            Generator.AvailablePower -= PowerConsumption * Time.deltaTime;
            //sub = true;
            return true;
        }
        else
        {
            nextFireTime = Time.time + 0.1f;
        }
        
        return false;
        //{
            //Debug.Log("Sub");
            //Generator.AvailablePower = Generator.AvailablePower - (PowerConsumption * Time.deltaTime);
            //Generator.UsePower(PowerConsumption * Time.deltaTime);
            //return true;
        //}
        //return false;
    }

    void Update()
    {
        //if (sub)
        //{
            //Generator.AvailablePower -= PowerConsumption * Time.deltaTime;
            //Debug.Log(Generator.AvailablePower);
            //sub = false;
        //}
    }
}

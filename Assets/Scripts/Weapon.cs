﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float RateOfFire;
    public float PowerConsumption;
    public GameObject DischargePrefab;
    public Transform Nozzle;
    public Generator Generator;

    private float nextFireTime;

    void Start ()
    {
        nextFireTime = Time.time;
	}

    public void Fire(Faction faction)
    {
        if (Time.time >= nextFireTime && PowerConsumption <= Generator.AvailablePower)
        {
            nextFireTime = Time.time + (1 / RateOfFire);

            Generator.AvailablePower -= PowerConsumption;

            GameObject discharge = Instantiate(DischargePrefab);
            discharge.transform.position = Nozzle.position;
            discharge.transform.rotation = Nozzle.rotation;
            discharge.GetComponent<WeaponDischarge>().Faction = faction;
        }
    }
}

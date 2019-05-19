using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    Projectile,
    Beam
}

public class Weapon : MonoBehaviour
{
    [Header("Common")]
    public Transform        Nozzle;
    public Generator        Generator;
    [Tooltip("Power consumption is units per second")]
    public float            PowerConsumption;
    public WeaponType       Type;
    [Tooltip("Damage per second")]
    public float            ShieldDamage;
    [Tooltip("damage per second")]
    public float            ArmourDamage;
    public WeaponDischarge  DischargePrefab;
    public float            MaxDistance;
    [Tooltip("Shots per second")]
    public float            RateOfFire;
    public LayerMask        Layers;

    [Header("Projectile")]
    public float ProjectileSpeed;

    [Header("Beam")]
    public float LifeTime;
    public bool Continuous;
    private WeaponDischarge currentDischarge;

    private float nextFireTime;

    void Start ()
    {
        nextFireTime = Time.time;
	}

    public void Fire(Faction faction)
    { 
        float powerConsumption = PowerConsumption / RateOfFire;
        float shieldDamage = ShieldDamage / RateOfFire;
        float armourDamage = ArmourDamage / RateOfFire;

        if (Continuous)
        {
            nextFireTime = 0;
            powerConsumption = PowerConsumption * Time.deltaTime;
            shieldDamage = ShieldDamage * Time.deltaTime;
            armourDamage = ArmourDamage * Time.deltaTime;
        }

        switch (Type)
        {
            case WeaponType.Projectile:
                if (Time.time >= nextFireTime && powerConsumption <= Generator.AvailablePower)
                {
                    nextFireTime = Time.time + (1 / RateOfFire);

                    Generator.AvailablePower -= powerConsumption;

                    WeaponDischarge discharge = Instantiate(DischargePrefab);
                    discharge.transform.position = Nozzle.position;
                    discharge.transform.rotation = Nozzle.rotation;
                    discharge.Faction = faction;
                    discharge.ShieldDamage = shieldDamage;
                    discharge.ArmourDamage = armourDamage;
                    discharge.Speed = ProjectileSpeed;
                    discharge.EndTime = Time.time + (MaxDistance / ProjectileSpeed); 
                    discharge.Owner = transform.parent.parent.gameObject;
                }
                break;
            case WeaponType.Beam:
                if (Time.time >= nextFireTime && powerConsumption <= Generator.AvailablePower)
                {
                    nextFireTime = Time.time + (1 / RateOfFire);

                    Generator.AvailablePower -= powerConsumption;

                    if (currentDischarge == null)
                    {
                        currentDischarge = Instantiate(DischargePrefab);
                    }
                    currentDischarge.BeamStart = Nozzle.position;
                    currentDischarge.BeamEnd = Nozzle.position + MaxDistance * Nozzle.right;
                    currentDischarge.EndTime = Time.time + LifeTime;

                    RaycastHit hit;
                    Ray ray = new Ray(Nozzle.position, Nozzle.right);
                    //int layer = ~(LayerMask.NameToLayer("City") | LayerMask.NameToLayer("Vehicles"));
                    if (Physics.Raycast(ray, out hit, 100, Layers))
                    {
                        var destroyable = hit.collider.gameObject.GetComponent<Destroyable>();
                        if (destroyable)
                        {
                            destroyable.HandleHit(shieldDamage, armourDamage, faction);
                            currentDischarge.BeamEnd = hit.point;
                        }
                        else
                        {
                            var blocker = hit.collider.GetComponent<Blocker>();
                            if (blocker)
                            {
                                currentDischarge.BeamEnd = hit.point;
                            }
                        }
                    }
                }
                break;
        }
    }
}

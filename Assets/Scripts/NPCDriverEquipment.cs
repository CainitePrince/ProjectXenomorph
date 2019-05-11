using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDriverEquipment : MonoBehaviour
{
    public Vehicle Vehicle;
    public Armour Armour;
    public Shield Shield;
    public Generator Generator;
    public List<Weapon> Weapons;
    public NPCHealthBars HealthBars;

    void Start()
    {
        var faction = GetComponent<AssignedFaction>();

        var armour = Instantiate(Armour, transform);
        HealthBars.Armour = armour;

        var vehicle = Instantiate(Vehicle, transform);

        var destroyable = vehicle.GetComponent<Destroyable>();
        destroyable.AssignedFaction = faction;
        destroyable.Armour = armour;
        destroyable.DestroyThis = gameObject;

        var target = vehicle.GetComponent<Target>();
        target.Faction = faction;

        var steering = GetComponent<DriverSteeringBehaviour>();
        steering.Vehicle = vehicle;

        var generator = Instantiate(Generator, transform);
        HealthBars.Power = generator;

        if (Shield)
        {
            var shield = Instantiate(Shield, transform);
            shield.Generator = generator;
            HealthBars.Shield = shield;
            destroyable.Shield = shield;
        }

        if (vehicle.HardPoints.Length < Weapons.Count)
        {
            Debug.LogError("More weapons equipped than vehicle has hardpoints");
            return;
        }

        for (int i = 0; i < Weapons.Count; ++i)
        {
            var weapon = Instantiate(Weapons[i], vehicle.HardPoints[i]);
            weapon.Generator = generator;
            vehicle.Weapons.Add(weapon);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    public Vehicle      Vehicle;
    public Armour       Armour;
    public Shield       Shield;
    public Generator    Generator;
    public List<Weapon> Weapons;
    public Afterburner  Afterburner;

	void Start ()
    {
        var player = GetComponent<Player>();
        var faction = player.GetComponent<AssignedFaction>();

        var armour = Instantiate(Armour, transform);

        var vehicle = Instantiate(Vehicle, transform);

        var destroyable = vehicle.GetComponent<Destroyable>();
        destroyable.AssignedFaction = faction;
        destroyable.Armour = armour;
        destroyable.DestroyThis = gameObject;

        var target = vehicle.GetComponent<Target>();
        target.Faction = faction;
        
        player.Vehicle = vehicle;

        var generator = Instantiate(Generator, transform);

        if (Afterburner)
        {
            var afterburner = Instantiate(Afterburner, transform);
            afterburner.Generator = generator;
            vehicle.Afterburner = afterburner;
            afterburner.Vehicle = vehicle;
        }

        if (Shield)
        {
            var shield = Instantiate(Shield, transform);
            shield.Generator = generator;
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

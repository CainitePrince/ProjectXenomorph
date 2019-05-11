using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomNPCDriver : MonoBehaviour
{
    public Vehicle[] Vehicles;
    public Shield[] Shields;
    public Armour[] Armours;
    public Generator[] Generators;
    public Weapon[] Weapons;

    static int count = 0;

	void Awake ()
    {
        System.Random random = new System.Random(System.Environment.TickCount + count);
        ++count;

        var equipment = GetComponent<NPCDriverEquipment>();
        equipment.Generator = Generators[random.Next(Generators.Length)];
        equipment.Vehicle = Vehicles[random.Next(Vehicles.Length)];
        equipment.Armour = Armours[random.Next(Armours.Length)];
        equipment.Shield = Shields[random.Next(Shields.Length)];

        int numHardpoints = equipment.Vehicle.HardPoints.Length;
        for (int i = 0; i < numHardpoints; ++i)
        {
            equipment.Weapons.Add(Weapons[random.Next(Weapons.Length)]);
        }

        var faction = GetComponent<AssignedFaction>();
        faction.Faction = (Faction)random.Next(3);

        var inventory = GetComponent<Inventory>();
        inventory.Items.Add(equipment.Generator.gameObject);
        inventory.Items.Add(equipment.Armour.gameObject);
        if (equipment.Shield)
        {
            inventory.Items.Add(equipment.Shield.gameObject);
        }
        foreach (var weapon in equipment.Weapons)
        {
            inventory.Items.Add(weapon.gameObject);
        }
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Faction
{
    Red,
    Blue,
    Green,
    Player,
    Pirate,
    Neutral,
    Civilian
};

public class AssignedFaction : MonoBehaviour
{
    public Faction Faction;
}

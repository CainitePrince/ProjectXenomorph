using UnityEngine;

namespace DuneRunner
{
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
}

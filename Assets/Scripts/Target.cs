using UnityEngine;

namespace DuneRunner
{
    public class Target : MonoBehaviour
    {
        public AssignedFaction Faction;

        TargetManager targetManager;

        void Start()
        {
            targetManager = GameObject.FindObjectOfType<TargetManager>();
            targetManager.PotentialTargets.Add(this);
        }

        void OnDestroy()
        {
            targetManager.PotentialTargets.Remove(this);
        }
    }
}

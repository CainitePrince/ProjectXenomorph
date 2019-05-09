using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriverSituationalAwareness : MonoBehaviour
{
    public List<Transform> Targets;
    public AssignedFaction Faction;

    private TargetManager targetManager;

    void Start()
    {
        targetManager = FindObjectOfType<TargetManager>();
    }

	void AcquireTargets()
    {
        Targets.Clear();
        foreach (Target target in targetManager.PotentialTargets)
        {
            if (Faction.Faction != target.Faction.Faction)
            {
                Targets.Add(target.transform);
            }
        }
    }

	void Update ()
    {
        AcquireTargets();
	}
}

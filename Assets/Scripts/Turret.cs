using UnityEngine;

namespace DuneRunner
{
    public class Turret : MonoBehaviour
    {
        public Transform[] Hardpoints;
        public float TurnRate;
        public float MaxDistance;
        public AssignedFaction Faction;
        public Weapon[] WeaponPrefabs;
        public Generator Generator;
        public GameObject Owner;

        private TargetManager TargetManager;
        private Transform CurrentTarget;
        private Weapon[] Weapons;

        void Start()
        {
            TargetManager = GameObject.FindObjectOfType<TargetManager>();

            Weapons = new Weapon[WeaponPrefabs.Length];
            for (int i = 0; i < WeaponPrefabs.Length; ++i)
            {
                var weapon = Instantiate(WeaponPrefabs[i], Hardpoints[i]);
                weapon.Generator = Generator;
                weapon.Owner = Owner;
                Weapons[i] = weapon;
            }
        }

        void PickTarget()
        {
            float closest = float.MaxValue;
            Transform closestTarget = null;
            foreach (Target target in TargetManager.PotentialTargets)
            {
                // Apparently it is possible that game objects are destroyed between the call that fills Targets and this call.
                if (target != null)
                {
                    if (target.Faction.Faction != Faction.Faction)
                    {
                        float distance = Vector3.Distance(target.transform.position, transform.position);
                        if (distance < MaxDistance && distance < closest)
                        {
                            closest = distance;
                            closestTarget = target.transform;
                        }
                    }
                }
            }

            CurrentTarget = closestTarget;
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(transform.position, transform.forward);
            if (CurrentTarget)
            {
                Gizmos.color = Color.white;
                Vector3 meToTarget = CurrentTarget.position - transform.position;
                Gizmos.DrawRay(transform.position, meToTarget);
            }
        }

        void Update()
        {
            if (CurrentTarget == null)
            {
                // Pick a target if we don't have one
                PickTarget();
            }
            else
            {
                // If distance to target got too big pick a new target
                Vector3 meToTarget = CurrentTarget.position - transform.position;
                float distance = meToTarget.magnitude;

                if (distance >= MaxDistance)
                {
                    PickTarget();
                }
            }

            // It's possible no targets are available
            if (CurrentTarget)
            {
                Debug.Log(CurrentTarget);

                Vector3 targetPos = new Vector3(CurrentTarget.position.x, transform.position.y, CurrentTarget.position.z);
                Vector3 meToTarget = targetPos - transform.position;

                // Rotate to target
                float angle = Vector3.SignedAngle(transform.forward, Vector3.Normalize(meToTarget), Vector3.up);


                Debug.Log(angle);

                if (angle > 0)
                {
                    transform.Rotate(new Vector3(0, TurnRate * Time.deltaTime, 0));
                }

                if (angle < 0)
                {
                    transform.Rotate(new Vector3(0, -TurnRate * Time.deltaTime, 0));
                }

                // Fire at target
                if (Mathf.Abs(angle) < 10)
                {
                    foreach (var weapon in Weapons)
                    {
                        weapon.Fire(Faction.Faction);
                    }
                }
            }
            else
            {
                // Return to default position
                float angle = Vector3.SignedAngle(transform.forward, Owner.transform.forward, Vector3.up);

                if (angle > 0)
                {
                    transform.Rotate(new Vector3(0, TurnRate * Time.deltaTime, 0));
                }

                if (angle < 0)
                {
                    transform.Rotate(new Vector3(0, -TurnRate * Time.deltaTime, 0));
                }
            }
        }
    }
}

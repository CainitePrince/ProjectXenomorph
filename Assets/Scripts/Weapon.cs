using UnityEngine;

namespace DuneRunner
{
    public enum WeaponType
    {
        //Arc,
        Projectile,
        Beam
    }

    public class Weapon : MonoBehaviour
    {
        [Header("Common")]
        public Transform Nozzle;
        public Generator Generator;
        [Tooltip("Power consumption is units per second")]
        public float PowerConsumption;
        public WeaponType Type;
        [Tooltip("Damage per second")]
        public float ShieldDamage;
        [Tooltip("damage per second")]
        public float ArmourDamage;
        public WeaponDischarge DischargePrefab;
        public float MaxDistance;
        [Tooltip("Shots per second")]
        public float RateOfFire;
        public LayerMask Layers;
        public GameObject Owner;

        [Header("Projectile")]
        public float ProjectileSpeed;
        public Vector3 ProjectileGravity;
        public float ExplosionRadius;

        [Header("Beam")]
        public float LifeTime;
        public bool Continuous;
        private WeaponDischarge currentDischarge;

        private float nextFireTime;
        private ObjectPool Pool;

        void Start()
        {
            nextFireTime = Time.time;
            Pool = gameObject.AddComponent<ObjectPool>();
            if (Pool == null)
            {
                Debug.LogWarning("Pool is null");
            }
            Pool.Prefab = DischargePrefab.gameObject;
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

                        WeaponDischarge discharge = Pool.Rent().GetComponent<WeaponDischarge>();
                        discharge.Init(faction,
                                       shieldDamage,
                                       armourDamage,
                                       Owner,
                                       Time.time + (MaxDistance / ProjectileSpeed),
                                       Vector3.zero,
                                       Vector3.zero,
                                       Pool,
                                       Nozzle.forward * ProjectileSpeed,
                                       ExplosionRadius,
                                       ProjectileGravity,
                                       Layers);
                        discharge.transform.position = Nozzle.position;
                        discharge.transform.rotation = Quaternion.FromToRotation(discharge.Front, discharge.Direction);
                        //discharge.Pool = Pool;

                        //discharge.Direction = Nozzle.right * ProjectileSpeed;
                        //discharge.Gravity = ProjectileGravity;

                        //discharge.Faction = faction;
                        //discharge.ShieldDamage = shieldDamage;
                        //discharge.ArmourDamage = armourDamage;
                        ////discharge.Speed = ProjectileSpeed;
                        //discharge.EndTime = Time.time + (MaxDistance / ProjectileSpeed);
                        //discharge.Owner = transform.parent.parent.gameObject;
                        //discharge.ExplosionRadius = ExplosionRadius;
                        //discharge.Mask = Layers;
                    }
                    break;
                /*
                case WeaponType.Projectile:
                    if (Time.time >= nextFireTime && powerConsumption <= Generator.AvailablePower)
                    {
                        nextFireTime = Time.time + (1 / RateOfFire);

                        Generator.AvailablePower -= powerConsumption;

                        WeaponDischarge discharge = Pool.Rent().GetComponent<WeaponDischarge>();
                        discharge.Pool = Pool;
                        discharge.transform.position = Nozzle.position;
                        discharge.Direction = Nozzle.right;
                        discharge.transform.rotation = Quaternion.FromToRotation(discharge.Front, discharge.Direction);
                        discharge.Faction = faction;
                        discharge.ShieldDamage = shieldDamage;
                        discharge.ArmourDamage = armourDamage;
                        discharge.Speed = ProjectileSpeed;
                        discharge.EndTime = Time.time + (MaxDistance / ProjectileSpeed); 
                        discharge.Owner = transform.parent.parent.gameObject;
                    }
                    break;
                */
                case WeaponType.Beam:
                    if (Time.time >= nextFireTime && powerConsumption <= Generator.AvailablePower)
                    {
                        nextFireTime = Time.time + (1 / RateOfFire);

                        Generator.AvailablePower -= powerConsumption;

                        if (currentDischarge == null || !currentDischarge.isActiveAndEnabled)
                        {
                            currentDischarge = Pool.Rent().GetComponent<WeaponDischarge>();
                            currentDischarge.Pool = Pool;
                        }
                        currentDischarge.BeamStart = Nozzle.position;
                        currentDischarge.BeamEnd = Nozzle.position + MaxDistance * Nozzle.forward;
                        currentDischarge.EndTime = Time.time + LifeTime;

                        RaycastHit hit;
                        Ray ray = new Ray(Nozzle.position, Nozzle.forward);
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
}

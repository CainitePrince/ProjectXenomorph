using System.Collections;
using UnityEngine;

namespace DuneRunner
{
    public class WeaponDischarge : MonoBehaviour
    {
        [Header("Set by weapon")]
        public Faction Faction;
        public float ShieldDamage;
        public float ArmourDamage;
        public GameObject Owner;
        public float EndTime;
        public LineRenderer Line;
        public Vector3 BeamStart;
        public Vector3 BeamEnd;
        public ObjectPool Pool;
        public Vector3 Direction;
        public float ExplosionRadius;
        public Vector3 Gravity;
        public LayerMask Mask;

        [Header("Set in inspector")]
        [SerializeField] private ParticleSystem particles;
        [SerializeField] private WeaponType Type;
        public Vector3 Front;

        private bool dead = false;

        void Start()
        {
            if (Type == WeaponType.Beam)
            {
                Line = GetComponent<LineRenderer>();
            }
        }

        public void Init(Faction faction,
                         float shieldDamage,
                         float armourDamage,
                         GameObject owner,
                         float endTime,
                         Vector3 beamStart,
                         Vector3 beamEnd,
                         ObjectPool pool,
                         Vector3 direction,
                         float explosionRadius,
                         Vector3 gravity,
                         LayerMask mask)
        {
            dead = false;

            Faction = faction;
            ShieldDamage = shieldDamage;
            ArmourDamage = armourDamage;
            Owner = owner;
            EndTime = endTime;
            BeamStart = beamStart;
            BeamEnd = beamEnd;
            Pool = pool;
            Direction = direction;
            ExplosionRadius = explosionRadius;
            Gravity = gravity;
            Mask = mask;
        }

        public void Explode()
        {
            // We only want to trigger the explosion once
            if (!dead)
            {
                Collider[] colliders = Physics.OverlapSphere(transform.position, ExplosionRadius, Mask);

                foreach (var c in colliders)
                {
                    var d = c.GetComponent<Destroyable>();
                    if (d)
                    {
                        d.HandleHit(ShieldDamage, ArmourDamage, Faction);
                    }
                }

                particles.Play();

                StartCoroutine(RemoveAfterDelay(0.1f));
            }
        }

        IEnumerator RemoveAfterDelay(float delay)
        {
            dead = true;
            yield return new WaitForSeconds(delay);
            Remove();
        }

        void Update()
        {
            if (dead) return;

            switch (Type)
            {
                case WeaponType.Projectile:
                    Direction += Gravity * Time.deltaTime;
                    transform.position += Direction * Time.deltaTime;

                    // If the projectile hits the ground.
                    // To do: this won't work when we introduce non-flat ground
                    if (transform.position.y < 0)
                    {
                        Explode();
                    }
                    break;

                case (WeaponType.Beam):
                    Line.SetPositions(new Vector3[] { BeamStart, BeamEnd });
                    break;
            }

            if (Time.time > EndTime)
            {
                Remove();
            }
        }

        public void Remove()
        {
            Pool.Return(gameObject);
        }
    }
}

using System.Collections;
using UnityEngine;

namespace DuneRunner
{
    public class Destroyable : MonoBehaviour
    {
        public AssignedFaction AssignedFaction;
        public Armour Armour;
        public Shield Shield;
        public GameObject ShieldBubble;
        public GameObject DestroyThis;
        public bool GodMode;

        private ItemDrops drops;
        private Inventory playerInventory;

        void Start()
        {
            drops = GetComponentInParent<ItemDrops>();
            playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        }

        void OnCollisionEnter(Collision col)
        {
            var discharge = col.gameObject.GetComponent<WeaponDischarge>();
            if (discharge)
            {
                // Disregard self-collision
                if (discharge.Owner == gameObject)
                {
                    return;
                }

                if (discharge.ExplosionRadius > 0)
                {
                    //Debug.Log("1:" + discharge.Owner.ToString() + ", 2: " + gameObject.ToString());

                    // Explode will call our HandleHit along with others within radius.
                    discharge.Explode();
                }
                else
                {
                    discharge.Remove();

                    HandleHit(discharge.ShieldDamage, discharge.ArmourDamage, discharge.Faction);
                }
            }
        }

        public void HandleHit(float shieldDamage, float armourDamage, Faction faction)
        {
            if (GodMode) return;

            if (faction != AssignedFaction.Faction)
            {
                if (Shield)
                {
                    Shield.Buffer -= shieldDamage;

                    if (Shield.Buffer < 0)
                    {
                        float surplus = Mathf.Abs(Shield.Buffer);


                        Shield.Buffer = 0;
                        float percentageLeft = surplus / shieldDamage;
                        Armour.Buffer -= percentageLeft * armourDamage;

                        Debug.Log(surplus + " " + percentageLeft + " " + (percentageLeft * armourDamage) + " " + (surplus / shieldDamage));
                    }
                    else
                    {
                        StartCoroutine(ShieldFlicker());
                    }
                }
                else
                {
                    Armour.Buffer -= armourDamage;
                }

                if (Armour.Buffer <= 0)
                {
                    // Clean up object
                    Destroy(DestroyThis);

                    // Drop items, if applicable
                    if (drops)
                    {
                        drops.Drop();
                    }

                    // Some NPC have bounties on them.
                    if (faction == Faction.Player)
                    {
                        var bounty = GetComponentInParent<Bounty>();
                        if (bounty)
                        {
                            playerInventory.Credits += bounty.Credits;
                        }
                    }
                }
            }
        }

        IEnumerator ShieldFlicker()
        {
            ShieldBubble.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            ShieldBubble.SetActive(false);
        }
    }
}

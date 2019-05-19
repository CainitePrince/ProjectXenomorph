using System.Collections;
using UnityEngine;

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
            if (discharge.Owner != gameObject)
            {
                discharge.Remove();
            }

            HandleHit(discharge.ShieldDamage, discharge.ArmourDamage, discharge.Faction);
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
                    float percentageLeft = 1.0f - surplus / shieldDamage;
                    Armour.Buffer -= percentageLeft * armourDamage;
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

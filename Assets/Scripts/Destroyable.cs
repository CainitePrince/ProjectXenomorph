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

            if (GodMode) return;

            if (discharge.Faction != AssignedFaction.Faction)
            {
                if (Shield)
                {
                    Shield.Buffer -= discharge.ShieldDamage;

                    if (Shield.Buffer < 0)
                    {
                        float surplus = Mathf.Abs(Shield.Buffer);
                        Shield.Buffer = 0;
                        float percentageLeft = 1.0f - surplus / discharge.ShieldDamage;
                        Armour.Buffer -= percentageLeft * discharge.ArmourDamage;
                    }
                    else
                    {
                        StartCoroutine(ShieldFlicker());
                    }
                }
                else
                {
                    Armour.Buffer -= discharge.ArmourDamage;
                }

                if (Armour.Buffer <= 0)
                {
                    Destroy(DestroyThis);

                    if (drops)
                    {
                        drops.Drop();
                    }

                    if (discharge.Faction == Faction.Player)
                    {
                        var bounty = GetComponentInParent<Bounty>();
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyable : MonoBehaviour
{
    public AssignedFaction Faction;
    public Armour Armour;
    public Shield Shield;
    public GameObject ShieldBubble;
    public GameObject DestroyThis;

    void OnCollisionEnter(Collision col)
    {
        var discharge = col.gameObject.GetComponent<WeaponDischarge>();
        if (discharge)
        {
            if (discharge.Faction != Faction.Faction)
            {
                discharge.Remove();

                if (Shield)
                {
                    Shield.Buffer -= discharge.Damage;

                    if (Shield.Buffer < 0)
                    {
                        float surplus = Mathf.Abs(Shield.Buffer);
                        Shield.Buffer = 0;
                        Armour.Buffer -= surplus;
                    }
                    else
                    {
                        StartCoroutine(ShieldFlicker());
                    }
                }
                else
                {
                    Armour.Buffer -= discharge.Damage;
                }

                if (Armour.Buffer <= 0)
                {
                    Destroy(DestroyThis);
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

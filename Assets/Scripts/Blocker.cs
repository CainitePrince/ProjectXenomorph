using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocker : MonoBehaviour
{
    void OnCollisionEnter(Collision col)
    {
        var discharge = col.gameObject.GetComponent<WeaponDischarge>();
        if (discharge)
        {
            discharge.Remove();
        }
    }
}

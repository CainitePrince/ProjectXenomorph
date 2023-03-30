using UnityEngine;

namespace DuneRunner
{
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
}

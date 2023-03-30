using System.Collections.Generic;
using UnityEngine;

namespace DuneRunner
{
    public class ItemDrops : MonoBehaviour
    {
        public List<GameObject> Drops;
        public ItemPickup ItemDropPrefab;

        public void Drop()
        {
            if (Drops.Count > 0)
            {
                int index = Random.Range(0, Drops.Count - 1);
                ItemPickup pickup = Instantiate(ItemDropPrefab, transform.position, Quaternion.identity);
                pickup.Item = Drops[index];
            }
        }
    }
}

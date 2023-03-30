using System.Collections.Generic;
using UnityEngine;

namespace DuneRunner
{
    public class Inventory : MonoBehaviour
    {
        public List<GameObject> Items;
        public float Credits;

        void Start()
        {
            var drops = GetComponent<ItemDrops>();
            if (drops)
            {
                foreach (var item in Items)
                {
                    drops.Drops.Add(item);
                }
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<GameObject>     Items;
    public float                Credits;

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

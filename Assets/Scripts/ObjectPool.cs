using System.Collections.Generic;
using UnityEngine;

namespace DuneRunner
{
    public class ObjectPool : MonoBehaviour
    {
        public int Count = 100;
        public GameObject Prefab;

        //private List<T> active = new List<T>();
        private List<GameObject> inactive = new List<GameObject>();

        void Start()
        {
            for (int i = 0; i < Count; ++i)
            {
                GameObject item = Instantiate(Prefab);
                item.SetActive(false);
                inactive.Add(item);
            }
        }

        public GameObject Rent()
        {
            if (inactive.Count > 0)
            {
                GameObject item = inactive[inactive.Count - 1];
                inactive.RemoveAt(inactive.Count - 1);
                item.SetActive(true);
                //active.Add(item);
                return item;
            }
            else
            {
                Debug.LogWarning("More requests than items available!");
                return default(GameObject);
            }
        }

        public void Return(GameObject item)
        {
            item.SetActive(false);
            inactive.Add(item);
        }
    }
}

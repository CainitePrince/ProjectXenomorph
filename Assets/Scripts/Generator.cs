using UnityEngine;

namespace DuneRunner
{
    public class Generator : MonoBehaviour
    {
        public float Capacity;
        public float AvailablePower;
        public float ProductionPerSecond;

        public void UsePower(float p)
        {
            AvailablePower -= p;
        }

        void Update()
        {
            AvailablePower = Mathf.Clamp(AvailablePower + ProductionPerSecond * Time.deltaTime, 0, Capacity);
        }
    }
}

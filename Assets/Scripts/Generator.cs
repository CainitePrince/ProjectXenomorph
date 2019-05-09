using UnityEngine;

public class Generator : MonoBehaviour
{
    public float Capacity;
    public float AvailablePower;
    public float ProductionPerSecond;
    
	void Update ()
    {
        AvailablePower = Mathf.Clamp(AvailablePower + ProductionPerSecond * Time.deltaTime, 0, Capacity);
    }
}

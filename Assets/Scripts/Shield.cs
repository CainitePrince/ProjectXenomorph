using System.Collections;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public float Capacity;
    public float Buffer;
    public float UnitsRechargedPerSecond;
    public float PowerConsumption;
    //public GameObject ShieldBubble;
    public Generator Generator;

	void Update ()
    {
        // Amount that we can recharge because of capacity
        float possibleRecharge = Mathf.Clamp(UnitsRechargedPerSecond * Time.deltaTime, 0, Capacity - Buffer);
        
        // Amount of power required for recharge
        float powerConsumption = possibleRecharge * PowerConsumption * Time.deltaTime;

        // Recharge if power allows
        if (Generator.AvailablePower >= powerConsumption)
        {
            Generator.AvailablePower -= powerConsumption;
            Buffer = Mathf.Clamp(Buffer + possibleRecharge, 0, Capacity);
        }
    }
}

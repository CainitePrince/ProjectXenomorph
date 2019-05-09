using UnityEngine;

public class Player : MonoBehaviour
{
    public Vehicle Vehicle;
	
	void Update ()
    {
		if (Input.GetKey(KeyCode.UpArrow))
        {
            Vehicle.PushThrottle();
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Vehicle.SteerLeft();
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            Vehicle.SteerRight();
        }

        if (Input.GetKey(KeyCode.Space))
        {
            Vehicle.DischargeWeapons(Faction.Player);
        }
	}
}

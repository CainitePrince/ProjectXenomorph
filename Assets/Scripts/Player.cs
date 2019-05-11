using UnityEngine;

public class Player : MonoBehaviour
{
    public Vehicle Vehicle;

    //bool tab = false;

	void Update ()
    {
        //if (Input.GetKey(KeyCode.Tab))
        //{
        //    Vehicle.UseAfterburner();
        //}
        //if (Input.GetKeyDown(KeyCode.Tab))
        //{
        //    tab = true;
        //}

        //if (Input.GetKeyUp(KeyCode.Tab))
        //{
        //    tab = false;
        //}

        if (Input.GetKey(KeyCode.UpArrow))
        {
            //Debug.Log(tab);
            Vehicle.PushThrottle(Input.GetKey(KeyCode.Tab));
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

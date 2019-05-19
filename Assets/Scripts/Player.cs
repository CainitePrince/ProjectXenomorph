using UnityEngine;

public class Player : MonoBehaviour
{
    public Vehicle Vehicle;
    public GameObject InventoryScreen;
    public GameObject GameScreen;

	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (!InventoryScreen.activeInHierarchy)
            {
                InventoryScreen.SetActive(true);
                GameScreen.SetActive(false);
            }
            else
            {
                InventoryScreen.SetActive(false);
                GameScreen.SetActive(true);
            }
        }

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

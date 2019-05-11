using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public Image        PowerBar;
    public Image        ShieldBar;
    public Image        ArmourBar;

    public Text         CreditsValue;

    public GameObject   Player;

    private Generator   playerGenerator;
    private Shield      playerShields;
    private Armour      playerArmour;

    private Inventory   playerInventory;

	void Start()
    {
        playerArmour = Player.GetComponentInChildren<Armour>();
        playerShields = Player.GetComponentInChildren<Shield>();
        playerGenerator = Player.GetComponentInChildren<Generator>();
        playerInventory = Player.GetComponent<Inventory>();
    }
	
	void Update ()
    {
        PowerBar.fillAmount  = playerGenerator.AvailablePower / playerGenerator.Capacity;
        ArmourBar.fillAmount = playerArmour.Buffer / playerArmour.Capacity;
        ShieldBar.fillAmount = playerShields.Buffer / playerShields.Capacity;

        CreditsValue.text = playerInventory.Credits.ToString();
	}
}

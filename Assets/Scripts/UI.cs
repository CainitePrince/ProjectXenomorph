using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public Image        PowerBar;
    public Image        ShieldBar;
    public Image        ArmourBar;

    public GameObject   Player;

    private Generator   playerGenerator;
    private Shield      playerShields;
    private Armour      playerArmour;

	void Start()
    {
        playerArmour = Player.GetComponentInChildren<Armour>();
        playerShields = Player.GetComponentInChildren<Shield>();
        playerGenerator = Player.GetComponentInChildren<Generator>();
    }
	
	void Update ()
    {
        PowerBar.fillAmount  = playerGenerator.AvailablePower / playerGenerator.Capacity;
        ArmourBar.fillAmount = playerArmour.Buffer / playerArmour.Capacity;
        ShieldBar.fillAmount = playerShields.Buffer / playerShields.Capacity;
	}
}

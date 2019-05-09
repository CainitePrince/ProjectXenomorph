using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCHealthBars : MonoBehaviour
{
    public Image PowerBar;
    public Image ShieldBar;
    public Image ArmourBar;

    public Generator Power;
    public Armour Armour;
    public Shield Shield;
	
    void Start()
    {
        //Power = GetComponentInParent<Generator>();
        //Armour = GetComponentInParent<Armour>();
        //Shield = GetComponentInParent<Shield>();
        //if (Shield == null)
        //{
        //    ShieldBar.enabled = false;
        //}
    }

	void Update ()
    {
        Camera camera = Camera.main;

        transform.LookAt(transform.position + camera.transform.rotation * Vector3.back, camera.transform.rotation * Vector3.up);

        PowerBar.fillAmount = Power.AvailablePower / Power.Capacity;

        if (Shield != null)
        {
            ShieldBar.fillAmount = Shield.Buffer / Shield.Capacity;
        }
        else
        {
            ShieldBar.enabled = false;
        }
        ArmourBar.fillAmount = Armour.Buffer / Armour.Capacity;
    }
}

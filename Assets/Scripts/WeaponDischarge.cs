using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDischarge : MonoBehaviour
{
    public float        LifeTime = 5;
    public float        Speed = 10;
    public Faction      Faction = Faction.Red;
    public int          ShieldDamage;
    public int          ArmourDamage;
    public GameObject   Owner;

    private float   EndTime;

    void Start ()
    {
        EndTime = Time.time + LifeTime;
	}
	
	void Update ()
    {
        transform.position += transform.right * Speed * Time.deltaTime;

        if (Time.time > EndTime)
        {
            Destroy(gameObject);
        }
	}

    public void Remove()
    {
        Destroy(gameObject);
    }
}

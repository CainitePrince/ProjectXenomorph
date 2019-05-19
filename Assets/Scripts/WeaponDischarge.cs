using UnityEngine;

public class WeaponDischarge : MonoBehaviour
{
    public float            Speed = 10;
    public Faction          Faction = Faction.Red;
    public float            ShieldDamage;
    public float            ArmourDamage;
    public GameObject       Owner;
    public WeaponType       Type;
    public float            EndTime;
    public LineRenderer     Line;
    public Vector3          BeamStart;
    public Vector3          BeamEnd;

    void Start ()
    {
         if (Type == WeaponType.Beam)
        {
            Line = GetComponent<LineRenderer>();
        }
	}

    void Update()
    {
        switch (Type)
        {
            case (WeaponType.Projectile):
                transform.position += transform.right * Speed * Time.deltaTime;       
                break;
            case (WeaponType.Beam):
                Line.SetPositions(new Vector3[] {BeamStart, BeamEnd});
                break;
        }

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

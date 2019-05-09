using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Radar : MonoBehaviour
{
    public Image[] Areas;
    public Color Tier1;
    public Color Tier2;
    public Color Tier3;

    TargetManager targetManager;
    Transform player;

    enum Area
    {
        Top = 0,
        TopLeft = 1,
        Left = 2,
        BottomLeft = 3,
        Bottom = 4,
        BottomRight = 5,
        Right = 6,
        TopRight = 7,
        Center = 8
    }

	void Start ()
    {
        targetManager = GameObject.FindObjectOfType<TargetManager>();
        player = GameObject.Find("Player").transform;
        playerPosition = player.position;
	}

    //void OnDrawGizmos()
    //{
    //    Gizmos.DrawLine(player.transform.position, player.transform.position + new Vector3(0.3826f, 0, 0.923879f) * 100);
    //}

    Vector3 playerPosition;

	void Update ()
    {
        if (player != null)
        {
            playerPosition = player.position;
        }

        const float centerRadius = 10;
        int[] count = new int[9];

	    foreach (var target in targetManager.PotentialTargets)
        {
            if (target.Faction.Faction != Faction.Player)
            {
                if (Vector3.Distance(target.transform.position, playerPosition) < centerRadius)
                {
                    count[(int)Area.Center]++;
                }
                else
                {
                    Vector3 v = Vector3.Normalize(target.transform.position - playerPosition);
                    float angle = Vector3.SignedAngle(v, new Vector3(0.3826f, 0, 0.923879f), Vector3.up);
                    if (angle < 0)
                    {
                        angle += 360;
                    }
                    int area = (int)Mathf.Floor(angle / 45.0f);
                    //Debug.Log("P: " + target.transform.position.ToString() + ", Angle: " + angle.ToString() + ", Area: " + area.ToString());
                    count[area]++;
                    
                }
            }
        }

        for (int i = 0; i < 9; ++i)
        {
            if (count[i] == 0)
            {
                Areas[i].color = Color.white;
            }
            else if (count[i] == 1)
            {
                Areas[i].color = Tier1;
            }
            else if (count[i] == 2)
            {
                Areas[i].color = Tier2;
            }
            else if (count[i] > 2)
            {
                Areas[i].color = Tier3;
            }
        }
	}
}

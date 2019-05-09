using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Player Player;
	
	void Update ()
    {
        if (Player)
        {
            Vector3 position = Player.transform.position;
            position.y = transform.position.y;
            transform.position = position;
        }
	}
}

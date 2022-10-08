using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Player.instance.jumpHability = true;
            Player.instance.canJump = true;
            Destroy(gameObject);
        }
    }
}

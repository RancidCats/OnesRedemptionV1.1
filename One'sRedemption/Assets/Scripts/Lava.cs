using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerFeets"))
        {
            Player.instance.ModifyHealth(0, 1500);
        }
    }
}

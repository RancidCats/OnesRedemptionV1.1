using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeisserCollider : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            MakeDamage();
        }
    }
    private void MakeDamage()
    {
        Player.instance.decreaseHealth = 5;
        Player.instance.burningOn = true;
    }
}

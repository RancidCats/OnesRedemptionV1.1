using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        other.GetComponent<IDamageable>()?.DecreaseHealth((int)Player.instance.damage);
    }
}

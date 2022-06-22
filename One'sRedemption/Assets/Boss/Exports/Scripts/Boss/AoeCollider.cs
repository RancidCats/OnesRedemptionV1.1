using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Collider))]
public class AoeCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            col.GetComponent<IDamageable>().DecreaseHealth(BossController.instance.damage);
            print("toque al player");
            if(gameObject.CompareTag("AoeCollider270"))
            {
                MeshCollider[] colliders = GetComponentsInChildren<MeshCollider>();
                foreach(var collider in colliders)
                {
                    collider.enabled = false;
                }
            }
        }
    }
}

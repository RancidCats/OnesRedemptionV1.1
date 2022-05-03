using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Indicator : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("estalactita"))
        {
            Destroy(gameObject);
        }
        
    }
    
    
}

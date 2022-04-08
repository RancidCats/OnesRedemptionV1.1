using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeetsLogic : MonoBehaviour
{
    int _contactCount;
    public void OnTriggerEnter(Collider other)
    {
        _contactCount++;
        if (_contactCount >= 1)
        
            Player.instance.isGrounded = true;
              
        
    }
    public void OnTriggerExit(Collider other)
    {
        _contactCount--;
        if (_contactCount == 0)       
            Player.instance.isGrounded = false;        
    }

}

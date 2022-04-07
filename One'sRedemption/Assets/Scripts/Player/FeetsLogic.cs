using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeetsLogic : MonoBehaviour
{
    public float contactCount;
    public PlayerMovement player;

    void Start()
    {
        
    }


    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        contactCount++;
        if (contactCount == 1)
        {
            player.canJump = true;
            player.canMove = false;

        }
    }
    private void OnTriggerExit(Collider other)
    {
        contactCount--;
        if (contactCount == 0)
        {
            player.canJump = false;
            player.canMove = true;

        }
    }
}

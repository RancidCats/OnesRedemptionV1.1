using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(this.name == "Checkpoint_1")
            {
                GameManager._pastCheckpoint_1 = true;
                GameManager._pastCheckpoint_2 = false;
                
            }
            else
            {
                GameManager._pastCheckpoint_2 = true;
                GameManager._pastCheckpoint_1 = false;
                
            }
                
        }
    }
}

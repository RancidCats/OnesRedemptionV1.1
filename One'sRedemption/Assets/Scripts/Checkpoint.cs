using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{

    [SerializeField] Light _GargoyleEye1;
    [SerializeField] Light _GargoyleEye2;
    [SerializeField] Light _GargoyleEye3;
    [SerializeField] Light _GargoyleEye4;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(this.name == "Checkpoint_1")
            {
                GameManager._pastCheckpoint_1 = true;
                GameManager._pastCheckpoint_2 = false;
                _GargoyleEye1.intensity = 40;
                _GargoyleEye2.intensity = 40;
            }
            else
            {
                GameManager._pastCheckpoint_2 = true;
                GameManager._pastCheckpoint_1 = false;
                _GargoyleEye3.intensity = 40;
                _GargoyleEye4.intensity = 40;
            }
            
        }
    }
}

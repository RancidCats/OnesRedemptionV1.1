using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{

    [SerializeField] Light _GargoyleEye1;
    [SerializeField] Light _GargoyleEye2;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.spawnPos = transform.position;
            AudioManager.instance.Play("Sword_Unsheathe");
            _GargoyleEye1.intensity = 40;
            _GargoyleEye2.intensity = 40; 
        }
    }
}

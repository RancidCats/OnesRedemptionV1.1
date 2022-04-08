using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    public Transform player;
    [Tooltip("Posicion relativa de la camara con el personaje.")]
    public Vector3 relativePositionToPlayer;
    public void Start()
    {
    }
    void Update()
    {
        transform.position = player.position + relativePositionToPlayer ;
        
        
    }
}

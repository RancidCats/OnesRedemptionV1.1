using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    public Transform player;
    Vector3 _position;
    public void Start()
    {
         _position = new Vector3 (0,25,-50);
    }
    void Update()
    {
        transform.position = player.position + _position ;
        
        
    }
}

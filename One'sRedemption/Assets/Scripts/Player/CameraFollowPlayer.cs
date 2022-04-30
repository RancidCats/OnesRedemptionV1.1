using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    public Transform target;
    [Tooltip("Posicion relativa de la camara con el personaje.")]
    public Vector3 offset;
    public float smoothSpeed;
   
    void FixedUpdate()
    {
        if (target)
        {
            Vector3 desiredPos = target.position + offset;
            Vector3 smoothedPos = Vector3.Lerp(transform.position, desiredPos, smoothSpeed * Time.deltaTime);

            transform.position = smoothedPos;
        }
    }
}

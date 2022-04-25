using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement
{
    Transform    _transform;
    Rigidbody    _rb;
    LayerMask    _layerMask;
    float        _moveSpeed,
                 _jumpForce,
                 _smoothVelocity,
                 _smoothTime;
    Walking      _walking;
    public Movement(Transform t, Rigidbody rb, float s, float jf,LayerMask _layerMask)
    {
        _transform = t;
        _rb = rb;
        _moveSpeed = s;
        _jumpForce = jf;

        _smoothTime = 0.05f;
        _walking = new Walking();
        
    }
    public void MovePlayer(Vector3 inputVector)
    {
        if (inputVector.x !=0 || inputVector.z != 0)
        {
            Player.instance.moving = true; //Pj en movimiento

            _walking.Walk( inputVector,_transform,_smoothTime,_smoothVelocity,_moveSpeed,_rb);
        }
        else
        {
            Player.instance.moving = false;
        }

    }
   
    public void Jump()
    {                                 
        _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);                  
    }
   public void Dash()
    {
         new Dash(_rb,_transform,_layerMask);
    }

 

   
}

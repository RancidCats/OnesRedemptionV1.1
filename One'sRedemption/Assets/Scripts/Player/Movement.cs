using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement
{
    Transform _transform;
    Rigidbody _rb;
    LayerMask _layerMask;
    float _moveSpeed,
          _jumpForce;                

    bool _walkPlaying;
    public Movement(Transform t, Rigidbody rb, float s,LayerMask _layerMask)
    {
        _transform = t;
        _rb = rb;
        _moveSpeed = s;


    }
    public void RotatePlayer(Vector3 _inputVector)
    {
        if (_inputVector.x != 0 || _inputVector.z != 0)
        {
            
            Vector3 dir = new Vector3(_inputVector.x, 0, _inputVector.z).normalized;
            float targetAngle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
            _transform.rotation = Quaternion.Euler(0, targetAngle, 0);

        }
    }
    public void MovePlayer(Vector3 _inputVector)
    {
        if (_inputVector.x !=0 || _inputVector.z != 0)
        {
            Player.instance.moving = true; //Pj en movimiento
            Vector3 dir = new Vector3(_inputVector.x, 0, _inputVector.z).normalized;
            _rb.MovePosition(_rb.position + dir * _moveSpeed * Time.deltaTime);
        }
        else
        {
            Player.instance.moving = false;
        }

        if (Player.instance.moving && !_walkPlaying)
        {
            AudioManager.instance.Play("Player_Step_1");

            _walkPlaying = true;
        }
        else if (!Player.instance.moving && _walkPlaying)
        {
            AudioManager.instance.Stop("Player_Step_1");
            _walkPlaying = false;
        }

    }
   
   //public void Jump()
   //{                                 
   //    _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);                  
   //}
    public void Attack()
    {

    }
   public void Dash()
    {
        new Dash(_rb,_transform,_layerMask);
        Player.instance.startDashCD = true;

    }

}

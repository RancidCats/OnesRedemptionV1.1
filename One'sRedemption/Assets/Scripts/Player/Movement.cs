using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement
{
    Transform    _transform;
    Rigidbody    _rb;
    
    float        _moveSpeed,
                 _jumpForce,
                 _smoothVelocity,
                 _smoothTime;
    public Movement(Transform t, Rigidbody rb, float s, float jf)
    {
        _transform = t;
        _rb = rb;
        _moveSpeed = s;
        _jumpForce = jf;

        _smoothTime = 0.1f;                
    }
    public void MovePlayer(Vector3 inputVector)
    {
        if (inputVector.x !=0 || inputVector.z != 0)
        {
            Player.instance.moving = true; //Pj en movimiento

            Vector3 dir = new Vector3(inputVector.x, 0, inputVector.z).normalized;
            float targetAngle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(_transform.eulerAngles.y, targetAngle, ref _smoothVelocity, _smoothTime);
            _transform.rotation = Quaternion.Euler(0, angle, 0);
            _rb.MovePosition(_rb.position + dir * _moveSpeed * Time.deltaTime);
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
        
        RaycastHit hit;
        if( Physics.Raycast(_transform.position, _transform.forward,out hit,Player.instance.dashLenght, 1 << LayerMask.NameToLayer("Platform")))
        {
            Debug.Log("Dash con choque");
            _rb.MovePosition(_rb.position +(_transform.forward * hit.distance) * Player.instance.dashSpeed);
        }
        else
        {
            Debug.Log("Dash completo");
            _rb.MovePosition(_rb.position + (_transform.forward * Player.instance.dashLenght) * Player.instance.dashSpeed);

        }




    }

 

   
}

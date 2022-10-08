using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash
{

    public Dash(Rigidbody _rb, Transform _transform)
    {
        
        RaycastHit hit;
        if (Physics.Raycast(_transform.position, _transform.forward, out hit, Player.instance.dashLenght, 1 << LayerMask.NameToLayer("Platform")))
        {
          
            _rb.MovePosition(_rb.position + (_transform.forward * hit.distance) * Player.instance.dashSpeed);
        }
        else
        {
            
            _rb.MovePosition(_rb.position + (_transform.forward * Player.instance.dashLenght) * Player.instance.dashSpeed);

        }
        //AudioManager.instance.Play("Dash");
    }
}

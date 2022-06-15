using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash
{

    public Dash(Rigidbody _rb, Transform _transform, LayerMask _layerMask )
    {
        RaycastHit hit;
        if (Physics.Raycast(_transform.position, _transform.forward, out hit, Player.instance.dashLenght, 1 << _layerMask))
        {
          
            _rb.MovePosition(_rb.position + (_transform.forward * hit.distance) * Player.instance.dashSpeed);
        }
        else
        {
            
            _rb.MovePosition(_rb.position + (_transform.forward * Player.instance.dashLenght) * Player.instance.dashSpeed);

        }

    }
}

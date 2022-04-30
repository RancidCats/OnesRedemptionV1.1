using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash
{

    public Dash(Rigidbody _rb, Transform _transform, LayerMask _layerMask )
    {
        RaycastHit hit;
        if (Physics.Raycast(_transform.position, _transform.forward, out hit, Player.instance.dashLenght, 1 << LayerMask.NameToLayer("Platform")))
        {
            Debug.Log("Dash con choque");
            _rb.MovePosition(_rb.position + (_transform.forward * hit.distance) * Player.instance.dashSpeed);
        }
        else
        {
            Debug.Log("Dash completo");
            _rb.MovePosition(_rb.position + (_transform.forward * Player.instance.dashLenght) * Player.instance.dashSpeed);

        }

    }
}
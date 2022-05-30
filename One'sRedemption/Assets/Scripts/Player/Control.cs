using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control
{
    
    
    
    Vector3           _inputVector;
    Movement          _movement;
    AnimationsManager _animations;


   
    public Control(Movement movement, AnimationsManager animations)
    {
        _movement = movement;
        _animations = animations;
    }
    public void ArtificialUpdate()
    {
        _inputVector.x = Input.GetAxis("Horizontal");
        _inputVector.z = Input.GetAxis("Vertical");
        _movement.RotatePlayer(_inputVector);
        //Movimiento
        if (Player.instance.canMove)
        {          
            _movement.MovePlayer(_inputVector);
        }
       
        //Salto
       // if (Input.GetKeyDown(KeyCode.Space) &&  Player.instance.isGrounded && Player.instance.canJump)
       // {
       //     _movement.Jump();
       //
       // }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {           
            _movement.Attack();
            _animations.Attack();
        }       
        if (Input.GetKeyDown(KeyCode.LeftShift) && Player.instance.canDash && Player.instance.isGrounded)
        {
            _animations.Dash();
            _movement.Dash();
        }
        //Reset Coyote Jump Test Position. (PlayerTestingScene)
        //if (Input.GetKeyDown(KeyCode.C))
          //  Player.instance.transform.position = new Vector3(120, 71, 105);
    }
    
}


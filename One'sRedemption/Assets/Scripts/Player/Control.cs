using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control
{
    
    
    
    Vector3    _inputVector;
    Movement   _movement;
    Animations _animations;



    public Control(Movement movement, Animations animations)
    {
        _movement = movement;
        _animations = animations;
    }
    public void ArtificialUpdate()
    {
        //Movimiento
        if (Player.instance.canMove)
        {
            _inputVector.x = Input.GetAxis("Horizontal");
            _inputVector.z = Input.GetAxis("Vertical");
            _movement.MovePlayer(_inputVector);
        }
       
        //Salto
        if (Input.GetKeyUp(KeyCode.Space) &&  Player.instance.isGrounded && Player.instance.canJump)
        {
            _movement.Jump();

        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            _animations.Attack();
        }       
    }
}


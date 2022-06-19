using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control
{
    
    
    
    Vector3           _inputVector;
    Movement          _movement;
    AnimationsManager _animations;
    bool _walkPlaying;

   
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
            if (_inputVector.x != 0 || _inputVector.z != 0)
            {
                Player.instance.moving = true; //Pj en movimiento
                _movement.MovePlayer(_inputVector);
                if (!_walkPlaying)
                {
                    AudioManager.instance.Play("Player_Step_1");

                    _walkPlaying = true;
                }
            }
            else
            {
                Player.instance.moving = false;
                if (_walkPlaying)
                {
                    AudioManager.instance.Stop("Player_Step_1");
                    _walkPlaying = false;
                }
            }
        }
        else
        {
            if (_walkPlaying)
            {
                AudioManager.instance.Stop("Player_Step_1");
                _walkPlaying = false;
            }
        }
    
       
      
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {           
            _animations.Attack();
        }       

        if (Input.GetKeyDown(KeyCode.LeftShift) && Player.instance.canDash && Player.instance.isGrounded)
        {
            _animations.Dash();
            _movement.Dash();
        }
          
    }
    
}


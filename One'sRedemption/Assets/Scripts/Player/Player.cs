using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player    instance;
   
    public        Rigidbody  rb;
    public        Animator   anim;

    public        float      moveSpeed,
                             jumpForce;

    public bool              moving,
                             canAttack,
                             isGrounded,
                             canJump,
                             canCombo,
                             canMove;
    //------------------------------------------------------------------------------------------------------------------------------------//

    Animations  _animations;
    Movement    _movement;
    Control     _control;
    void Start()
    {
        instance = this;



        _animations = new Animations(anim);
        _movement = new Movement(transform,rb,moveSpeed,jumpForce);
        _control = new Control(_movement,_animations);

        canMove = true;
        canJump = true;
    }

    // Update is called once per frame
    void Update()
    {    
        _control.ArtificialUpdate();
        _animations.ArtificialUpdate();
    }
    
}

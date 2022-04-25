using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;


    public LayerMask layerMask;
    public Rigidbody rb;
    public Animator anim;

    public float maxLife,
                             life,
                             moveSpeed,
                             jumpForce,
                             dashSpeed,
                             dashLenght;

    public bool moving,
                             canAttack,
                             isGrounded,
                             canJump,
                             canCombo,
                             canMove;
    //------------------------------------------------------------------------------------------------------------------------------------//

    Animations _animations;
    Movement    _movement;
    Control     _control;
    public void Awake()
    {
        instance = this;
        life = 10;

    }
    void Start()
    {



        _animations = new Animations(anim);
        _movement = new Movement(transform,rb,moveSpeed,jumpForce,layerMask);
        _control = new Control(_movement,_animations);

        canMove = true;
        canJump = true;
    }

    // Update is called once per frame
    void Update()
    {    
        _control.ArtificialUpdate();
        _animations.ArtificialUpdate();
        Death();

    }
    public void Death()
    {
        if (life <= 0)
        {
            Destroy(this);
        }
    }
    public void TakeDamage(float damageTaken)
    {
        life -= damageTaken;
    }
   
}

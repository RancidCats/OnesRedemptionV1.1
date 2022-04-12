using UnityEngine;

public class Animations
{
    Animator _anim;
    
    public Animations(Animator anim)
    {
        _anim = anim;
    }  
    public void ArtificialUpdate()
    {
        Moving();
    }
    public void Moving()
    {
        _anim.SetBool("moving", Player.instance.moving);       
    }
    public void Attack()
    {
        if (Player.instance.isGrounded)
        {
            if (Player.instance.canCombo)
            {
                _anim.SetTrigger("combo");

            }
            else
            {
                if (Player.instance.canAttack)
                {
                    _anim.SetTrigger("attack");
                }
            }
        }
        
    }
    public void Dash()
    {
        _anim.SetTrigger("dash");
    }

}

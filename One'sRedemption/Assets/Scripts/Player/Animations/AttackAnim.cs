using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAnim
{
    
    

    public AttackAnim(Animator _anim)
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
}

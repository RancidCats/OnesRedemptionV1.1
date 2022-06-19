using UnityEngine;

public class AnimationsManager
{
    [Header("Player Animator")]
    Animator _anim;
    
    public AnimationsManager(Animator anim)
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
        
        new AttackAnim(_anim);
        
    }
    public void Dash()
    {
       
        _anim.SetTrigger("dash");
    }

}

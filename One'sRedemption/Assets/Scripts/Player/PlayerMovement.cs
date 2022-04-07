using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    public  Rigidbody        myRb;
    public Animator          anim;

    
    public  float            moveSpeed,
                             rotationSpeed,
                             jumpSpeed,
                             smoothTime,
                             smoothVelocity;

    private Vector3          inputVector;

    public bool              canJump,
                             canMove;

 
    void Update()
    {
        
        AttackLogic();
        JumpLogic();
        if (canMove)
        {
            MovementLogic();
        }
        else
        {

            anim.SetBool("moving", false);
            
        }

    }
    private void FixedUpdate()
    {
        if (States("Idle"))
        {
            anim.ResetTrigger("combo");

        }
        if (inputVector.magnitude > 0 && canMove)
        {
            anim.SetBool("moving", true);

            Vector3 dir = new Vector3(inputVector.x, 0, inputVector.z).normalized;
            float targetAngle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref smoothVelocity, smoothTime);
            transform.rotation = Quaternion.Euler(0, angle, 0);
            myRb.MovePosition(myRb.position + 
                                    dir 
                                    * moveSpeed * Time.deltaTime);
            //anim.SetFloat("velX", inputVector.x);
            //anim.SetFloat("velY", inputVector.z);
        }
        else
        {
            anim.SetBool("moving", false);
        }



    }
    public bool States(string currentAnimation)
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName(currentAnimation))
        {        
            return true;
        }
        else
        {
            return false;
        }
    

    }
    public void AttackLogic()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (canMove)
            {
                
                    anim.SetTrigger("Attack");

                    canJump = false;
                    canMove = false;
                

            }
            else
            {
                if (!States("Idle") && !canMove)
                {
                    anim.SetTrigger("combo");
                }
            }
           
             
             
           
        }

    }
    public void JumpLogic()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (canJump)
            {
                myRb.AddForce(new Vector3(0, jumpSpeed, 0), ForceMode.Impulse);

            }
        }
    }
    public void MovementLogic()
    {
        
        inputVector.x = Input.GetAxis("Horizontal");
        inputVector.z = Input.GetAxis("Vertical");
        inputVector.y = 0;
        
       
        
    }

}

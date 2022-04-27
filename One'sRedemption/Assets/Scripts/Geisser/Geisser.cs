using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Geisser : MonoBehaviour
{
    public Animator anim;  
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerFeets"))
        {
            anim.SetTrigger("active");
            Debug.Log("EstalloElgeisser");
        }
    }  
   public void OnTriggerExit(Collider other)
   {
       if (other.CompareTag("PlayerFeets"))
       {
           Debug.Log("SeApagoElgeisser");
            anim.SetTrigger("end");

       }
   }
}

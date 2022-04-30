using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VanishingPlatform : MonoBehaviour
{
    [SerializeField] float _disappearTime = 3; //Tiempo que tarda en desaparecer la plataforma

    Animator _myAnim;

    [SerializeField] bool _canReset; //Booleano para saber si debe resetear
    [SerializeField] float _resetTime; //Tiempo que tarda en resetear

    //En el Start inicializo el animator y seteo el tiempo de desaparición
    private void Start()
    {
        _myAnim = this.GetComponent<Animator>();
        _myAnim.SetFloat("DisappearTime", 1 / _disappearTime); //Divido uno sobre el tiempo de desaparición ya que la animación dura 1 seg y la velocidad de la animación es también 1
    }

    //Si el player colisiona con la plataforma comienza a desvanecerse
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            _myAnim.SetBool("VanishTrigger", true);
        }
    }

    //Creo una función para resetear el trigger mediante una co-rutina
    public void TriggerReset()
    {
        if(_canReset)
        {
            StartCoroutine(Reset());
        }
    }

    //Espero el tiempo de reseteo dado y cambio el valor del booleano para volver al estado inicial de la animación
    IEnumerator Reset()
    {
        yield return new WaitForSeconds(_resetTime);
        _myAnim.SetBool("VanishTrigger", false);
    }

}

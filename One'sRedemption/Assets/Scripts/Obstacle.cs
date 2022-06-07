using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour , IDamageable
{
    //CODIGO EXACTAMENTE IGUAL AL DE ENTITY PERO QUITANDO LAS BARRAS DE VIDA

    [SerializeField] int _maxHp;
    [SerializeField] int _currHp;
    [SerializeField] ParticleSystem _boxHitted;
    [SerializeField] ParticleSystem _boxDeath;

    private void Start()
    {
        _currHp = _maxHp;
    }


    public int Health
    {
        get
        {
            return _currHp;
        }
    }
    public int MaxHealth
    {
        get
        {
            return _maxHp;
        }
    }

   public void DecreaseHealth(int value)
   {
       
        if (!_boxHitted.isPlaying)
        {
            _boxHitted.Play();
        }
        else
        {
            _boxHitted.Stop();
            _boxHitted.Play();

        }
        _currHp -= value;
        if (_currHp <= 0)
        {
            Instantiate(_boxDeath, new Vector3(1.08f, 0.56f, 0), transform.rotation);

            if (_boxHitted.isPlaying)
            {
                _boxHitted.Stop();
            }
            _boxDeath.Play();


                gameObject.SetActive(false);
        }
   }
   public void IncreaseHealth(int value)
   {

   }
}

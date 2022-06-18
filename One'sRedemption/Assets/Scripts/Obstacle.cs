using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour , IDamageable
{
    //CODIGO EXACTAMENTE IGUAL AL DE ENTITY PERO QUITANDO LAS BARRAS DE VIDA

    [SerializeField] int _maxHp;
    [SerializeField] int _currHp;
    [SerializeField] ParticleSystem _boxHitParticles;
    [SerializeField] GameObject     _boxDeathParticles;

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
        _currHp -= value;
        if (_currHp <= 0)
        {


            if (_boxHitParticles.isPlaying)
            {
                _boxHitParticles.Stop();
            }
            var deathParticles = Instantiate(_boxDeathParticles);
            deathParticles.transform.position = transform.position + new Vector3 (1,1,0);
            
            gameObject.SetActive(false);
        }
        if (!_boxHitParticles.isPlaying)
        {
            _boxHitParticles.Play();
        }
        else
        {
            _boxHitParticles.Stop();
            _boxHitParticles.Play();

        }
       
   }
   public void IncreaseHealth(int value)
   {

   }
}

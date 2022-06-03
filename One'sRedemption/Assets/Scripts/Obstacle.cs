using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour , IDamageable
{
    //CODIGO EXACTAMENTE IGUAL AL DE ENTITY PERO QUITANDO LAS BARRAS DE VIDA

    [SerializeField] int _maxHp;
    [SerializeField] int _currHp;

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
        Debug.Log("DustParticles");
        _currHp -= value;
        if (_currHp <= 0)
        {
            Debug.Log("destroyParticles");

            gameObject.SetActive(false);
        }
   }
   public void IncreaseHealth(int value)
   {

   }
}

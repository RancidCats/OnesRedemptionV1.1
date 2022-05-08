using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    //CODIGO EXACTAMENTE IGUAL AL DE ENTITY PERO QUITANDO LAS BARRAS DE VIDA

    [SerializeField] int _maxHp;
    [SerializeField] int _currHp;

    public int health
    {
        get
        {
            return _currHp;
        }
    }
    public int maxHealth
    {
        get
        {
            return _maxHp;
        }
    }

    private void Start()
    {
        _currHp = _maxHp;
    }

    public void ModifyHealth(int type, int value)
    {
        switch (type)
        {
            case 0:
                _currHp -= value;
                if (_currHp <= 0)
                {
                    gameObject.SetActive(false);
                    //play sounds etc
                }
                break;
            case 1:
                _currHp += value;
                if (_currHp > _maxHp)
                {
                    _currHp = _maxHp;
                }
                break;
        }
    }
}

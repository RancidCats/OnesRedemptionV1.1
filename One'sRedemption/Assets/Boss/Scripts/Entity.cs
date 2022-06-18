using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Entity : MonoBehaviour, IDamageable
{
    [SerializeField]
    protected int   _currHp;
    [SerializeField]
    protected int   _maxHp;
    [Header("UI")]
    [SerializeField]
    protected SlideBar _hpBar;
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


    public virtual void IncreaseHealth(int value)
    {
        _currHp += value;
        _hpBar.RefreshBar(_currHp, _maxHp);
        if (_currHp > _maxHp)
        {
            _currHp = _maxHp;
        }
    }

    public virtual void DecreaseHealth(int value)
    {
        _currHp -= value;
        _hpBar.RefreshBar(_currHp, _maxHp);
        if (_currHp <= 0)
        {
            _currHp = 0;
            Death();
        }
    }

    public virtual void Death()
    {
        gameObject.SetActive(false);
        //play sounds etc
    }
}

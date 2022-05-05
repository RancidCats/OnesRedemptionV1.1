using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Entity : MonoBehaviour
{
    [SerializeField]
    protected int   _currHp;
    [SerializeField]
    protected int   _maxHp;
    [Header("UI")]
    [SerializeField]
    protected SlideBar _hpBar;
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

    public void ModifyHealth(int type, int value)
    {
        switch (type)
        {
            case 0:
                _currHp -= value;
                _hpBar.RefreshBar(_currHp, _maxHp);
                if (_currHp <= 0)
                {
                    gameObject.SetActive(false);
                    //play sounds etc
                }
                break;
            case 1:
                _currHp += value;
                _hpBar.RefreshBar(_currHp, _maxHp);
                if (_currHp > _maxHp)
                {
                    _currHp = _maxHp;
                }
                break;
        }
    }
}

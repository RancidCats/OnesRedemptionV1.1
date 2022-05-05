using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Entity : MonoBehaviour
{
    [SerializeField]
    protected int currHp;
    [SerializeField]
    protected int maxHp;
    protected Image hpBar;
    public int health
    {
        get
        {
            return currHp;
        }
    }
    public int maxHealth
    {
        get
        {
            return maxHp;
        }
    }

    public void ModifyHealth(int type, int value)
    {
        switch (type)
        {
            case 0:
                currHp -= value;

                if (currHp <= 0)
                {
                    gameObject.SetActive(false);
                    //play sounds etc
                }
                break;
            case 1:
                currHp += value;

                if(currHp > maxHp)
                {
                    currHp = maxHp;
                }
                break;
        }
        hpBar.fillAmount = currHp / maxHp;
    }
}

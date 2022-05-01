using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float _enemyLife;
    public float decreaseHealth
    {
        get { return _enemyLife; }
        set { _enemyLife -= value; }
    }
}

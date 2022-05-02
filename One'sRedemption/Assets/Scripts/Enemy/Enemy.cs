using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _enemyLife;

    private void Update()
    {
        if(_enemyLife <= 0)
        {
            Destroy(gameObject);
        }
    }
    public float decreaseHealth
    {
        get { return _enemyLife; }
        set { _enemyLife -= value; }
    }
}

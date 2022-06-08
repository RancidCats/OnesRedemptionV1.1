using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceDeath : MonoBehaviour
{
    [SerializeField] float _time;
    public void Update()
    {
        Destroy(gameObject, _time);
    }

}

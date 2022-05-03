using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOEDeath : MonoBehaviour
{
    public GameObject Boss;
    void Start()
    {
        Boss = GameObject.Find("BossModel");
    }
    void Update()
    {
        if (!Boss.activeSelf)
        {
            Destroy(gameObject);
        }
    }
}

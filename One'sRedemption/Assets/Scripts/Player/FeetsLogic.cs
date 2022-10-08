using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeetsLogic : MonoBehaviour
{
    [SerializeField]
    int _contactCount;
    [SerializeField]
    bool _coyoteStart;
    [SerializeField]
    float _coyoteTime;
    [SerializeField]
    float _coyoteCounter;
    public void Update()
    {
        if (_coyoteStart)
        {
            CoyoteJump();
        }
        else
        {
            _coyoteCounter = 0;
        }
    }
    void CoyoteJump()
    {
        if (_coyoteCounter <= _coyoteTime)
        {
            _coyoteCounter += Time.deltaTime;
        }
        else
        {
            Player.instance.isGrounded = false;
            _coyoteStart = false;
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        _contactCount++;
        if (_contactCount >= 2)
        {
            Player.instance.isGrounded = true;
            Player.instance.canJump = true;
            _coyoteStart = false;
        }



    }
    public void OnTriggerExit(Collider other)
    {
        _contactCount--;
        if (_contactCount == 1)
        {
             _coyoteStart = true;
            //Player.instance.isGrounded = false;

        }
    }

}

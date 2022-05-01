using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeisserCollider : MonoBehaviour
{
    bool _playerStanding;
    float _deadTime = 3;
    float _timer;
    public Geisser fatherGeisser;
    public void Update()
    {
        if (_playerStanding)
        {
            PlayerStanding();
        }
    }
    private void PlayerStanding()
    {
        _timer += Time.deltaTime;
        if (_timer <= _deadTime)
        {
            float rest = _timer % 1;
            if (rest <= 0.01)
            {
                 MakeDamage();
            }
        }
        else
        {
            _timer = 0;
            _playerStanding = false;
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            MakeDamage();

        }
    }
    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerStanding = true;

        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerStanding = false;
        }
    }
  

    private void MakeDamage()
    {
        Player.instance.decreaseHealth = fatherGeisser.damage;
        Player.instance.burningOn = true;
    }
}

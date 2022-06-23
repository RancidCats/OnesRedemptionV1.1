using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField] int _baseDamage;
    [SerializeField] int _damage;
    int _hitCounter;
    public GameObject blood;
    public GameObject hit;

    public void ResetHitCounter()
    {
        _hitCounter = 0;
        _damage = _baseDamage;
    }
    public int HitCounter()
    {
        _hitCounter++;
        IncreaseDamage();
        return _hitCounter;
    }
    void IncreaseDamage()
    {
        _damage =(int)(_damage + (_damage * _hitCounter * 0.125f));

    }

    public void OnTriggerEnter(Collider other)
    {   
        if (other.GetComponentInParent<IDamageable>() != null)//Pregunto por el player porque sino le hace daño
        {
            
           other.GetComponentInParent<IDamageable>().DecreaseHealth(_damage);
        }
        if (other.CompareTag("Untagged") && !GetComponentInChildren<CapsuleCollider>())
        {
            Instantiate(hit, transform.position, Quaternion.identity);           
        }
        if (other.GetComponentInChildren<CapsuleCollider>())
        {
            Instantiate(blood, transform.position, Quaternion.identity);
            AudioManager.instance.Play("Sword_Attack_3");
        }
    }

}

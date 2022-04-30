using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;


    public LayerMask layerMask;
    public Rigidbody rb;
    public Animator anim;

    public float             damage,    
                             maxLife,
                             moveSpeed,
                             jumpForce,
                             dashSpeed,
                             dashLenght;
    public bool moving,
                             canAttack,
                             isGrounded,
                             canJump,
                             canCombo,
                             canMove;
    
    //------------------------------------------------------------------------------------------------------------------------------------//
    [SerializeField]
    float       _life;
    [SerializeField]
    int         _hitCounter;
    float       _firstDamage;
    Animations  _animations;
    Movement    _movement;
    Control     _control;
    public void Awake()
    {
        instance = this;
    }
    void Start()
    {

        _firstDamage = damage;
        _life = maxLife;
        _animations = new Animations(anim);
        _movement = new Movement(transform, rb, moveSpeed, jumpForce, layerMask);
        _control = new Control(_movement, _animations);

        canMove = true;
        canJump = true;
    }

    // Update is called once per frame
    void Update()
    {
        _control.ArtificialUpdate();
        _animations.ArtificialUpdate();
        Death();

    }
    public void Death()
    {
        if (_life <= 0)
        {
            Destroy(gameObject);
        }
    }
    public void ResetHitCounter()
    {
        _hitCounter = 0;
        damage = _firstDamage;
    }
    public int HitCounter()
    {
        _hitCounter++;
        IncreaseDamage();
        return _hitCounter;
    }
    public void IncreaseDamage()
    {
        damage = damage + (damage *_hitCounter * 0.125f);
        Debug.Log("Maken: " + damage);
    }

    public float decreaseHealth
    {
        get
        {
            return _life;
        }
        set
        {
            _life -= value;
        }
    }
    public float increaseHealth
    {
        get
        {
            return _life;

        }
        set
        {
            _life += value;
        }
    }



   
}

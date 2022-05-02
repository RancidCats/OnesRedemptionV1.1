using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;


    public LayerMask layerMask;
    public Rigidbody rb;
    public Animator anim;

    public float damage,
                             maxLife,
                             moveSpeed,
                             jumpForce,
                             dashSpeed,
                             dashLenght,
                             dashTimer,
                             dashDeadTime;
    public bool              burningOn;

    public bool              moving,
                             canDash,
                             startDashCD,
                             canAttack,
                             isGrounded,
                             canJump,
                             canCombo,
                             canMove;

    //------------------------------------------------------------------------------------------------------------------------------------//
    [SerializeField]
    float           _life;
    [SerializeField]
    int             _hitCounter;
    float           _firstDamage;
    Animations      _animations;
    Movement        _movement;
    Control         _control;
    [SerializeField ]
    float _burningCD;
    [SerializeField]
    float _burningTimer;
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
        if (burningOn == true)
        {
            BurningTimer();
        }
        if (startDashCD)
        {
            DashTimer();
        }
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
    private void DashTimer()
    {
        dashTimer += Time.deltaTime;
        if (dashTimer <= dashDeadTime)
        {
            canDash = false;
        }
        else
        {
            canDash = true;
            dashTimer = 0;
            startDashCD = false;
        }

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
    public float health
    {
        get
        {
            return _life;
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
    private void BurningTimer()
    {
        if (_burningTimer <= _burningCD)
        {
            _burningTimer+=Time.deltaTime;
            float rest = _burningTimer % 1;
            if (rest <= 0.01f)
            {

                decreaseHealth = 1;

            }
        }
        else
        {
            _burningTimer = 0;
            burningOn = false;

        }

    }
    
    public void MakeASlashAudio()
    {
        AudioManager.instance.Play("Sword_Attack_1");
    }    
}

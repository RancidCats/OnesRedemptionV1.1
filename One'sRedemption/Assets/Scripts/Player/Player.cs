using System.Collections;
using UnityEngine;

public class Player : Entity
{
    public static Player instance;

    [Header("Guarda la layer en la que trabaja el RayCast")]
    public LayerMask layerMask;

    public Rigidbody rb;
    public Animator anim;

    public float             damage,                            
                             moveSpeed,
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
                             canCombo,
                             canMove;

    //------------------------------------------------------------------------------------------------------------------------------------// 

    [SerializeField]
    int                   _hitCounter;
    float                 _firstDamage;
    AnimationsManager     _animations;
    Movement              _movement;
    Control               _control;
    [SerializeField ]
    float                 _burningCD;
    [SerializeField]
    float                 _burningTimer;               
    public void Awake()
    {
       

            instance = this;

        
    }
    void Start()
    {
        _firstDamage = damage;
        _currHp = _maxHp;
        _animations = new AnimationsManager(anim);
        _movement = new Movement(transform, rb, moveSpeed,layerMask);
        _control = new Control(_movement, _animations);
   

        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        _control.ArtificialUpdate();
        _animations.ArtificialUpdate();
        if (burningOn == true)
        {
            BurningTimer();
        }
        if (startDashCD)
        {
            DashTimer();
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
        damage = Mathf.Round(damage + (damage *_hitCounter * 0.125f)) ;
      
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
    private void BurningTimer()
    {
        if (_burningTimer <= _burningCD)
        {
            _burningTimer+=Time.deltaTime;
            float rest = _burningTimer % 1;
            if (rest <= 0.01f)
            {

                DecreaseHealth(1);

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

    sealed protected override void OnTriggerEnter(Collider other)
    {
     
        if (other.CompareTag("BossAttackCollider"))
        {
    
             DecreaseHealth(BossController.instance.damage);
        }           
    }  
}

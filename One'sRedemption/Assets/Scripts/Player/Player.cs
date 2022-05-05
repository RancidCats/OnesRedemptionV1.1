using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;


    public LayerMask layerMask;
    public Rigidbody rb;
    public Animator anim;

    public float             damage,                            
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
    float           _maxLife;
    float           _life;
   
    [SerializeField]
    int             _hitCounter;
    float           _firstDamage;
    AnimationsManager      _animations;
    Movement        _movement;
    Control         _control;
    [SerializeField ]
    float _burningCD;
    [SerializeField]
    float _burningTimer;
            
    [Header("UI")]
    [SerializeField]
    SlideBar _lifeBarUI;
    
    public void Awake()
    {
        instance = this;
    }
    void Start()
    {

        _firstDamage = damage;
        _life = _maxLife;
        _animations = new AnimationsManager(anim);
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
    public void TakeDamage(float _damageTaken)
    {
        _life -= _damageTaken;
        _lifeBarUI.RefreshBar(_life, _maxLife);
        if (_life <= 0)
        {
            Destroy(gameObject);
        }
    }
   
    public float health
    {
        get
        {
            return _life;
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

                TakeDamage(1f);

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
    public void OnTriggerEnter(Collider other)
    {
     
        if (other.CompareTag("BossAttackCollider"))
        {

             TakeDamage(BossController.instance.damage);
        }           
    }  
}

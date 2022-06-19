using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player : Entity
{
    public static Player instance;

    public Sword mySword;
    public Rigidbody rb;
    public Animator anim;

    public float                           
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
    [SerializeField] Image _dashCoolDownImage;
    AnimationsManager      _animations;
    Movement               _movement;
    Control                _control;
    [SerializeField] float _burningCD;
    [SerializeField] float _burningTimer;
    public void Awake()
    {
       

            instance = this;
            
        
    }
    void Start()
    {
        _currHp = _maxHp;
        _animations = new AnimationsManager(anim);
        _movement = new Movement(transform, rb, moveSpeed);
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
        mySword.ResetHitCounter();
    }
    public void HitCounter()
    {
        mySword.HitCounter();
    }
    private void DashTimer()
    {
        dashTimer += Time.deltaTime;

        if (dashTimer <= dashDeadTime)
        {
            
            _dashCoolDownImage.fillAmount = 1 - dashTimer/dashDeadTime;
            canDash = false;
        }
        else
        {

            _dashCoolDownImage.fillAmount = 0;
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
}

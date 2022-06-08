using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using System;
using Utilidades;
public class BossController : Entity
{
    [Header("Physics & Parts")]

    [SerializeField] private Rigidbody rb;
    [SerializeField] private Animator ani;
    //[SerializeField] private Transform leftHand;
    public static BossController instance;
    private EnemyMove enemyMove;


    [Header("Stats")]
    public int damage;
    public int walkSpeed;

    [Header("Animations")] //sin usar al 2/05
    public bool isAttacking;
    //public bool isMoving;
    //public bool isIdle;



    [Header("Checks")] //public para chequeos globales
    private bool canAttack;
    private bool canMove;
    public bool invulnerable;
    public bool playerAtAttackAngle;
    public bool playerAtMoveRange; //usar con Physics.checksphere

    private static BossStages currentStage;
    public enum BossStages
    {
        Stage_1 = 0,
        Stage_2 = 1,
        Stage_3 = 2
    }

    public Transform playerTarget;

    [Header("Spell checks")]
    //private float spell0timer;
    [SerializeField] private int range;
    //private LayerMask playerLayer;
    private bool[] canUseSpell = new bool[3];
    private bool[] isUsingSpell = new bool[3];
    private float[] spellTimer = new float[3];

    //spells
    [Header("Spells")]
    [SerializeField]
    private GameObject shockwave;
    //public GameObject fireball;

    //stages






    void Awake()
    {
        playerTarget = Player.instance.transform;
        instance = this;
        enemyMove = new EnemyMove(playerTarget, GetComponent<NavMeshAgent>());
    }
    private void Start()
    {
        _maxHp = 1500;
        _currHp = _maxHp;
        canUseSpell[1] = true;
        EventHandler.OnBossStageChanged += ChangeStage;
    }
    private void FixedUpdate()
    {
        enemyMove.EnemyBehaviour(canMove);
        SpellBehaviour();
        Attack();
        MoveBehaviour();

    }



    //------ATAQUES--------//
    void Attack()
    {
        if (Sistemas.IsAnimationPlaying(ani, "BossSwipe2"))
        {
            isAttacking = true;
        }
        else
        {
            isAttacking = false;
        }
        if (Sistemas.GetDistanceXZ(playerTarget.position, transform.position) < 3f && !canUseSpell[1] && playerAtAttackAngle) //devuelve la posicion sin tener en cuenta el eje Y
        {
            canAttack = true;
        }
        else canAttack = false;
        if (canAttack)
        {
            ani.SetTrigger("Attack");

        }
        else ani.ResetTrigger("Attack"); // para que no quede colgado algun trigger



    }

    //-----MOVIMIENTO------//
    void MoveBehaviour()
    {
        //Vector3 lookPos = PlayerTarget.transform.position - transform.position; //sacar vector de direccion
        //lookPos.y = 0; //remover y
        //Quaternion rotation = Quaternion.LookRotation(lookPos); //crear una rotacion que mire a ese vector
        //if (transform.rotation == rotation)
        //{
        //    playerAtCorrectAngle = true;
        //}
        //else playerAtCorrectAngle = false;

        //if (Sistemas.GetDistanceXZ(PlayerTarget.position, transform.position) < 30 && canMove == true && 
        //    Sistemas.GetDistanceXZ(PlayerTarget.position, transform.position) > 2.5f) 
        //{
        //   transform.Translate(Vector3.forward * walkSpeed * Time.deltaTime); //movimiento a reemplazar por navmeshagent             
        //}    
        Vector3 lookPos = playerTarget.transform.position - transform.position; //sacar vector de direccion
        lookPos.y = 0; //remover y
        Quaternion rotation = Quaternion.LookRotation(lookPos); //crear una rotacion que mire a ese vector
        if (transform.rotation == rotation)
        {
            playerAtAttackAngle = true;
        }
        else
        {
            playerAtAttackAngle = false;
        }
        if (!playerAtAttackAngle && canMove) transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 10);

        if (isAttacking || Sistemas.IsAnimationPlaying(ani, "BossJumpStart") ||
            Sistemas.IsAnimationPlaying(ani, "BossJumpIdle") || Sistemas.IsAnimationPlaying(ani, "BossJumpFinish") ||
            Sistemas.GetDistanceXZ(playerTarget.position, transform.position) > range) // si se esta ejecutando alguna de estas animaciones, que no se pueda mover
        {
            canMove = false;
        }
        else
        {
            canMove = true;
        }
        if (canMove)
        {
            ani.SetBool("Walk", true);
            //play sonido de caminar boss
        }
        else ani.SetBool("Walk", false);
    }

    void SpellBehaviour() // falta incorporar uso de fases
    {
        if (!canUseSpell[1] && spellTimer[1] <= 7 && !Sistemas.IsAnimationPlaying(ani, "BossJumpStart") &&
            !Sistemas.IsAnimationPlaying(ani, "BossJumpFinish") && !Sistemas.IsAnimationPlaying(ani, "BossJumpIdle")) //si no se esta ejecutando alguna de estas animaciones, que empiece el timer
        {
            spellTimer[1] += Time.deltaTime;
        }
        if (!isAttacking)
        {
            if (Sistemas.GetDistanceXZ(transform.position, playerTarget.position) < 10f && canUseSpell[1])
            {
                Spells(1); //si esta a 10 metros de distancia, saltar hacia el player
            }
        }
        if (spellTimer[1] >= 7) //resetear el timer
        {
            canUseSpell[1] = true;
            spellTimer[1] = 0;
        }
    }

    void Spells(int spellType)
    {
        switch (spellType)
        {
            //case 0:
            //    if (canUseSpell0) //ataque que gira
            //    {
            //        canUseSpell0 = false;
            //        ani.SetTrigger("360Attack");
            //        //isUsingSpell0 = true;
            //    }
            //    break;
            case 1:
                if (canUseSpell[1])
                {
                    canUseSpell[1] = false;
                    ani.SetTrigger("JumpAttack"); //ataque de salto
                }
                break;
        }
    }

    IEnumerator JumpAttack()
    {
        isUsingSpell[1] = true;
        transform.LookAt(playerTarget);
        Vector3 targetPos = playerTarget.position; //posicion instantanea del player
        float timer = 0;
        Vector3 attackPos = Aoe.GetYPointFromTransform(playerTarget);
        GameObject go = Aoe.CreateAreaOfEffect(attackPos, 2); //crear area de efecto en base a la posicion del player, ver definiciones para entender

        rb.AddForce(Vector3.up * 6, ForceMode.Impulse); //dar impulso para sensacion de salto
        if (Sistemas.GetDistanceXZ(transform.position, targetPos) > 1.2f) //si no esta cerca
        {
            while (Sistemas.GetDistanceXZ(transform.position, targetPos) >= 1.2f)
            {
                timer += Time.fixedDeltaTime;
                transform.position = Vector3.Lerp(transform.position, targetPos, timer / 10); //mover linealmente desde la posicion actual del player hacia la posicion del target, dividido por 10 porque es rapidisimo sino

                if (Sistemas.GetDistanceXZ(transform.position, targetPos) < 1.2f && !ani.GetBool("JumpAttackFinish"))
                {
                    ani.SetTrigger("JumpAttackFinish");
                }

                yield return new WaitForFixedUpdate();//esperar los frames de fixedDeltaTime
            }
            if (!ani.GetBool("JumpAttackFinish") && Sistemas.GetDistanceXZ(transform.position, targetPos) < 1.2f)
            {
                ani.SetTrigger("JumpAttackFinish");
            }
        }
        else // si no esta cerca, esperar sin mover
        {
            yield return new WaitForSecondsRealtime(0.6f); //duracion de la animacion en la que deberia c

            ani.SetTrigger("JumpAttackFinish");
        }
        //ani.SetTrigger("JumpAttackFinish");
        Destroy(go);
        GameObject go2 = Aoe.CreateAoeCollider(attackPos, 2); //crear area de efecto con collider en la posicion del player
        GameObject shockwave = Instantiate(this.shockwave, go.transform.position + Vector3.up * 0.3f, Quaternion.identity); //particle system de shockwave
        yield return new WaitForSecondsRealtime(0.25f);
        Destroy(go2);
        Destroy(shockwave, 0.8f);
        isUsingSpell[1] = false;
    }

    IEnumerator BasicAttack(int attackType)
    {
        Vector3 attackPos = transform.position + transform.forward * 3.5f; //a ojo, posicion del area de efecto del ataque, si no le agrego transform.forward * 3 se instancia justo abajo del boss
        transform.LookAt(attackPos);
        Quaternion attackRota = transform.rotation;
        GameObject go = Aoe.CreateAreaOfEffect(attackPos, attackType, attackRota); // creo la area de efecto (sin collider) en tal posicion
        yield return new WaitForSecondsRealtime(1.36f); // 1 segundo, 30 frames ==> 11 frames = 0.36
        Destroy(go);
        GameObject go2 = Aoe.CreateAoeCollider(attackPos, attackType, attackRota); // creo la area de efecto con collider en tal posicion
        yield return new WaitForSecondsRealtime(0.3f);
        Destroy(go2);
    }


    void StageBehaviour()
    {
        if (_currHp <= 950 && _currHp >= 450) EventHandler.BossStageChange(ref currentStage);
        if (_currHp < 450) EventHandler.BossStageChange(ref currentStage);
    }


    public void ChangeStage(ref BossStages stage)
    {
        stage++;
        switch (stage)
        {
            case (BossStages) 1:
                //empiecen a caer estalactitas
                //habilitar ataque 360
                //dar mas feedback, cambiar colores
                break;
            case (BossStages) 2:
                //habilitar caniones de fuego
                //habilitar pelea cercana
                break;
        }
    }

    public override void DecreaseHealth(int value)
    {
        _currHp -= value;
        _hpBar.RefreshBar(_currHp, _maxHp);
        StageBehaviour();
        if (_currHp <= 0)
        {
            _currHp = 0;
            Death();
        }
    }
}

    

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
using Utilidades;
public enum BossStages
{
    Stage_1 = 0,
    Stage_2 = 1,
    Stage_3 = 2
}
public class BossController : Entity
{
    [Header("Physics & Parts")]
    #region
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Animator ani;
    //[SerializeField] private Transform leftHand;
    public static BossController instance;
    private EnemyMove enemyMove;
    private List<GameObject> attackObjects = new List<GameObject>();
    #endregion
    #region
    [Header("Stats")]
    public int damage;
    public int walkSpeed;

    [Header("Animations")]
    public bool isAttacking;
    public bool isJumping;
    public GameObject bossDead;
    //public bool isMoving;
    //public bool isIdle;
    #endregion

    [Header("Checks")] //public para chequeos globales
    #region
    [SerializeField] private bool[] canAttack = new bool[2];
    [SerializeField] private bool[] canUseAttack = new bool[2];
    [SerializeField ] private bool[] isUsingAttack = new bool[2];
    private float attack2timer;
    [SerializeField] private bool canMove;
    public bool invulnerable;
    [SerializeField] private bool[] playerAtAttackAngle = new bool[2];
    public Transform playerTarget;
    #endregion

    //stages
    #region
    private static BossStages currentStage;
    public bool bossStage1passed = false;
    public bool bossStage2passed = false;
    #endregion

    [Header("Spell checks")]
    #region
    [SerializeField] private int range;
    [SerializeField] private bool[] canUseSpell = new bool[2];
    private bool[] isUsingSpell = new bool[2];
    private float spellTimer;

    [SerializeField]private GameObject shockwave;
    #endregion

    void Awake()
    {
        if (Player.instance != null)
        {
            playerTarget = Player.instance.transform;
        }
        instance = this;
        enemyMove = new EnemyMove(playerTarget, GetComponent<NavMeshAgent>());
    }
    private void Start()
    {
        if (Player.instance != null)
        {
            playerTarget = Player.instance.transform;
        }
        _maxHp = 1500;
        _currHp = _maxHp;
        EventHandler.BossStageHandler += ChangeStage;
        canUseSpell[0] = true;
        canUseAttack[0] = false;
        currentStage = 0;
    }
    private void FixedUpdate()
    {
        enemyMove.EnemyBehaviour(canMove);
        SpellBehaviour();
        AttackBehaviour();
        MoveBehaviour();
    }



    //------ATAQUES--------//
    void AttackBehaviour()
    {
        #region chequeos
        if (Sistemas.IsAnimationPlaying(ani, "BossSwipe2") || Sistemas.IsAnimationPlaying(ani, "BossAttack270"))
            isAttacking = true;
        else
            isAttacking = false;

        if (Sistemas.IsAnimationPlaying(ani, "BossAttack270")) isUsingAttack[1] = true;
        else isUsingAttack[1] = false;
        if (Sistemas.IsAnimationPlaying(ani, "'BossSwipe2")) isUsingAttack[0] = true;
        else isUsingAttack[0] = false;
        #endregion
        #region attack1

        if (Sistemas.GetDistanceXZ(playerTarget.position, transform.position) < 3f && !canAttack[1] && !canUseSpell[0] && !isJumping
            && playerAtAttackAngle[0] && !invulnerable) //Sistemas.GetDistanceXZ() devuelve la distancia sin tener en cuenta el eje Y
            canAttack[0] = true;
        else canAttack[0] = false;
        if (!canAttack[0])
            ani.ResetTrigger("Attack");
        else ani.SetTrigger("Attack");
        #endregion
        #region attack 2 (ataque 270 grados)
        if (canUseAttack[0])
        {
            Vector3 direction = (playerTarget.position - transform.position).normalized;
            if (attack2timer >= 5 && Sistemas.IsAnimationPlaying(ani, "BossSwipe2", 0.5f))
            {
                if (Vector3.Dot(direction, transform.forward) <= 0)
                    playerAtAttackAngle[1] = true;
                else playerAtAttackAngle[1] = false;
            }
            if (!invulnerable && playerAtAttackAngle[1] && Sistemas.IsAnimationPlaying(ani, "BossSwipe2", 0.5f))
                canAttack[1] = true;
            else canAttack[1] = false;
            if (canAttack[1])
            {
                attack2timer = 0;
                ani.SetTrigger("270Attack");
            }
            else
            {
                attack2timer += Time.fixedDeltaTime;
                ani.ResetTrigger("270Attack");
            }
        }
        #endregion
    }
    //-----MOVIMIENTO------//
    void MoveBehaviour()
    {
        #region movimiento viejo
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
        #endregion

        Vector3 lookPos = playerTarget.position - transform.position; //sacar vector de direccion
        lookPos.y = 0; //remover y
        Quaternion rotation = Quaternion.LookRotation(lookPos); //crear una rotacion que mire a ese vector

        if (transform.rotation == rotation)
            playerAtAttackAngle[0] = true;
        else
            playerAtAttackAngle[0] = false;

        if (!playerAtAttackAngle[0] && canMove )
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 10);
        

        if (isAttacking || invulnerable || isJumping || Sistemas.IsAnimationPlaying(ani, "BossJumpFinish") || 
            Sistemas.GetDistanceXZ(playerTarget.position, transform.position) > range) // si esta haciendo algo, que no se pueda mover
            canMove = false;
        else
            canMove = true;

        if (canMove)
        {
            ani.SetBool("Walk", true);
            //play sonido de caminar boss
        }
        else 
            ani.SetBool("Walk", false);
    }

    void SpellBehaviour() // falta incorporar uso de fases
    {
        if (!canUseSpell[0] && spellTimer <= 7 && !isJumping) 
        {
            spellTimer += Time.deltaTime;
        }
        if (!isAttacking && !invulnerable)
        {
            if (Sistemas.GetDistanceXZ(transform.position, playerTarget.position) < 12f && canUseSpell[0])
            {
                Spells(0); //si esta a 10 metros de distancia, saltar hacia el player
            }
        }
        if (spellTimer >= 7) //resetear el timer
        {
            canUseSpell[0] = true;
            spellTimer = 0;
        }
    }

    void Spells(int spellType)
    {
        switch (spellType)
        {
            case 0:
                if (canUseSpell[0])
                {
                    canUseSpell[0] = false;
                    ani.SetTrigger("JumpAttack"); //ataque de salto
                }
                break;
        }
    }
    IEnumerator IntroStage()
    {
        switch (currentStage)
        {
            case (BossStages)1:
                yield return new WaitForSeconds(3);
                ani.SetTrigger("Stand");
                yield return new WaitForSeconds(0.1f);
                yield return new WaitWhile(() => Sistemas.IsAnimationPlaying(ani, "BossCrouchToStand"));
                ani.SetTrigger("Stomp");
                yield return new WaitForSeconds(0.1f);
                yield return new WaitUntil(() => Sistemas.IsAnimationPlaying(ani, "BossStomp", 0.442f)); //27 frames de 61 = 0.442 de 1 (normalizado)
                var spawner = FindObjectOfType<Spawner>();
                spawner.isEnabled = true;
                canUseAttack[0] = true;
                yield return new WaitWhile(() => Sistemas.IsAnimationPlaying(ani, "BossStomp"));
                invulnerable = false;
                break;
            case (BossStages)2:
                yield return new WaitForSeconds(4);
                ani.SetTrigger("Stand");
                yield return new WaitForSeconds(0.1f);
                yield return new WaitWhile(() => Sistemas.IsAnimationPlaying(ani, "BossCrouchToStand"));
                ani.SetTrigger("Roar");
                yield return new WaitUntil(() => Sistemas.IsAnimationPlaying(ani, "BossCannonWarcry", 0.35f)); //duracion animacion a esperar para spawnear los caniones
                CannonManager.instance.SpawnCannons();
                yield return new WaitUntil(() => Sistemas.IsAnimationPlaying(ani, "BossCannonWarcry", 0.85f));
                CannonManager.instance.isEnabled = true;
                yield return new WaitForSeconds(0.1f);
                yield return new WaitWhile(() => Sistemas.IsAnimationPlaying(ani, "BossCannonWarcry"));
                invulnerable = false;
                break;
        }
    }
    IEnumerator JumpAttack()
    {
        isJumping = true;
        isUsingSpell[1] = true;
        transform.LookAt(playerTarget);
        damage = 50;
        Vector3 targetPos = playerTarget.position; //posicion instantanea del player
        float timer = 0;
        Vector3 attackPos = Aoe.GetYPointFromTransform(playerTarget);
        GameObject go = Aoe.CreateAreaOfEffect(attackPos, 2); //crear area de efecto en base a la posicion del player, ver definiciones para entender
        attackObjects.Add(go);
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
            yield return new WaitWhile(() => Sistemas.GetDistanceXZ(transform.position, targetPos) == 0);
        }
        else // si no esta cerca, esperar sin mover
        {
            yield return new WaitForSeconds(0.6f); //duracion de la animacion en la que deberia c
            ani.SetTrigger("JumpAttackFinish");
        }
        //ani.SetTrigger("JumpAttackFinish");
        Destroy(go);
        GameObject go2 = Aoe.CreateAoeCollider(attackPos, 2); //crear area de efecto con collider en la posicion del player
        GameObject shockwave = Instantiate(this.shockwave, go2.transform.position + Vector3.up * 0.3f, Quaternion.identity); //particle system de shockwave
        attackObjects.Add(go2);
        attackObjects.Add(shockwave);
        yield return new WaitForSeconds(0.2f);
        Destroy(go2);
        Destroy(shockwave, 0.6f);
        yield return new WaitForSeconds(1.7f);
        damage = 25;
        isUsingSpell[1] = false;
        isJumping = false;
    }

    IEnumerator BasicAttack(int attackType)
    {
        Vector3 attackPos;
        if (attackType == 0)
        {
            attackPos = transform.position + transform.forward * 3.5f;//a ojo, posicion del area de efecto del ataque, si no le agrego transform.forward * 3 se instancia justo abajo del boss
            transform.LookAt(attackPos);
        }
        else
        {
            Vector3 dir = playerTarget.position - transform.position;
            dir.y = 0;
            var rota = Quaternion.LookRotation(dir);
            while(transform.rotation != rota)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rota, 30);
                if (!Sistemas.IsAnimationPlaying(ani, "BossAttack270")) continue;
                yield return new WaitForFixedUpdate();
            }
            attackPos = new Vector3(transform.position.x, 7.8f, transform.position.z) + transform.forward;
            print($"Aoe position: {attackPos}");
        }
            
        
        
        Quaternion attackRota = transform.rotation;
        GameObject go = Aoe.CreateAreaOfEffect(attackPos, attackType, attackRota); // creo la area de efecto (sin collider) en tal posicion
        attackObjects.Add(go);
        if (attackType == 0)
            yield return new WaitForSeconds(1.36f); // 1 segundo, 30 frames ==> 11 frames = 0.36
        else yield return new WaitForSeconds(0.76666f);
        Destroy(go);
        GameObject go2 = Aoe.CreateAoeCollider(attackPos, attackType, attackRota); // creo la area de efecto con collider en tal posicion
        attackObjects.Add(go2);
        yield return new WaitForSeconds(0.3f);
        Destroy(go2);
    }


    void StageBehaviour()
    {
        if (_currHp <= 950 && !bossStage1passed )
        {
            EventHandler.BossStageChange();
            bossStage1passed = true;
        }
        if (_currHp < 450 && !bossStage2passed)
        {
            EventHandler.BossStageChange();
            bossStage2passed = true;
        }
    }


    public void ChangeStage()
    {
        currentStage++;
        ani.SetTrigger("Wounded");
        StopAttacks();
        invulnerable = true;
        StartCoroutine(IntroStage());
    }

    public override void DecreaseHealth(int value)
    {
        if (!invulnerable)
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

    void StopAttacks()
    {
        foreach(var attack in attackObjects)
        {
            Destroy(attack);
        }
        StopAllCoroutines();
        isJumping = false;
    }
    public override void Death()
    {
        invulnerable = true;
        StopAttacks();
        ani.SetTrigger("Death");
        Vector3 rotation = new Vector3(-90, 0, 0);
        Instantiate(bossDead, transform.position, Quaternion.Euler(rotation));
        //efectos piolas, sonidos
    }
    //IEnumerator TurnTransparent()
    //{
    //    //Material mat = transform.GetComponentInChildren<Material>();
    //    //float timer = 0; 
    //    //while(mat.color.a > 0)
    //    //{
    //    //    timer += Time.fixedDeltaTime * 10;
    //    //    mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, mat.color.a - timer);
    //    //    yield return new WaitForFixedUpdate();
    //    //}
    //    //gameObject.SetActive(false);
    //}
}

    

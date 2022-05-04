using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class BossController : MonoBehaviour
{
    [Header("Physics & Parts")]
    public Rigidbody rb;
    public Animator ani;
    public Transform leftHand;
    public Aoe AoeReference; //despues habria que hacer un namespace, por ahora esto cumple
    public static BossController instance;

    [Header("Stats")]
    [SerializeField]
    private int currentHp;
    [SerializeField]
    private int maxHp;
    public int health
    {
        get
        {
            return currentHp;
        }
        set
        {
           currentHp = value;
        }
    } //por donde se pasa la vida

    

    public int damage;
    public int walkSpeed;
    public Image hpBar;

    [Header("Animations")] //sin usar al 2/05
    public bool isAttacking;
    public bool isMoving;
    public bool isIdle;



    [Header("Checks")]
    public bool canAttack;
    public bool canMove;
    public bool invulnerable;
    public bool playerAtCorrectAngle;
    public bool playerAtMoveRange; //usar con Physics.checksphere
    public float bossPhase;
    public Transform PlayerTarget;

    [Header("Spell checks")]
    private float spell0timer;
    [SerializeField]
    private float spell1timer;
    private float spell2timer;
    private bool canUseSpell0; //360 attack
    [SerializeField]
    private bool canUseSpell1;//jumpattack
    private bool canUseSpell2;
    private bool isUsingSpell0;
    [SerializeField]
    private bool isUsingSpell1;
    private bool isUsingSpell2;

    //spells
    [Header("Spells")]
    public GameObject shockwave;
    public GameObject fireball;


    void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        currentHp = maxHp;
        canUseSpell1 = true;
        AoeReference = new Aoe();
    }
    private void FixedUpdate()
    {
        MoveBehaviour();
        SpellBehaviour();
        Attack();
        HealthBehaviour();

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
        if (Sistemas.GetDistanceXZ(PlayerTarget.position, transform.position) < 2.9f && !canUseSpell1 && playerAtCorrectAngle) //devuelve la posicion sin tener en cuenta el eje Y
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
        Vector3 lookPos = PlayerTarget.transform.position - transform.position; //sacar vector de direccion
        lookPos.y = 0; //remover y
        Quaternion rotation = Quaternion.LookRotation(lookPos); //crear una rotacion que mire a ese vector
        if (transform.rotation == rotation)
        {
            playerAtCorrectAngle = true;
        }
        else playerAtCorrectAngle = false;
        if (!isAttacking && !isUsingSpell1 && !playerAtCorrectAngle && playerAtMoveRange)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 9); //que rote de forma suavizada hacia tal rotacion
        }
            

        if (isAttacking || Sistemas.IsAnimationPlaying(ani, "BossJumpStart") ||
            Sistemas.IsAnimationPlaying(ani, "BossJumpIdle") || Sistemas.IsAnimationPlaying(ani, "BossJumpFinish")
            || Sistemas.GetDistanceXZ(transform.position, PlayerTarget.position) > 30) // si se esta ejecutando alguna de estas animaciones, que no se pueda mover
        {
            canMove = false;
            playerAtMoveRange = false;
        }
        else
        {
            canMove = true;
            playerAtMoveRange = true;
        }

        if (Sistemas.GetDistanceXZ(PlayerTarget.position, transform.position) < 30 && canMove == true && 
            Sistemas.GetDistanceXZ(PlayerTarget.position, transform.position) > 2.5f) 
        {
           transform.Translate(Vector3.forward * walkSpeed * Time.deltaTime); //movimiento a reemplazar por navmeshagent             
        }

        if (canMove)
        {
            ani.SetBool("Walk", true);

            //play sonido de caminar boss
        }
        else
        {
            ani.SetBool("Walk", false);
        }
        
    }

    void SpellBehaviour() // falta incorporar uso de fases
    {
        if (currentHp <= 1500 && currentHp > 1000)
        {
            bossPhase = 0;
        }
        else if (currentHp < 1000 && currentHp > 500)
        {
            bossPhase = 1;
        }
        else
        {
            bossPhase = 2;
        }
        if (!canUseSpell1 && spell1timer <= 7 && !Sistemas.IsAnimationPlaying(ani, "BossJumpStart") &&
            !Sistemas.IsAnimationPlaying(ani, "BossJumpFinish") && !Sistemas.IsAnimationPlaying(ani, "BossJumpIdle")) //si no se esta ejecutando alguna de estas animaciones, que empiece el timer
        {
            spell1timer += Time.deltaTime;
        }
        if (!isAttacking)
        {
            if (Sistemas.GetDistanceXZ(transform.position, PlayerTarget.position) < 10f && canUseSpell1)
            {
                print("so joda");
                Spells(1); //si esta a 10 metros de distancia, saltar hacia el player
            }
        }
        if (spell1timer >= 7) //resetear el timer
        {
            canUseSpell1 = true;
            spell1timer = 0;
        }
    }

    void Spells(int spellType) 
    {
        switch (spellType)
        {
            case 0:
                if (canUseSpell0) //ataque que gira
                {
                    canUseSpell0 = false;
                    ani.SetTrigger("360Attack");
                    isUsingSpell0 = true;
                }
                break;
            case 1:
                if (canUseSpell1)
                {
                    canUseSpell1 = false;
                    ani.SetTrigger("JumpAttack"); //ataque de salto
                    isUsingSpell1 = true;
                }
                break;
        }
    }

    IEnumerator JumpAttack()
    {
        transform.LookAt(PlayerTarget);
        Vector3 targetPos = PlayerTarget.position; //posicion instantanea del player
        float timer = 0;
        Vector3 attackPos = AoeReference.GetYPointFromTransform(PlayerTarget); 
        GameObject go = AoeReference.CreateAreaOfEffect(attackPos, 2); //crear area de efecto en base a la posicion del player, ver definiciones para entender
        bool animFinished = false;
        rb.AddForce(Vector3.up * 6, ForceMode.Impulse); //dar impulso para sensacion de salto
        if(Sistemas.GetDistanceXZ(transform.position, targetPos) > 1.5f) //si no esta cerca
        {
            while (Sistemas.GetDistanceXZ(transform.position, targetPos) > 1.5f)
            {
                timer += Time.fixedDeltaTime;
                transform.position = Vector3.Lerp(transform.position, targetPos, timer / 10); //mover linealmente desde la posicion actual del player hacia la posicion del target, dividido por 10 porque es rapidisimo sino

                if(Sistemas.GetDistanceXZ(transform.position, targetPos) < 1.5f && !animFinished)
                {
                    animFinished = true;
                    ani.SetTrigger("JumpAttackFinish");
                }

                yield return new WaitForFixedUpdate();//esperar los frames de fixedDeltaTime
            }
        }
        else // si no esta cerca, esperar sin mover
        {
            yield return new WaitForSecondsRealtime(0.74f); //duracion de la animacion en la que deberia c

            ani.SetTrigger("JumpAttackFinish");
        }
        Destroy(go);
        GameObject go2 = AoeReference.CreateAoeCollider(attackPos, 2); //crear area de efecto con collider en la posicion del player
        GameObject shockwave = Instantiate(this.shockwave, go.transform.position + Vector3.up * 0.3f, Quaternion.identity); //particle system de shockwave
        yield return new WaitForSecondsRealtime(0.25f); 
        Destroy(go2);
        Destroy(shockwave, 0.8f);
        isUsingSpell1 = false;
    }

    IEnumerator BasicAttack(int attackType)
    {
        Vector3 dir = PlayerTarget.position - transform.position;//saco direccion
        Vector3 attackPos = transform.position + transform.forward * 3; //a ojo, posicion del area de efecto del ataque, si no le agrego transform.forward * 3 se instancia justo abajo del boss
        Quaternion attackRota = transform.rotation;
        GameObject go = AoeReference.CreateAreaOfEffect(attackPos, attackType, attackRota); // creo la area de efecto (sin collider) en tal posicion
        yield return new WaitForSecondsRealtime(1.36f); // 1 segundo, 30 frames ==> 11 frames = 0.36
        Destroy(go);
        GameObject go2 = AoeReference.CreateAoeCollider(attackPos, attackType, attackRota); // creo la area de efecto con collider en tal posicion
        yield return new WaitForSecondsRealtime(0.3f);
        Destroy(go2);
    }

    IEnumerator FireballSpell()//ignorar
    {
        
        for (int i = 0; i < 10; i++)
        {
            GameObject fireballGO = Instantiate(fireball, leftHand);
            fireballGO.GetComponent<Fireball>().target = PlayerTarget;
            fireballGO.GetComponent<Fireball>().damage = Random.Range(10, 16);
            yield return new WaitForSecondsRealtime(0.7f);
        }
    }

    void HealthBehaviour()
    {
        if (currentHp <= 0)
        {
            transform.gameObject.SetActive(false);
        }
        if (currentHp > maxHp)
        {
            currentHp = maxHp;
        }
       // hpBar.fillAmount = currentHp / maxHp;

    }
    private void TakeDamage(int value)
    {
        this.currentHp -= value;
        //reproducir sonido, etc
    }
}

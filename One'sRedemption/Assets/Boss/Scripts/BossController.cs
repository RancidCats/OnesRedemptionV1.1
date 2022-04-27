using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossController : MonoBehaviour
{
    [Header("Physics & Parts")]
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private Animator ani;
    [SerializeField]
    private List<GameObject> aoeColliders = new List<GameObject>();
    [SerializeField]
    private List<GameObject> areaOfEffectObjects = new List<GameObject>();
    [SerializeField]
    private Transform leftHand;


    [Header("Stats")]
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
    }


    public int damage;
    [SerializeField]
    private int walkSpeed;
    public Image hpBar;

    [Header("Animations")]
    [SerializeField]
    private bool isAttacking;
    [SerializeField]
    private bool isMoving;
    [SerializeField]
    private bool isIdle;



    [Header("Checks")]
    public bool canAttack;
    public bool canMove;
    public bool invulnerable;
    //public bool playerAtMoveRange;
    //public bool playerAtAttackRange;
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
    private bool isUsingSpell1;
    private bool isUsingSpell2;

    //spells
    [Header("Spells")]
    [SerializeField]
    private GameObject shockwave;
    [SerializeField]
    private GameObject fireball;



    private void Start()
    {
        canUseSpell1 = false;
        spell1timer = 2.5f;
        currentHp = maxHp;
    }
    private void FixedUpdate()
    {
        Attack();
        MoveBehaviour();
        SpellBehaviour();
        HealthBehaviour();
    }

    //------ATAQUES--------//
    void Attack()
    {
        if (Sistemas.GetDistanceXZ(PlayerTarget.position, transform.position) < 2.9f && !canUseSpell1)
        {
            canAttack = true;
        }
        else canAttack = false;
        if (canAttack)
        {
            ani.SetTrigger("Attack");

        }
        else ani.ResetTrigger("Attack");

        if (Sistemas.IsAnimationPlaying(ani, "BossSwipe2"))
        {
            isAttacking = true;
        }
        else isAttacking = false;
    }

    //-----MOVIMIENTO------//
    void MoveBehaviour()
    {
        if (Sistemas.GetDistanceXZ(PlayerTarget.position, transform.position) < 30 && canMove == true)
        {
            Vector3 lookPos = PlayerTarget.transform.position - transform.position;
            lookPos.y = 0;
            Quaternion rotation = Quaternion.LookRotation(lookPos);
            transform.Translate(Vector3.forward * walkSpeed * Time.deltaTime);
            if (transform.rotation != rotation)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, rotation, 0.1f);
            }
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
        if (Sistemas.IsAnimationPlaying(ani, "BossSwipe2") || Sistemas.IsAnimationPlaying(ani, "BossJump2"))
        {
            canMove = false;
        }
        else canMove = true;
    }

    void SpellBehaviour()
    {
        if (currentHp <= 1500 && currentHp > 1000)
        {
            bossPhase = 0;
        }
        else if (currentHp < 1000 && currentHp > 500)
        {
            bossPhase = 1;
            //hacer un sonido/algo
        }
        else
        {
            bossPhase = 2;
            //lo mismo q arriba
        }
        if (!canUseSpell1 && spell1timer <= 5 && !Sistemas.IsAnimationPlaying(ani, "BossJump2"))
        {
            spell1timer += Time.deltaTime;
        }
        if (spell1timer >= 5)
        {
            canUseSpell1 = true;
            spell1timer = 0;
        }
        if (!isAttacking)
        {
            if (Sistemas.GetDistanceXZ(transform.position, PlayerTarget.position) < 10f)
                Spells(1);
        }
        
    }

    void Spells(int spellType)
    {
        switch (spellType)
        {
            case 0:
                if (canUseSpell0)
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
                    ani.SetTrigger("JumpAttack");
                    isUsingSpell1 = true;
                }
                break;
        }
    }


    
    void StartCoroutine(int type)
    {
        switch (type)
        {
            case 0:
                StopAllCoroutines();
                break;
        }
    }

    void FinishCoroutine(int type)
    {
        switch (type)
        {
            case 0:
                StopCoroutine(JumpAttack());
                break;
        }
    }

    IEnumerator JumpAttack()
    {
        float time = 0;
        time += Time.fixedDeltaTime;
        rb.AddForce(Vector3.up * 6, ForceMode.Impulse);
        Vector3 playerPos = new Vector3(PlayerTarget.position.x, transform.position.y, PlayerTarget.position.z);
        while (Sistemas.GetDistanceXZ(transform.position, PlayerTarget.position) > 1)
        {
            transform.position = Vector3.Lerp(transform.position, playerPos, time);
            yield return new WaitForSeconds(0.02f);
        }
    }
    IEnumerator FireballSpell()
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
            //transform.parent.gameObject.SetActive(false);
        }
        if (currentHp > maxHp)
        {
            currentHp = maxHp;
        }
        //hpBar.fillAmount = currentHp / maxHp;

    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        //reproducir sonido, stun quizas? efectos, etc
        print($"Boss has taken {damage}!");
    }

    //----------------------------------SISTEMAS---------------------------------------//

    //---SISTEMA COLLIDER-------//
    public void CreateAreaOfEffect(Vector3 position, int type, Transform parent)
    {
        string path = "";
        switch (type)
        {
            case 0:
                path = "Prefabs/Aoe50";
                break;
            case 1:
                path = "Prefabs/Aoe270";
                break;
            case 2:
                path = "Prefabs/Aoe360";
                break;
        }
        var asset = Resources.Load(path);
        GameObject aoeObject = Instantiate(asset as GameObject, position, Quaternion.identity, parent);
        aoeObject.transform.LookAt(-parent.position); //posibles errores ----> ver pivot 
    }
    public void CreateAoeCollider(Vector3 position, int type, Transform parent)
    {
        string path = "";
        switch (type)
        {
            case 0:
                path = "Prefabs/Arch50";
                break;
            case 1:
                path = "Prefabs/Arch270";
                break;
            case 2:
                path = "Prefabs/Arch360";
                break;
        }
        var asset = Resources.Load(path);
        GameObject aoeObject = Instantiate(asset as GameObject, position, Quaternion.identity, parent);
        aoeObject.transform.LookAt(-parent.position); //posibles errores ----> ver pivots de los objetos
        aoeObject.tag = "BossAttack";
    }

    public void SetCollider(int collider) // rehacer
    {
        switch (collider)
        {
            //ataques
            case 0:
                aoeColliders[collider].SetActive(true);//ataque basico
                break;
            case 1:
                aoeColliders[collider].SetActive(true);//ataque 360
                break;
            case 2:
                aoeColliders[collider].SetActive(true); //ataque salto
                break;

        }

    }
    public void ResetCollider(int collider) //rehacer
    {
        switch (collider)
        {
            case 0:
                aoeColliders[collider].SetActive(false);//ataque basico
                break;
            case 1:
                aoeColliders[collider].SetActive(false); //ataque 360
                break;
            case 2:
                aoeColliders[collider].SetActive(false); //ataque salto
                break;

        }
    }
    public void SetAoe(int attackType)
    {
        areaOfEffectObjects[attackType].gameObject.SetActive(true);
        if (attackType == 2) areaOfEffectObjects[attackType].gameObject.transform.position = PlayerTarget.position;
    }//rehacer
    public void ResetAoe(int attackType)
    {
        areaOfEffectObjects[attackType].gameObject.SetActive(false);
    }//rehacer
}

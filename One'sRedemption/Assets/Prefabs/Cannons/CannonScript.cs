using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonScript : MonoBehaviour
{
    [SerializeField]
    private GameObject cannonBall;
    [SerializeField]
    private Transform cannonSpawn;
    private bool fire;
    [SerializeField]
    private bool playerInRange;
    private bool startCd;
    private float timer;
    public Transform target;

    [SerializeField]
    private float height;
    [SerializeField]
    private float gravity;
    public bool isEnabled;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            fire = false;
            playerInRange = false;
        }
    }
    // Update is called once per frame
    private void Start()
    {
        target = Player.instance.transform;
        GetComponent<SphereCollider>().enabled = true;
    }
    void FixedUpdate()
    {
        Behaviour();
    }
    void Behaviour()
    {
        if (isEnabled)
        {
            if (playerInRange)
            {
                startCd = true;
            }
            if (fire)
            {
                Shoot();
                fire = false;
            }

            if (startCd) timer += Time.fixedDeltaTime;
            if (timer > 3)
            {
                timer = 0;
                startCd = false;
                fire = true;
            }
        }
    }
    void Shoot()
    {
        GameObject cannonBall = Instantiate(this.cannonBall, cannonSpawn.position, Quaternion.identity);
        Rigidbody rb = cannonBall.GetComponent<Rigidbody>();
        var sphere = cannonBall.GetComponent<CannonBall>();
        sphere.damage = 15;
        print($"{gameObject.name} shot a ball!");
        rb.useGravity = true;
        rb.velocity = CalculateVelocity(target.position, cannonSpawn.position, gravity, height).intialVelocity;
    }

    /// <summary>
    /// CalculateVelocity
    /// Calcula la velocidad necesaria en X y en Y para que impacte en el player de forma parabolica.
    /// </summary>
    /// <param name="targetPos"></param>
    /// <param name="zeroReference"></param>
    /// <param name="gravityValue"></param>
    /// <param name="targetHeight"></param>
    /// <returns></returns>

    LaunchData CalculateVelocity(Vector3 targetPos, Vector3 zeroReference, float gravityValue, float targetHeight)
    {
        float yDisplacement = targetPos.y - zeroReference.y;
        Vector3 displacementXZ = new Vector3(targetPos.x - zeroReference.x, 0, targetPos.z - zeroReference.z);

        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravityValue * targetHeight);
        float time = Mathf.Sqrt(-2 * targetHeight / gravityValue) + Mathf.Sqrt(2 * (yDisplacement - targetHeight) / gravityValue);
        Vector3 velocityXZ = displacementXZ / time;
        
        return new LaunchData(velocityXZ + velocityY * -Mathf.Sign(gravity), time);
        
    }

    /// <summary>
    /// struct utilizado para guardar los datos de la velocidad y el tiempo sin que sean modificables.
    /// El proposito de este struct es separar la informacion en un grupo que se puedan leer sus variables pero no escribir/sobreescribir.
    /// </summary>
    struct LaunchData
    {
        
        public readonly Vector3 intialVelocity;
        public readonly float timeToTarget;

        public LaunchData(Vector3 intialVelocity, float timeToTarget)
        {
            this.intialVelocity = intialVelocity;
            this.timeToTarget = timeToTarget;
        }
    }
}

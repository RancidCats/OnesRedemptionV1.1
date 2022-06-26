using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Estalactita : MonoBehaviour
{
    private ParticleSystem particle;
    private MeshRenderer mr;
    private BoxCollider bc;
    private RaycastHit hit;
    public GameObject indicator;
    public float damage;
    public float maxLenghtRay;
    private GameObject newindicator;
    Vector3 hitfloor;
    bool indicatorFlag;
    

    private void Awake()
    {
        particle = GetComponentInChildren<ParticleSystem>();
        mr = GetComponent<MeshRenderer>();
        bc = GetComponent<BoxCollider>();
        indicatorFlag = false;
    }

  
    public void FixedUpdate()
    {
        StartCoroutine(WaitForDrawIndicator());
    }
    IEnumerator WaitForDrawIndicator()
    {
        yield return new WaitForSeconds(1);
        if (!indicatorFlag)
        {
            DrawIndicator();
            indicatorFlag = true;
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("estalactita"))
        {
            Destroy(newindicator);
            Destroy(gameObject);
        }
        if (collision.gameObject.GetComponent<IDamageable>() != null)
        {
            collision.gameObject.GetComponent<IDamageable>().DecreaseHealth((int)damage);
        }
            StartCoroutine(Break());
    }

    private IEnumerator Break()
    {
        particle.Play();
        Destroy(newindicator);
        mr.enabled = false;
        bc.enabled = false;

        yield return new WaitForSeconds(particle.main.startLifetime.constantMax);
        Destroy(gameObject);
        
    }

    public void DrawIndicator()
    {
        if (Physics.Raycast(transform.position, -transform.up, out hit , maxLenghtRay , 1 << LayerMask.NameToLayer("Platform")))
        {
            hitfloor = hit.point;
            hitfloor.y = Player.instance.transform.position.y + 0.1f;
            newindicator = Instantiate(indicator, hitfloor, Quaternion.identity);
            
          
        }
    }
}

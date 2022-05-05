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
    

    private void Awake()
    {
        particle = GetComponentInChildren<ParticleSystem>();
        mr = GetComponent<MeshRenderer>();
        bc = GetComponent<BoxCollider>();
    }

    private void Start()
    {
        DrawIndicator();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player.instance.ModifyHealth(0, (int)damage);
        }
        StartCoroutine(Break());
    }

    private IEnumerator Break()
    {
        particle.Play();
        mr.enabled = false;
        bc.enabled = false;

        yield return new WaitForSeconds(particle.main.startLifetime.constantMax);
        Destroy(newindicator);
        Destroy(gameObject);
        
    }

    public void DrawIndicator()
    {
        if (Physics.Raycast(transform.position, -transform.up, out hit , maxLenghtRay , 1 << LayerMask.NameToLayer("Platform")))
        {
            hitfloor = hit.point;
            hitfloor.y = hit.point.y + 0.1f;
            newindicator = Instantiate(indicator, hitfloor, Quaternion.identity);
          
        }
    }
}

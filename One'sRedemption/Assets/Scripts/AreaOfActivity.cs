using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaOfActivity : MonoBehaviour
{
    [Tooltip("Radio de acción.")]
    public float ratio;
    [Tooltip("Altura de Spawn")]
    [SerializeField] float _height;
    [Tooltip("Cantidad de tiempo antes de Spawnear otra ronda")]
    [SerializeField] float _coolDown;
    [Tooltip("Cantidad de Estalactitas que se van a Spawnear.")]
    [SerializeField] int quantity;
    [SerializeField] GameObject _estalactitas;
    public bool _toggle;
    public bool playerInArea;
    
    public int _myIndex;
   

    public void Update()
    {
        float _distance = Vector3.Distance(transform.position, Player.instance.transform.position);
        if ( _distance <= ratio)
        {
            playerInArea = true;
        }
        else
        {
            playerInArea = false;
        }
       
    }
    public void StartSpawn()
    {
        StartCoroutine(SpawnObject());

    }
    public void StopSpawn()
    {
        StopCoroutine(SpawnObject());

    }


    IEnumerator SpawnObject()
    {

        while (BossController.instance.Health > 0)
        {
            for (int i = 0; i < quantity; i++)
            {
                Vector2 _spawnAtPos = new Vector2(Random.Range(-ratio, ratio), Random.Range(-ratio, ratio));
                GameObject e = Instantiate(_estalactitas);
                e.transform.position = transform.position + new Vector3(_spawnAtPos.x, _height, _spawnAtPos.y);
                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(_coolDown);
        }

        

        
    }
    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, ratio);
        Gizmos.color = Color.red;
           
    }
}

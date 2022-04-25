using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Geisser : MonoBehaviour
{
    public float     timer;
    [SerializeField]
    float             lavaTime;
    public GameObject lava;
    public bool startGeisser;

   

    public void Start()
    {
        lava.SetActive(false);
        startGeisser = false;
    }
    public void Update()
    {
        if (startGeisser)
        {
            StartCoroutine(Geiser(true));

        }
        
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerFeets"))
        {
            Debug.Log("EstalloElgeisser");
            startGeisser = true;
        }
    }
   public IEnumerator Geiser(bool start)
    {
        if (start)
        {
            lavaTime += Time.deltaTime;
            if (lavaTime<= timer)
            {
                lava.SetActive(true);
            }
            else
            {
                lavaTime = 0;

                lava.SetActive(false);
                yield return startGeisser=false;

            }
        }

    }
}

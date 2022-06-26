using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossFightStart : MonoBehaviour
{
    [SerializeField] Slider _bossBar;
    bool _fightstarted = false;

    private void Awake()
    {
        _bossBar.gameObject.SetActive(false);
    }

    //Cuando triggerea la barra del boss se vuelve visible
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerFeets"))
        {
            if (!_fightstarted)
            {
                AudioManager.instance.Play("Boss_Minotaur_Scream");
                _bossBar.gameObject.SetActive(true);
                _fightstarted = true;
            }

        }
    }

}
